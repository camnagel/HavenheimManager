using AssetManager.Containers;
using AssetManager.Editors;
using AssetManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AssetManager.Extensions;
using Condition = AssetManager.Enums.Condition;

namespace AssetManager.Handlers
{
    public class TraitHandler
    {
        public TraitHandler(MainWindowViewModel vm)
        {
            _vm = vm;
        }

        private readonly MainWindowViewModel _vm;

        // Trait Filter Lists
        private HashSet<Core> CoreTraitFilters = new HashSet<Core>();
        private HashSet<Skill> SkillTraitFilters = new HashSet<Skill>();
        private HashSet<Class> ClassTraitFilters = new HashSet<Class>();
        private HashSet<Combat> CombatTraitFilters = new HashSet<Combat>();
        private HashSet<Role> RoleTraitFilters = new HashSet<Role>();
        private HashSet<Magic> MagicTraitFilters = new HashSet<Magic>();
        private HashSet<Bonus> BonusTraitFilters = new HashSet<Bonus>();
        private HashSet<Condition> ConditionTraitFilters = new HashSet<Condition>();
        private HashSet<Source> SourceTraitFilters = new HashSet<Source>();
        private HashSet<string> CustomTraitFilters = new HashSet<string>();

        private bool _activeTraitFilters => CoreTraitFilters.Any() ||
                                            SkillTraitFilters.Any() ||
                                            ClassTraitFilters.Any() ||
                                            CombatTraitFilters.Any() ||
                                            RoleTraitFilters.Any() ||
                                            ConditionTraitFilters.Any() ||
                                            SourceTraitFilters.Any() ||
                                            CustomTraitFilters.Any() ||
                                            MagicTraitFilters.Any() ||
                                            BonusTraitFilters.Any();

        internal void TraitCoreFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Core toggleCore = filter.StringToCore();

                if (CoreTraitFilters.Contains(toggleCore))
                {
                    CoreTraitFilters.Remove(toggleCore);
                }
                else
                {
                    CoreTraitFilters.Add(toggleCore);
                }

