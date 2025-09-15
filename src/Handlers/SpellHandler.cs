using HavenheimManager.Containers;
using HavenheimManager.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HavenheimManager.Handlers;

public class SpellHandler : INotifyPropertyChanged
{
    public ObservableCollection<Spell> CurrentSpell { get; set; } = new();

    internal void Clear()
    {
    }

    internal void InitializePathfinder()
    {
    }

    internal void InitializeHavenheim()
    {
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void RefreshButtonState()
    {
    }
}