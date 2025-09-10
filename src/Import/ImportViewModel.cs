using System.ComponentModel;
using System.Windows;
using HavenheimManager.Extensions;
using Microsoft.Win32;

namespace HavenheimManager.Import;

public class ImportViewModel : INotifyPropertyChanged
{
    private string _selectedSourceType;
    private string _sourcePath;

    public ImportViewModel()
    {
        SaveCommand = new DelegateCommand(SaveAction);
        SelectCommand = new DelegateCommand(SelectAction);
        CancelCommand = new DelegateCommand(CancelAction);
    }

    public string? SourcePath
    {
        get => _sourcePath;
        set
        {
            _sourcePath = value ?? "";
            OnPropertyChanged("SourcePath");
        }
    }

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

    public event PropertyChangedEventHandler? PropertyChanged;

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
        OpenFileDialog dialog = new();
        dialog.Title = "Select CSV File";
        dialog.Multiselect = false;

        if (dialog.ShowDialog() == true)
        {
            SourcePath = dialog.FileName;
        }
    }

    public string GetSourcePath()
    {
        return SourcePath;
    }

    public SourceType GetSourceType()
    {
        return _selectedSourceType.StringToSourceType();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}