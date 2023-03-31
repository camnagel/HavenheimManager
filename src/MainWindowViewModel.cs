using AssetManager.Containers;
using AssetManager.Editors;
using AssetManager.Enums;
using AssetManager.Import;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using AssetManager.Handlers;
using Condition = AssetManager.Enums.Condition;
using File = System.IO.File;

namespace AssetManager
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Primary Object Collections
        public List<Trait> MasterTraitList { get; } = new();
        public List<Feat> MasterFeatList { get; } = new();
        public List<Item> MasterItemList { get; } = new();

        // Selected Object Backing Collections
        public ObservableCollection<Trait> CurrentTrait { get; set; } = new();
        public ObservableCollection<Feat> CurrentFeat { get; set; } = new();
        public ObservableCollection<Spell> CurrentSpell { get; set; } = new();
        public ObservableCollection<Item> CurrentItem { get; set; } = new();

        private string _saveFileName = "";

        // Asset Commands
        public DelegateCommand LoadCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand SaveAsCommand { get; }
        public DelegateCommand ImportCommand { get; }

        // Handlers
        public FeatHandler FeatHandler { get; set; }
        public TraitHandler TraitHandler { get; set; }
        public ItemHandler ItemHandler { get; set; }

        public MainWindowViewModel()
        {
            LoadCommand = new DelegateCommand(LoadAction);
            SaveCommand = new DelegateCommand(SaveAction);
            SaveAsCommand = new DelegateCommand(SaveAsAction);
            ImportCommand = new DelegateCommand(ImportAction);
            
            // Traits
            TraitHandler = new TraitHandler(this);
            CoreTraitCheckboxCommand = new DelegateCommand(TraitHandler.TraitCoreFilterAction);
            SkillTraitCheckboxCommand = new DelegateCommand(TraitHandler.TraitSkillFilterAction);
            ClassTraitCheckboxCommand = new DelegateCommand(TraitHandler.TraitClassFilterAction);
            CombatTraitCheckboxCommand = new DelegateCommand(TraitHandler.TraitCombatFilterAction);
            RoleTraitCheckboxCommand = new DelegateCommand(TraitHandler.TraitRoleFilterAction);
            MagicTraitCheckboxCommand = new DelegateCommand(TraitHandler.TraitMagicFilterAction);
            BonusTraitCheckboxCommand = new DelegateCommand(TraitHandler.TraitBonusFilterAction);
            ConditionTraitCheckboxCommand = new DelegateCommand(TraitHandler.TraitConditionFilterAction);
            SourceTraitCheckboxCommand = new DelegateCommand(TraitHandler.TraitSourceFilterAction);
            CustomTraitCheckboxCommand = new DelegateCommand(TraitHandler.TraitCustomFilterAction);
            AddFavoriteTraitCommand = new DelegateCommand(TraitHandler.AddFavoriteTraitAction);
            AddHiddenTraitCommand = new DelegateCommand(TraitHandler.AddHiddenTraitAction);
            EditTraitCommand = new DelegateCommand(TraitHandler.EditTraitAction);
            NewTraitCommand = new DelegateCommand(TraitHandler.NewTraitAction);
            RemoveTraitCommand = new DelegateCommand(TraitHandler.RemoveTraitAction);
            RemoveFavoriteTraitCommand = new DelegateCommand(TraitHandler.RemoveFavoriteTraitAction);
            RemoveHiddenTraitCommand = new DelegateCommand(TraitHandler.RemoveHiddenTraitAction);
            TraitSearchRemovePlaceholderTextCommand = new DelegateCommand(TraitHandler.TraitSearchRemovePlaceholderTextAction);
            TraitSearchAddPlaceholderTextCommand = new DelegateCommand(TraitHandler.TraitSearchAddPlaceholderTextAction);
            
            // Feats
            FeatHandler = new FeatHandler(this);
            CoreFeatCheckboxCommand = new DelegateCommand(FeatHandler.FeatCoreFilterAction);
            SkillFeatCheckboxCommand = new DelegateCommand(FeatHandler.FeatSkillFilterAction);
            ClassFeatCheckboxCommand = new DelegateCommand(FeatHandler.FeatClassFilterAction);
            CombatFeatCheckboxCommand = new DelegateCommand(FeatHandler.FeatCombatFilterAction);
            RoleFeatCheckboxCommand = new DelegateCommand(FeatHandler.FeatRoleFilterAction);
            MagicFeatCheckboxCommand = new DelegateCommand(FeatHandler.FeatMagicFilterAction);
            BonusFeatCheckboxCommand = new DelegateCommand(FeatHandler.FeatBonusFilterAction);
            ConditionFeatCheckboxCommand = new DelegateCommand(FeatHandler.FeatConditionFilterAction);
            SourceFeatCheckboxCommand = new DelegateCommand(FeatHandler.FeatSourceFilterAction);
            CustomFeatCheckboxCommand = new DelegateCommand(FeatHandler.FeatCustomFilterAction);
            AddFavoriteFeatCommand = new DelegateCommand(FeatHandler.AddFavoriteFeatAction);
            AddHiddenFeatCommand = new DelegateCommand(FeatHandler.AddHiddenFeatAction);
            EditFeatCommand = new DelegateCommand(FeatHandler.EditFeatAction);
            NewFeatCommand = new DelegateCommand(FeatHandler.NewFeatAction);
            RemoveFeatCommand = new DelegateCommand(FeatHandler.RemoveFeatAction);
            RemoveFavoriteFeatCommand = new DelegateCommand(FeatHandler.RemoveFavoriteFeatAction);
            RemoveHiddenFeatCommand = new DelegateCommand(FeatHandler.RemoveHiddenFeatAction);
            FeatSearchRemovePlaceholderTextCommand = new DelegateCommand(FeatHandler.FeatSearchRemovePlaceholderTextAction);
            FeatSearchAddPlaceholderTextCommand = new DelegateCommand(FeatHandler.FeatSearchAddPlaceholderTextAction);
            FeatMinLevelRemovePlaceholderTextCommand = new DelegateCommand(FeatHandler.FeatMinLevelRemovePlaceholderTextAction);
            FeatMinLevelAddPlaceholderTextCommand = new DelegateCommand(FeatHandler.FeatMinLevelAddPlaceholderTextAction);
            FeatMaxLevelRemovePlaceholderTextCommand = new DelegateCommand(FeatHandler.FeatMaxLevelRemovePlaceholderTextAction);
            FeatMaxLevelAddPlaceholderTextCommand = new DelegateCommand(FeatHandler.FeatMaxLevelAddPlaceholderTextAction);

            // Items
            ItemHandler = new ItemHandler(this);
        }

        #region Traits
        // Filtered Trait Collections
        public ObservableCollection<Trait> FilteredTraitList { get; set; } = new();
        public ObservableCollection<Trait> FavoriteTraitList { get; set; } = new();
        public ObservableCollection<Trait> HiddenTraitList { get; set; } = new();

        // Trait Tag Collections
        public ObservableCollection<string> CustomTraitFilterList { get; set; } = new();
        public ObservableCollection<string> CoreTraitFilterList { get; set; } = new();
        public ObservableCollection<string> SkillTraitFilterList { get; set; } = new();
        public ObservableCollection<string> ClassTraitFilterList { get; set; } = new();
        public ObservableCollection<string> CombatTraitFilterList { get; set; } = new();
        public ObservableCollection<string> RoleTraitFilterList { get; set; } = new();
        public ObservableCollection<string> MagicTraitFilterList { get; set; } = new();
        public ObservableCollection<string> BonusTraitFilterList { get; set; } = new();
        public ObservableCollection<string> ConditionTraitFilterList { get; set; } = new();
        public ObservableCollection<string> SourceTraitFilterList { get; set; } = new();
        
        private Trait? _selectedTrait;
        public Trait? SelectedTrait
        {
            get => _selectedTrait;
            set
            {
                if (value != null)
                {
                    SelectedTrait = null;
                }
                _selectedTrait = value;
                CurrentTrait.Clear();
                if (value != null)
                {
                    CurrentTrait.Add(value);
                }

                OnPropertyChanged("SelectedTrait");
            }
        }

        private string _traitSearchText = RegexHandler.SearchPlaceholderText;
        public string TraitSearchText
        {
            get => _traitSearchText;
            set
            {
                _traitSearchText = value;
                TraitHandler.ApplyTraitFilters();

                OnPropertyChanged("TraitSearchText");
            }
        }

        // Trait Checkbox Commands
        public DelegateCommand CoreTraitCheckboxCommand { get; }
        public DelegateCommand SkillTraitCheckboxCommand { get; }
        public DelegateCommand ClassTraitCheckboxCommand { get; }
        public DelegateCommand CombatTraitCheckboxCommand { get; }
        public DelegateCommand RoleTraitCheckboxCommand { get; }
        public DelegateCommand MagicTraitCheckboxCommand { get; }
        public DelegateCommand BonusTraitCheckboxCommand { get; }
        public DelegateCommand ConditionTraitCheckboxCommand { get; }
        public DelegateCommand SourceTraitCheckboxCommand { get; }
        public DelegateCommand CustomTraitCheckboxCommand { get; }

        // Trait Control Bar Commands
        public DelegateCommand TraitSearchRemovePlaceholderTextCommand { get; }
        public DelegateCommand TraitSearchAddPlaceholderTextCommand { get; }
        public DelegateCommand AddFavoriteTraitCommand { get; }
        public DelegateCommand AddHiddenTraitCommand { get; }
        public DelegateCommand EditTraitCommand { get; }
        public DelegateCommand NewTraitCommand { get; }
        public DelegateCommand RemoveTraitCommand { get; }
        public DelegateCommand RemoveFavoriteTraitCommand { get; }
        public DelegateCommand RemoveHiddenTraitCommand { get; }
        #endregion

        #region Feats
        // Filtered Feat Collections
        public ObservableCollection<Feat> FilteredFeatList { get; set; } = new();
        public ObservableCollection<Feat> FavoriteFeatList { get; set; } = new();
        public ObservableCollection<Feat> HiddenFeatList { get; set; } = new();

        // Feat Tag Collections
        public ObservableCollection<string> CustomFeatFilterList { get; set; } = new();
        public ObservableCollection<string> CoreFeatFilterList { get; set; } = new();
        public ObservableCollection<string> SkillFeatFilterList { get; set; } = new();
        public ObservableCollection<string> ClassFeatFilterList { get; set; } = new();
        public ObservableCollection<string> CombatFeatFilterList { get; set; } = new();
        public ObservableCollection<string> RoleFeatFilterList { get; set; } = new();
        public ObservableCollection<string> MagicFeatFilterList { get; set; } = new();
        public ObservableCollection<string> BonusFeatFilterList { get; set; } = new();
        public ObservableCollection<string> ConditionFeatFilterList { get; set; } = new();
        public ObservableCollection<string> SourceFeatFilterList { get; set; } = new();

        private Feat? _selectedFeat;
        public Feat? SelectedFeat
        {
            get => _selectedFeat;
            set
            {
                if (value != null)
                {
                    SelectedFeat = null;
                }
                _selectedFeat = value;
                CurrentFeat.Clear();
                if (value != null)
                {
                    CurrentFeat.Add(value);
                }

                OnPropertyChanged("SelectedFeat");
            }
        }

        private string _selectedFeatReq;
        public string? SelectedFeatReq
        {
            get => _selectedFeatReq;
            set
            {
                _selectedFeatReq = value ?? "";
                string sanitizedSelection = _selectedFeatReq.Sanitize();
                foreach (Feat possibleFeat in MasterFeatList)
                {
                    if (possibleFeat.Name.Sanitize() == sanitizedSelection)
                    {
                        SelectedFeat = possibleFeat;
                        _selectedFeatReq = "";
                    }
                }

                OnPropertyChanged("SelectedFeatReq");
            }
        }

        private string _featSearchText = RegexHandler.SearchPlaceholderText;
        public string FeatSearchText
        {
            get => _featSearchText;
            set
            {
                _featSearchText = value;
                FeatHandler.ApplyFeatFilters();

                OnPropertyChanged("FeatSearchText");
            }
        }

        private string _featMinLevel = RegexHandler.FeatMinLevelPlaceholder.ToString();
        public string FeatMinLevel
        {
            get => _featMinLevel;
            set
            {
                if (value == "")
                {
                    _featMinLevel = value;
                    OnPropertyChanged("FeatMinLevel");
                    return;
                }
                if (!RegexHandler.NumberFilter.IsMatch(value))
                {
                    int input = int.Parse(value);
                    if (value == _featMinLevel)
                    {
                        return;
                    }
                    _featMinLevel = input > int.Parse(_featMaxLevel) ? _featMaxLevel : value;
                    FeatHandler.ApplyFeatFilters();
                    OnPropertyChanged("FeatMinLevel");
                }
                else
                {
                    string messageBoxText = "Level must be an integer between 0 and 20";
                    string caption = "Error";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;

                    MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
                }
            }
        }

        private string _featMaxLevel = RegexHandler.FeatMaxLevelPlaceholder.ToString();
        public string FeatMaxLevel
        {
            get => _featMaxLevel;
            set
            {
                if (value == "")
                {
                    _featMaxLevel = value;
                    OnPropertyChanged("FeatMaxLevel");
                    return;
                }
                if (!RegexHandler.NumberFilter.IsMatch(value) && int.Parse(value) <= 20)
                {
                    int input = int.Parse(value);
                    if (value == _featMaxLevel)
                    {
                        return;
                    }
                    _featMaxLevel = input < int.Parse(_featMinLevel) ? _featMinLevel : value;
                    FeatHandler.ApplyFeatFilters();
                    OnPropertyChanged("FeatMaxLevel");
                }
                else
                {
                    string messageBoxText = "Level must be an integer between 0 and 20";
                    string caption = "Error";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;

                    MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
                }
            }
        }

        // Feat Checkbox Commands
        public DelegateCommand CoreFeatCheckboxCommand { get; }
        public DelegateCommand SkillFeatCheckboxCommand { get; }
        public DelegateCommand ClassFeatCheckboxCommand { get; }
        public DelegateCommand CombatFeatCheckboxCommand { get; }
        public DelegateCommand RoleFeatCheckboxCommand { get; }
        public DelegateCommand MagicFeatCheckboxCommand { get; }
        public DelegateCommand BonusFeatCheckboxCommand { get; }
        public DelegateCommand ConditionFeatCheckboxCommand { get; }
        public DelegateCommand SourceFeatCheckboxCommand { get; }
        public DelegateCommand CustomFeatCheckboxCommand { get; }
        public DelegateCommand FeatMinLevelRemovePlaceholderTextCommand { get; }
        public DelegateCommand FeatMinLevelAddPlaceholderTextCommand { get; }
        public DelegateCommand FeatMaxLevelRemovePlaceholderTextCommand { get; }
        public DelegateCommand FeatMaxLevelAddPlaceholderTextCommand { get; }

        // Feat Control Bar Commands
        public DelegateCommand FeatSearchRemovePlaceholderTextCommand { get; }
        public DelegateCommand FeatSearchAddPlaceholderTextCommand { get; }
        public DelegateCommand AddFavoriteFeatCommand { get; }
        public DelegateCommand AddHiddenFeatCommand { get; }
        public DelegateCommand EditFeatCommand { get; }
        public DelegateCommand NewFeatCommand { get; }
        public DelegateCommand RemoveFeatCommand { get; }
        public DelegateCommand RemoveFavoriteFeatCommand { get; }
        public DelegateCommand RemoveHiddenFeatCommand { get; }
        #endregion

        #region Items
        // Filtered Item Collections
        public ObservableCollection<Item> FilteredItemList { get; set; } = new();
        public ObservableCollection<Item> FavoriteItemList { get; set; } = new();
        public ObservableCollection<Item> HiddenItemList { get; set; } = new();

        // Item Tag Collections
        public ObservableCollection<string> CustomItemFilterList { get; set; } = new();
        public ObservableCollection<string> CoreItemFilterList { get; set; } = new();
        public ObservableCollection<string> SkillItemFilterList { get; set; } = new();
        public ObservableCollection<string> ClassItemFilterList { get; set; } = new();
        public ObservableCollection<string> CombatItemFilterList { get; set; } = new();
        public ObservableCollection<string> RoleItemFilterList { get; set; } = new();
        public ObservableCollection<string> MagicItemFilterList { get; set; } = new();
        public ObservableCollection<string> BonusItemFilterList { get; set; } = new();
        public ObservableCollection<string> SourceItemFilterList { get; set; } = new();

        private Item? _selectedItem;
        public Item? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value != null)
                {
                    SelectedItem = null;
                }
                _selectedItem = value;
                CurrentItem.Clear();
                if (value != null)
                {
                    CurrentItem.Add(value);
                }

                OnPropertyChanged("SelectedItem");
            }
        }

        private string _selectedItemReq;
        public string? SelectedItemReq
        {
            get => _selectedItemReq;
            set
            {
                _selectedItemReq = value ?? "";
                string sanitizedSelection = _selectedItemReq.Sanitize();
                foreach (Item possibleItem in MasterItemList)
                {
                    if (possibleItem.Name.Sanitize() == sanitizedSelection)
                    {
                        SelectedItem = possibleItem;
                        _selectedItemReq = "";
                    }
                }

                OnPropertyChanged("SelectedItemReq");
            }
        }

        private string _itemSearchText = RegexHandler.SearchPlaceholderText;
        public string ItemSearchText
        {
            get => _itemSearchText;
            set
            {
                _itemSearchText = value;
                ItemHandler.ApplyItemFilters();

                OnPropertyChanged("ItemSearchText");
            }
        }

        // Item Checkbox Commands
        public DelegateCommand CoreItemCheckboxCommand { get; }
        public DelegateCommand SkillItemCheckboxCommand { get; }
        public DelegateCommand ClassItemCheckboxCommand { get; }
        public DelegateCommand CombatItemCheckboxCommand { get; }
        public DelegateCommand RoleItemCheckboxCommand { get; }
        public DelegateCommand MagicItemCheckboxCommand { get; }
        public DelegateCommand BonusItemCheckboxCommand { get; }
        public DelegateCommand SourceItemCheckboxCommand { get; }
        public DelegateCommand CustomItemCheckboxCommand { get; }

        // Item Control Bar Commands
        public DelegateCommand ItemSearchRemovePlaceholderTextCommand { get; }
        public DelegateCommand ItemSearchAddPlaceholderTextCommand { get; }
        public DelegateCommand AddFavoriteItemCommand { get; }
        public DelegateCommand AddHiddenItemCommand { get; }
        public DelegateCommand EditItemCommand { get; }
        public DelegateCommand NewItemCommand { get; }
        public DelegateCommand RemoveItemCommand { get; }
        public DelegateCommand RemoveFavoriteItemCommand { get; }
        public DelegateCommand RemoveHiddenItemCommand { get; }
        #endregion

        private void LoadAction(object arg)
        {
            OpenFileDialog dialog = new()
            {
                Title = "Select File",
                Multiselect = false
            };

            if (dialog.ShowDialog() == true)
            {
                SerialBin bin = JsonSerializer.Deserialize<SerialBin>(File.ReadAllText(dialog.FileName))!;

                /*foreach (Clip clip in bin.ClipList)
                {
                    ClipList.Add(clip);
                }*/
            }
        }

        private void SaveAction(object arg)
        {
            if (_saveFileName.Length == 0)
            {
                SaveAsAction(arg);
                return;
            }
            SerializeAndSave(_saveFileName);
        }

        private void SerializeAndSave(string filename)
        {
            /*_saveFileName = filename;
            var jsonOpts = new JsonSerializerOptions { WriteIndented = true };
            var bin = new SerialBin(ClipList, ReadyList, ExportedList, HeadList);
            string serialBin = JsonSerializer.Serialize(bin, jsonOpts);

            File.WriteAllText(filename, serialBin);*/
        }

        private void SaveAsAction(object arg)
        {
            SaveFileDialog dialog = new()
            {
                Title = "Enter Filename"
            };

            if (dialog.ShowDialog() == true)
            {
                SerializeAndSave(dialog.FileName);
            }
        }

        private void ImportAction(object arg)
        {
            try
            {
                var vm = new ImportViewModel();
                var configWindow = new ImportView(vm);

                if (configWindow.ShowDialog() == true)
                {
                    string importPath = vm.GetSourcePath();
                    SourceType type = vm.GetSourceType();

                    switch (type)
                    {
                        case SourceType.Trait:
                            foreach (Trait trait in ImportReader.ReadTraitCsv(importPath))
                            {
                                MasterTraitList.Add(trait);
                            }
                            TraitHandler.UpdateTraitCustomTags();
                            TraitHandler.ApplyTraitFilters();
                            break;
                        case SourceType.Feat:
                            foreach (Feat feat in ImportReader.ReadFeatCsv(importPath))
                            {
                                MasterFeatList.Add(feat);
                            }
                            FeatHandler.UpdateFeatReqs();
                            FeatHandler.UpdateFeatCustomTags();
                            FeatHandler.ApplyFeatFilters();
                            break;
                        case SourceType.Item:
                            ImportReader.ReadItemCsv(importPath);
                            break;
                        case SourceType.Spell:
                            ImportReader.ReadSpellCsv(importPath);
                            break;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                string messageBoxText = "Source type was not selected";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                
                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }

            RefreshButtonState();
        }

        public void RefreshButtonState()
        {
            LoadCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            LoadCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            SaveAsCommand.RaiseCanExecuteChanged();
            ImportCommand.RaiseCanExecuteChanged();
            CoreTraitCheckboxCommand.RaiseCanExecuteChanged();
            SkillTraitCheckboxCommand.RaiseCanExecuteChanged();
            ClassTraitCheckboxCommand.RaiseCanExecuteChanged();
            CombatTraitCheckboxCommand.RaiseCanExecuteChanged();
            RoleTraitCheckboxCommand.RaiseCanExecuteChanged();
            MagicTraitCheckboxCommand.RaiseCanExecuteChanged();
            SourceTraitCheckboxCommand.RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
