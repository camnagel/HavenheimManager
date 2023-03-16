using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AssetManager.Enums;

namespace AssetManager.Import
{
    public class ImportViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> SourcePathList { get; } = new();

        private string _selectedSourceType;
        public string? SelectedSourceType
        {
            get => _selectedSourceType;
            set
            {
                _selectedSourceType = value ?? "";
                OnPropertyChanged("SelectedSourceType");
            }
        }

        public DelegateCommand SelectCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public ImportViewModel()
        {
            SaveCommand = new DelegateCommand(SaveAction);
            SelectCommand = new DelegateCommand(SelectAction);
            CancelCommand = new DelegateCommand(CancelAction);
        }

        private void SaveAction(object arg)
        {
            if (arg is Window window)
            {
                if (SourcePathList.Any() && _selectedSourceType.Length > 0)
                {
                    window.DialogResult = true;
                }

                window.Close();
            }
        }

        private void CancelAction(object arg)
        {
            if (arg is Window window)
            {
                window.DialogResult = false;
                window.Close();
            }
        }

        private void SelectAction(object arg)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select CSV File";
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == true)
            {
                SourcePathList.Clear();
                SourcePathList.Add(dialog.FileName);
            }
        }

        public string GetSourcePath() => SourcePathList.First();

        public SourceType GetSourceType() => _selectedSourceType.StringToSourceType();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
