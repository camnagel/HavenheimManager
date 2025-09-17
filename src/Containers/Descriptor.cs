namespace HavenheimManager.Containers;

public class Descriptor : IDescriptor
{
    public Descriptor(string name, string description, string additionalInformation, string reference,
        string reference2, string reference3)
    {
        Name = name;
        Description = description;
        AdditionalInformation = additionalInformation;
        Reference = reference;
        Reference2 = reference2;
        Reference3 = reference3;
    }

    public Descriptor(string name, string description, string additionalInformation, string reference,
        string reference2)
    {
        Name = name;
        Description = description;
        AdditionalInformation = additionalInformation;
        Reference = reference;
        Reference2 = reference2;
        Reference3 = string.Empty;
    }

    public Descriptor(string name, string description, string additionalInformation, string reference)
    {
        Name = name;
        Description = description;
        AdditionalInformation = additionalInformation;
        Reference = reference;
        Reference2 = string.Empty;
        Reference3 = string.Empty;
    }

    public string Name { get; }

    public string Description { get; }

    public string AdditionalInformation { get; }

    public string Reference { get; }

    public string Reference2 { get; }

    public string Reference3 { get; }
}