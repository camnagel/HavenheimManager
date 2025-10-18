using HavenheimManager.Editors;

namespace HavenheimManager.Containers;

public class DescWrapper
{
    private string _descriptor;

    internal DescWrapper(string descriptor)
    {
        _descriptor = descriptor;
    }

    public string Descriptor
    {
        get => _descriptor;
        set
        {
            _descriptor = value;
        }
    }
}