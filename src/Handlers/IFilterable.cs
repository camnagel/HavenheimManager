using HavenheimManager.Descriptors;
using System.ComponentModel;

namespace HavenheimManager.Handlers;

public interface IFilterable : INotifyPropertyChanged
{
    DescriptorSettings DescriptorSettings { get; }

    FilterHandler FilterHandler { get; }

    void ApplyFilters();
}