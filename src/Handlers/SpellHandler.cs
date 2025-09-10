using System.Collections.ObjectModel;
using System.ComponentModel;
using HavenheimManager.Containers;

namespace HavenheimManager.Handlers;

public class SpellHandler : INotifyPropertyChanged
{
    public ObservableCollection<Spell> CurrentSpell { get; set; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void RefreshButtonState()
    {
    }
}