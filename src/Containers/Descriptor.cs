namespace HavenheimManager.Containers;

public class Descriptor : IDescriptor
{
    private string _name;
    private string _description;
    private string _additionalInformation;
    private string _reference;
    private string _reference2;
    private string _reference3;

    public Descriptor(string name, string description, string additionalInformation, string reference, string reference2, string reference3)
    {
        _name = name;
        _description = description;
        _additionalInformation = additionalInformation;
        _reference = reference;
        _reference2 = reference2;
        _reference3 = reference3;
    }

    public Descriptor(string name, string description, string additionalInformation, string reference, string reference2)
    {
        _name = name;
        _description = description;
        _additionalInformation = additionalInformation;
        _reference = reference;
        _reference2 = reference2;
        _reference3 = string.Empty;
    }

    public Descriptor(string name, string description, string additionalInformation, string reference)
    {
        _name = name;
        _description = description;
        _additionalInformation = additionalInformation;
        _reference = reference;
        _reference2 = string.Empty;
        _reference3 = string.Empty;
    }

    public string Name => _name;

    public string Description => _description;

    public string AdditionalInformation => _additionalInformation;

    public string Reference => _reference;

    public string Reference2 => _reference2;

    public string Reference3 => _reference3;
}