                ApplyTraitFilters();
            }
        }

        internal void TraitSkillFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Skill toggleSkill = filter.StringToSkill();

                if (SkillTraitFilters.Contains(toggleSkill))
                {
                    SkillTraitFilters.Remove(toggleSkill);
                }
                else
                {
                    SkillTraitFilters.Add(toggleSkill);
                }

                ApplyTraitFilters();
            }
        }

        internal void TraitClassFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Class toggleClass = filter.StringToClass();

                if (ClassTraitFilters.Contains(toggleClass))
                {
                    ClassTraitFilters.Remove(toggleClass);
                }
                else
                {
                    ClassTraitFilters.Add(toggleClass);
                }

                ApplyTraitFilters();
            }
        }

        internal void TraitCombatFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Combat toggleCombat = filter.StringToCombat();

                if (CombatTraitFilters.Contains(toggleCombat))
                {
                    CombatTraitFilters.Remove(toggleCombat);
                }
                else
                {
                    CombatTraitFilters.Add(toggleCombat);
                }

                ApplyTraitFilters();
            }
        }

        internal void TraitRoleFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Role toggleRole = filter.StringToRole();

                if (RoleTraitFilters.Contains(toggleRole))
                {
                    RoleTraitFilters.Remove(toggleRole);
                }
                else
                {
                    RoleTraitFilters.Add(toggleRole);
                }

                ApplyTraitFilters();
            }
        }

        internal void TraitMagicFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Magic toggleMagic = filter.StringToMagic();

                if (MagicTraitFilters.Contains(toggleMagic))
                {
                    MagicTraitFilters.Remove(toggleMagic);
                }
                else
                {
                    MagicTraitFilters.Add(toggleMagic);
                }

                ApplyTraitFilters();
            }
        }

        internal void TraitBonusFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Bonus toggleBonus = filter.StringToBonus();

                if (BonusTraitFilters.Contains(toggleBonus))
                {
                    BonusTraitFilters.Remove(toggleBonus);
                }
                else
                {
                    BonusTraitFilters.Add(toggleBonus);
                }

                ApplyTraitFilters();
            }
        }

        internal void TraitConditionFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Condition toggleCondition = filter.StringToCondition();

                if (ConditionTraitFilters.Contains(toggleCondition))
                {
                    ConditionTraitFilters.Remove(toggleCondition);
                }
                else
                {
                    ConditionTraitFilters.Add(toggleCondition);
                }

                ApplyTraitFilters();
            }
        }

        internal void TraitSourceFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Source toggleSource = filter.StringToSource();

                if (SourceTraitFilters.Contains(toggleSource))
                {
                    SourceTraitFilters.Remove(toggleSource);
                }
                else
                {
                    SourceTraitFilters.Add(toggleSource);
                }

                ApplyTraitFilters();
            }
        }

        internal void TraitCustomFilterAction(object arg)
        {
            if (arg is string filter)
            {
                if (CustomTraitFilters.Contains(filter))
                {
                    CustomTraitFilters.Remove(filter);
                }
                else
                {
                    CustomTraitFilters.Add(filter);
                }

                ApplyTraitFilters();
            }
        }

        internal void ApplyTraitFilters()
        {
            _vm.FilteredTraitList.Clear();
            List<Trait> possibleTraits = (_vm.TraitSearchText != RegexHandler.SearchPlaceholderText && _vm.TraitSearchText != "" ?
                _vm.MasterTraitList.Where(x => x.Name.Sanitize().Contains(_vm.TraitSearchText.Sanitize())).ToList() :
                _vm.MasterTraitList).Where(x => !_vm.FavoriteTraitList.Contains(x) && !_vm.HiddenTraitList.Contains(x)).ToList();

            foreach (Source filter in SourceTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.Source == filter).ToList();
            }

            foreach (Trait trait in BroadTraitFilter(possibleTraits))
            {
                _vm.FilteredTraitList.Add(trait);
            }
        }

        private IEnumerable<Trait> BroadTraitFilter(List<Trait> possibleTraits)
        {
            if (!_activeTraitFilters)
            {
                foreach (Trait trait in possibleTraits)
                {
                    yield return trait;
                }
                yield break;
            }

            foreach (Trait trait in possibleTraits)
            {
                if (CoreTraitFilters.Any(filter => trait.CoreTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (SkillTraitFilters.Any(filter => trait.SkillTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (ClassTraitFilters.Any(filter => trait.ClassTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (CombatTraitFilters.Any(filter => trait.CombatTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (RoleTraitFilters.Any(filter => trait.RoleTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (MagicTraitFilters.Any(filter => trait.MagicTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (BonusTraitFilters.Any(filter => trait.BonusTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (ConditionTraitFilters.Any(filter => trait.ConditionTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (CustomTraitFilters.Any(filter => trait.CustomTags.Contains(filter)))
                {
                    yield return trait;
                }
            }
        }

        private IEnumerable<Trait> StrictTraitFilter(List<Trait> possibleTraits)
        {
            foreach (Core filter in CoreTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.CoreTags.Contains(filter)).ToList();
            }

            foreach (Skill filter in SkillTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.SkillTags.Contains(filter)).ToList();
            }

            foreach (Class filter in ClassTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.ClassTags.Contains(filter)).ToList();
            }

            foreach (Combat filter in CombatTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.CombatTags.Contains(filter)).ToList();
            }

            foreach (Role filter in RoleTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.RoleTags.Contains(filter)).ToList();
            }

            foreach (Magic filter in MagicTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.MagicTags.Contains(filter)).ToList();
            }

            foreach (Bonus filter in BonusTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.BonusTags.Contains(filter)).ToList();
            }

            foreach (Condition filter in ConditionTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.ConditionTags.Contains(filter)).ToList();
            }

            foreach (string filter in CustomTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.CustomTags.Contains(filter)).ToList();
            }

            foreach (Trait trait in possibleTraits)
            {
                yield return trait;
            }
        }

        internal void UpdateTraitCustomTags()
        {
            _vm.CustomTraitFilterList.Clear();
            foreach (Trait trait in _vm.MasterTraitList)
            {
                if (_vm.FavoriteTraitList.Contains(trait) || _vm.HiddenTraitList.Contains(trait))
                    continue;

                foreach (string tag in trait.CustomTags)
                {
                    if (!_vm.CustomTraitFilterList.Contains(tag))
                    {
                        _vm.CustomTraitFilterList.Add(tag);
                    }
                }
            }
        }

        internal void AddFavoriteTraitAction(object arg)
        {
            if (_vm.SelectedTrait != null && !_vm.FavoriteTraitList.Contains(_vm.SelectedTrait))
            {
                _vm.FavoriteTraitList.Add(_vm.SelectedTrait);
                _vm.HiddenTraitList.Remove(_vm.SelectedTrait);

                UpdateTraitCustomTags();
                ApplyTraitFilters();
                _vm.SelectedTrait = null;
            }
        }

        internal void AddHiddenTraitAction(object arg)
        {
            if (_vm.SelectedTrait != null && !_vm.HiddenTraitList.Contains(_vm.SelectedTrait))
            {
                _vm.HiddenTraitList.Add(_vm.SelectedTrait);
                _vm.FavoriteTraitList.Remove(_vm.SelectedTrait);

                UpdateTraitCustomTags();
                ApplyTraitFilters();
                _vm.SelectedTrait = null;
            }
        }

        internal void EditTraitAction(object arg)
        {
            try
            {
                if (_vm.SelectedTrait != null)
                {
                    var vm = new TraitViewModel(_vm.SelectedTrait);
                    var configWindow = new TraitView(vm);

                    if (configWindow.ShowDialog() == true)
                    {
                        Trait newTrait = vm.GetTrait();

                        if (_vm.MasterTraitList.Contains(_vm.SelectedTrait))
                        {
                            _vm.MasterTraitList.Remove(_vm.SelectedTrait);
                            _vm.MasterTraitList.Add(newTrait);

                            if (_vm.FavoriteTraitList.Contains(_vm.SelectedTrait))
                            {
                                _vm.FavoriteTraitList.Remove(_vm.SelectedTrait);
                                _vm.FavoriteTraitList.Add(newTrait);
                            }

                            else if (_vm.HiddenTraitList.Contains(_vm.SelectedTrait))
                            {
                                _vm.HiddenTraitList.Remove(_vm.SelectedTrait);
                                _vm.HiddenTraitList.Add(newTrait);
                            }

                            UpdateTraitCustomTags();
                            ApplyTraitFilters();

                            _vm.SelectedTrait = newTrait;
                        }
                    }
                }
                else
                {
                    string messageBoxText = "No trait selected to edit";
                    string caption = "Select Trait";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Exclamation;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                string messageBoxText = "Exception when adding trait";
                string caption = "Exception";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }

            _vm.RefreshButtonState();
        }

        internal void NewTraitAction(object arg)
        {
            try
            {
                var vm = new TraitViewModel(new Trait());
                var configWindow = new TraitView(vm);

                if (configWindow.ShowDialog() == true)
                {
                    Trait newTrait = vm.GetTrait();
                    if (_vm.MasterTraitList.Select(x => x.Name).Contains(newTrait.Name))
                    {
                        string messageBoxText = "Trait with same name already exists";
                        string caption = "Duplicate";
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Exclamation;
                        MessageBox.Show(messageBoxText, caption, button, icon);
                    }
                    else
                    {
                        _vm.MasterTraitList.Add(newTrait);

                        UpdateTraitCustomTags();
                        ApplyTraitFilters();
                        if (_vm.FilteredTraitList.Contains(newTrait))
                        {
                            _vm.SelectedTrait = newTrait;
                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                string messageBoxText = "Exception when adding trait";
                string caption = "Exception";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }

            _vm.RefreshButtonState();
        }

        internal void TraitSearchRemovePlaceholderTextAction(object arg)
        {
            if (_vm.TraitSearchText == RegexHandler.SearchPlaceholderText)
            {
                _vm.TraitSearchText = "";
            }
        }

        internal void TraitSearchAddPlaceholderTextAction(object arg)
        {
            if (string.IsNullOrWhiteSpace(_vm.TraitSearchText))
            {
                _vm.TraitSearchText = RegexHandler.SearchPlaceholderText;
            }
        }

        internal void RemoveTraitAction(object arg)
        {
            if (_vm.SelectedTrait != null)
            {
                string messageBoxText = "Trait will be removed. Are you sure?";
                string caption = "Warning";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                if (result == MessageBoxResult.Yes)
                {
                    _vm.MasterTraitList.Remove(_vm.SelectedTrait);
                    _vm.HiddenTraitList.Remove(_vm.SelectedTrait);
                    _vm.FavoriteTraitList.Remove(_vm.SelectedTrait);
                    _vm.SelectedTrait = null;
                    UpdateTraitCustomTags();
                    ApplyTraitFilters();
                }
            }
        }

        internal void RemoveFavoriteTraitAction(object arg)
        {
            if (_vm.SelectedTrait != null && _vm.FavoriteTraitList.Contains(_vm.SelectedTrait))
            {
                _vm.FavoriteTraitList.Remove(_vm.SelectedTrait);

                _vm.SelectedTrait = null;
                UpdateTraitCustomTags();
                ApplyTraitFilters();
            }
        }

        internal void RemoveHiddenTraitAction(object arg)
        {
            if (_vm.SelectedTrait != null && _vm.HiddenTraitList.Contains(_vm.SelectedTrait))
            {
                _vm.HiddenTraitList.Remove(_vm.SelectedTrait);

                _vm.SelectedTrait = null;
                UpdateTraitCustomTags();
                ApplyTraitFilters();
            }
        }
    }
}
