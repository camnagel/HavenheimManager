using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Imaging;
using AssetManager.Containers;
using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace AssetManager
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Clip> ClipList { get; set; } = new();

        public ObservableCollection<Clip> ReadyList { get; set; } = new();

        public ObservableCollection<Clip> ExportedList { get; set; } = new();

        public ObservableCollection<Name> HeadList { get; set; } = new();

        public ObservableCollection<Name> ComparisonList { get; set; } = new();

        public ObservableCollection<Clip> CurrentClip { get; set; } = new();

        private string _saveFileName = "";

        private Uri _traitPath;
        public Uri? TraitPath
        {
            get => _traitPath;
            set
            {
                _traitPath = value;
                OnPropertyChanged("TraitPath");
            }
        }

        private BitmapImage? _clipImage;
        public BitmapImage? ClipImage
        {
            get => _clipImage;
            set
            {
                _clipImage = value;
                OnPropertyChanged("ClipImage");
            }
        }

        private BitmapImage? _headImage;
        public BitmapImage? HeadImage
        {
            get => _headImage;
            set
            {
                _headImage = value;
                OnPropertyChanged("HeadImage");
            }
        }

        /// <summary>
        /// The selected clip
        /// </summary>
        private Clip? _selectedClip;
        public Clip? SelectedClip
        {
            get => _selectedClip;
            set
            {
                if (value != null)
                {
                    SelectedClip = null;
                }
                _selectedClip = value;
                CurrentClip.Clear();
                ClipImage = null;
                if (value != null)
                {
                    CurrentClip.Add(value);
                    if (value.ClipImagePath is { Length: > 0 })
                    {
                        ClipImage = new BitmapImage(new Uri(value.ClipImagePath, UriKind.Absolute));
                    }
                }

                UpdateComparisonList();
                OnPropertyChanged("SelectedClip");
            }
        }

        /// <summary>
        /// The selected name for the Master Reference List
        /// </summary>
        private Name? _selectedMasterName;
        public Name? SelectedMasterName
        {
            get => _selectedMasterName;
            set
            {
                HeadImage = null;
                _selectedMasterName = value;
                UpdateComparisonList();
                OnPropertyChanged("SelectedMasterName");
            }
        }

        /// <summary>
        /// The selected head for the Master Reference List
        /// </summary>
        private Head? _selectedMasterHead;
        public Head? SelectedMasterHead
        {
            get => _selectedMasterHead;
            set
            {
                _selectedMasterHead = value;
                HeadImage = null;
                UpdateComparisonList();
                if (value is { HeadImagePath: { Length: > 0 } })
                {
                    HeadImage = new BitmapImage(new Uri(value.HeadImagePath, UriKind.Absolute));
                }
                OnPropertyChanged("SelectedMasterHead");
            }
        }

        /// <summary>
        /// The selected name
        /// </summary>
        private Name? _selectedName;
        public Name? SelectedName
        {
            get => _selectedName;
            set
            {
                _selectedName = value;
                OnPropertyChanged("SelectedName");
            }
        }

        /// <summary>
        /// The selected head
        /// </summary>
        private Head? _selectedHead;
        public Head? SelectedHead
        {
            get => _selectedHead;
            set
            {
                _selectedHead = value;
                OnPropertyChanged("SelectedHead");
            }
        }

        /// <summary>
        /// The selected name in the comparison list
        /// </summary>
        private Name? _selectedCompareName;
        public Name? SelectedCompareName
        {
            get => _selectedCompareName;
            set
            {
                _selectedCompareName = value;
                OnPropertyChanged("SelectedCompareName");
            }
        }

        private string? _rejectedHead;
        public string? RejectedHead
        {
            get => _rejectedHead;
            set
            {
                _rejectedHead = value;
                OnPropertyChanged("RejectedHead");
            }
        }

        private string? _exportHead;
        public string? ExportHead
        {
            get => _exportHead;
            set
            {
                _exportHead = value;
                OnPropertyChanged("ExportHead");
            }
        }

        private string? _readyHead;
        public string? ReadyHead
        {
            get => _readyHead;
            set
            {
                _readyHead = value;
                OnPropertyChanged("ReadyHead");
            }
        }

        // Asset Commands
        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand SaveAsCommand { get; set; }

        // Compare Commands
        public DelegateCommand MoveNameCommand { get; set; }

        //Checkbox Commands
        public DelegateCommand CheckedCategoryCheckboxCommand { get; set; } // TODO
        public DelegateCommand UncheckedCategoryCheckboxCommand { get; set; } // TODO

        // Master Commands
        public DelegateCommand AddMasterNameCommand { get; set; }
        public DelegateCommand RemoveMasterNameCommand { get; set; }
        public DelegateCommand SetHeadPathCommand { get; set; }
        public DelegateCommand AddMasterHeadCommand { get; set; }
        public DelegateCommand RemoveMasterHeadCommand { get; set; }

        // Clip Commands
        public DelegateCommand AddClipCommand { get; set; }
        public DelegateCommand RemoveClipCommand { get; set; }
        public DelegateCommand SetClipPathCommand { get; set; }
        public DelegateCommand MarkClipActiveCommand { get; set; }
        public DelegateCommand MarkClipReadyCommand { get; set; }
        public DelegateCommand MarkClipExportedCommand { get; set; }

        // Name Commands
        public DelegateCommand AddNameCommand { get; set; }
        public DelegateCommand RemoveNameCommand { get; set; }

        // Head Commands
        public DelegateCommand AddHeadCommand { get; set; }
        public DelegateCommand RejectHeadCommand { get; set; }
        public DelegateCommand ExportHeadCommand { get; set; }
        public DelegateCommand RemoveHeadCommand { get; set; }
        public DelegateCommand ReadyHeadCommand { get; set; }

        public MainWindowViewModel()
        {
            LoadCommand = new DelegateCommand(LoadAction);
            SaveCommand = new DelegateCommand(SaveAction);
            SaveAsCommand = new DelegateCommand(SaveAsAction);
            AddMasterNameCommand = new DelegateCommand(AddMasterNameAction);
            AddMasterHeadCommand = new DelegateCommand(AddMasterHeadAction);
            RemoveMasterHeadCommand = new DelegateCommand(RemoveMasterHeadAction);
            RemoveMasterNameCommand = new DelegateCommand(RemoveMasterNameAction);
            SetHeadPathCommand = new DelegateCommand(SetHeadPathAction);
            AddClipCommand = new DelegateCommand(AddClipAction);
            RemoveClipCommand = new DelegateCommand(RemoveClipAction);
            MarkClipActiveCommand = new DelegateCommand(MarkClipActiveAction);
            MarkClipReadyCommand = new DelegateCommand(MarkClipReadyAction);
            MarkClipExportedCommand = new DelegateCommand(MarkClipExportedAction);
            SetClipPathCommand = new DelegateCommand(SetClipPathAction);
            AddNameCommand = new DelegateCommand(AddNameAction);
            RemoveNameCommand = new DelegateCommand(RemoveNameAction);
            AddHeadCommand = new DelegateCommand(AddHeadAction);
            RejectHeadCommand = new DelegateCommand(RejectHeadAction);
            ExportHeadCommand = new DelegateCommand(ExportHeadAction);
            RemoveHeadCommand = new DelegateCommand(RemoveHeadAction);
            ReadyHeadCommand = new DelegateCommand(ReadyHeadAction);
            MoveNameCommand = new DelegateCommand(MoveNameAction);
        }

        private void MoveNameAction(object arg)
        {
            if (SelectedCompareName != null && SelectedName != null && 
                SelectedCompareName.NameName == SelectedName.NameName)
            {
                foreach (var head in SelectedCompareName.Heads)
                {
                    if (SelectedName.Heads.Select(x => x.HeadName).Contains(head.HeadName)) continue;

                    SelectedName.Heads.Add(new Head(head.HeadName));
                }
                
                UpdateComparisonList();
            }
        }

        /// <summary>
        /// Removes the head from the selected name in the selected clip
        /// </summary>
        /// <param name="arg"></param>
        private void RemoveHeadAction(object arg)
        {
            MessageBoxResult result = MessageBox.Show(
                "Confirm?", "Remove Head", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;

            if (SelectedName != null)
            {
                if (SelectedHead != null && SelectedName.Heads.Select(x => x.HeadName).Contains(SelectedHead.HeadName))
                {
                    SelectedName.Heads.Remove(SelectedHead);
                    SelectedHead = null;
                }

                if (RejectedHead != null && SelectedName.Rejected.Contains(RejectedHead))
                {
                    SelectedName.Rejected.Remove(RejectedHead);
                    RejectedHead = null;
                }

                if (ExportHead != null && SelectedName.Exported.Contains(ExportHead))
                {
                    SelectedName.Exported.Remove(ExportHead);
                    ExportHead = null;
                }

                if (ReadyHead != null && SelectedName.Ready.Contains(ReadyHead))
                {
                    SelectedName.Ready.Remove(ReadyHead);
                    ReadyHead = null;
                }

                UpdateComparisonList();
            }
        }

        private void ReadyHeadAction(object arg)
        {
            if (SelectedName != null && SelectedHead != null)
            {
                SelectedName.Ready.Add(SelectedHead.HeadName);
                SelectedName.Heads.Remove(SelectedHead);
                SelectedHead = null;
            }
        }

        private void ExportHeadAction(object arg)
        {
            if (SelectedName != null && SelectedHead != null)
            {
                SelectedName.Exported.Add(SelectedHead.HeadName);
                SelectedName.Heads.Remove(SelectedHead);
                SelectedHead = null;
            }
        }

        private void RejectHeadAction(object arg)
        {
            if (SelectedName != null && SelectedHead != null)
            {
                SelectedName.Rejected.Add(SelectedHead.HeadName);
                SelectedName.Heads.Remove(SelectedHead);
                SelectedHead = null;
            }
        }

        private void AddHeadAction(object arg)
        {
            if (SelectedName != null)
            {
                var result = Interaction.InputBox("Enter Head", "Add Head");
                if (result.Length == 0) return;
                if (SelectedName.Heads.Select(x => x.HeadName).Contains(result)) return;
                SelectedName.Heads.Add(new Head(result));
                UpdateComparisonList();
                SelectedHead = null;
            }
        }

        private void RemoveNameAction(object arg)
        {
            MessageBoxResult result = MessageBox.Show(
                "Confirm?", "Remove Name", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;

            if (SelectedClip != null && SelectedName != null)
            {
                SelectedClip.Names.Remove(SelectedName);
                UpdateComparisonList();
                SelectedName = null;
                SelectedHead = null;
            }
        }

        private void AddNameAction(object arg)
        {
            if (SelectedClip != null)
            {
                var result = Interaction.InputBox("Enter Name", "Add Name");
                if (result.Length == 0) return;
                if (SelectedClip.Names.Select(x => x.NameName).Contains(result)) return;
                SelectedClip.Names.Add(new Name(result, new ObservableCollection<Head>(), new ObservableCollection<string>()));
                UpdateComparisonList();
                SelectedName = null;
            }
        }

        private void SetClipPathAction(object arg)
        {
            if (SelectedClip != null)
            {
                OpenFileDialog dialog = new()
                {
                    Title = "Select Image",
                    Multiselect = false
                };

                if (dialog.ShowDialog() == true)
                {
                    SelectedClip.ClipImagePath = dialog.FileName;
                }
            }
        }

        private void RemoveClipAction(object arg)
        {
            MessageBoxResult result = MessageBox.Show(
                "Confirm?", "Remove Clip", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;

            if (SelectedClip != null)
            {
                ClipList.Remove(SelectedClip);
                SelectedClip = null;
                SelectedName = null;
                SelectedHead = null;
                UpdateComparisonList();
            }
        }

        private void AddClipAction(object arg)
        {
            string result = Interaction.InputBox("Enter Clip", "Add Clip");
            if (result.Length == 0) return;
            if (ClipList.Select(x => x.ClipName).Contains(result)) return;

            ClipList.Add(new Clip(result, new ObservableCollection<Name>()));
            SelectedClip = null;
        }

        private void MarkClipReadyAction(object arg)
        {
            if (SelectedClip != null)
            {
                ReadyList.Add(SelectedClip);
                ExportedList.Remove(SelectedClip);
                ClipList.Remove(SelectedClip);
            }
            SelectedClip = null;
        }

        private void MarkClipActiveAction(object arg)
        {
            if (SelectedClip != null && !ClipList.Contains(SelectedClip))
            {
                ClipList.Add(SelectedClip);
                ReadyList.Remove(SelectedClip);
                ExportedList.Remove(SelectedClip);
            }
            SelectedClip = null;
        }

        private void MarkClipExportedAction(object arg)
        {
            if (SelectedClip != null)
            {
                ExportedList.Add(SelectedClip);
                ReadyList.Remove(SelectedClip);
                ClipList.Remove(SelectedClip);
            }
            SelectedClip = null;
        }

        private void SetHeadPathAction(object arg)
        {
            if (SelectedMasterHead != null)
            {
                OpenFileDialog dialog = new()
                {
                    Title = "Select Image",
                    Multiselect = false
                };

                if (dialog.ShowDialog() == true)
                {
                    SelectedMasterHead.HeadImagePath = dialog.FileName;
                }
            }
        }

        private void RemoveMasterHeadAction(object arg)
        {
            MessageBoxResult result = MessageBox.Show(
                "Confirm?", "Remove Head", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;

            if (SelectedMasterName != null && SelectedMasterHead != null)
            {
                SelectedMasterName.Heads.Remove(SelectedMasterHead);
                SelectedMasterHead = null;
                UpdateComparisonList();
            }
        }

        private void RemoveMasterNameAction(object arg)
        {
            MessageBoxResult result = MessageBox.Show(
                "Confirm?", "Remove Name", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;

            if (SelectedMasterName != null)
            {
                HeadList.Remove(SelectedMasterName);
                SelectedMasterHead = null;
                SelectedMasterName = null;
                UpdateComparisonList();
            }
        }

        private void AddMasterHeadAction(object arg)
        {
            if (SelectedMasterName != null)
            {
                var result = Interaction.InputBox("Enter Head", "Add Head");
                if (result.Length == 0) return;
                if (SelectedMasterName.Heads.Select(x => x.HeadName).Contains(result)) return;
                SelectedMasterName.Heads.Add(new Head(result));
                UpdateComparisonList();
                SelectedMasterHead = null;
            }
        }

        private void UpdateComparisonList()
        {
            ComparisonList.Clear();

            if (SelectedClip == null) return;

            foreach (Name name in SelectedClip.Names)
            {
                ObservableCollection<Head> missingHeads = new();
                Name headListName = HeadList.FirstOrDefault(x => x.NameName == name.NameName) ?? new Name();
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if (headListName.Heads == null) continue;

                foreach (string head in headListName.Heads.Select(x => x.HeadName))
                {
                    if (!name.Heads.Select(x => x.HeadName).Contains(head) && 
                        !name.Rejected.Contains(head) && 
                        !name.Exported.Contains(head) && 
                        !name.Ready.Contains(head))
                    {
                        missingHeads.Add(new Head(head));
                    }
                }

                ComparisonList.Add(new Name(name.NameName, missingHeads, new ObservableCollection<string>()));
            }
        }

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

                foreach (Clip clip in bin.ClipList)
                {
                    ClipList.Add(clip);
                }

                foreach (Clip clip in bin.ReadyList)
                {
                    ReadyList.Add(clip);
                }

                foreach (Clip clip in bin.ExportList)
                {
                    ExportedList.Add(clip);
                }

                foreach (Name name in bin.HeadList)
                {
                    HeadList.Add(name);
                }
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
            _saveFileName = filename;
            var jsonOpts = new JsonSerializerOptions { WriteIndented = true };
            var bin = new SerialBin(ClipList, ReadyList, ExportedList, HeadList);
            string serialBin = JsonSerializer.Serialize(bin, jsonOpts);

            File.WriteAllText(filename, serialBin);
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

        private void AddMasterNameAction(object arg)
        {
            var result = Interaction.InputBox("Enter Name", "Add Name");
            if (result.Length == 0) return;
            if (HeadList.Select(x => x.NameName).Contains(result)) return;
            HeadList.Add(new Name(result, new ObservableCollection<Head>(), new ObservableCollection<string>()));
            UpdateComparisonList();
            SelectedMasterName = null;
        }

        private void RefreshButtonState()
        {
            LoadCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            LoadCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            SaveAsCommand.RaiseCanExecuteChanged();
            AddMasterNameCommand.RaiseCanExecuteChanged();
            AddMasterHeadCommand.RaiseCanExecuteChanged();
            RemoveMasterHeadCommand.RaiseCanExecuteChanged();
            RemoveMasterNameCommand.RaiseCanExecuteChanged();
            SetHeadPathCommand.RaiseCanExecuteChanged();
            AddClipCommand.RaiseCanExecuteChanged();
            RemoveClipCommand.RaiseCanExecuteChanged();
            SetClipPathCommand.RaiseCanExecuteChanged();
            AddNameCommand.RaiseCanExecuteChanged();
            RemoveNameCommand.RaiseCanExecuteChanged();
            AddHeadCommand.RaiseCanExecuteChanged();
            RejectHeadCommand.RaiseCanExecuteChanged();
            ExportHeadCommand.RaiseCanExecuteChanged();
            RemoveHeadCommand.RaiseCanExecuteChanged();

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
