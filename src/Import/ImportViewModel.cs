using AssetManager.Enums;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;

namespace AssetManager.Import
{
    public class ImportViewModel : INotifyPropertyChanged
    {
        private string _sourcePath;
        public string? SourcePath
        {
            get => _sourcePath;
            set
            {
                _sourcePath = value ?? "";
                OnPropertyChanged("SourcePath");
            }
        }

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
                if (SourcePath is { Length: > 0 } && _selectedSourceType.Length > 0)
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
                SourcePath = dialog.FileName;
            }
        }

        public string GetSourcePath() => SourcePath;

        public SourceType GetSourceType() => _selectedSourceType.StringToSourceType();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
