namespace HavenheimManager.Containers;

public class BonusTsv
{
    internal BonusTsv(string type, string source, int value)
    {
        Type = type;
        Source = source;
        Value = value;
    }

    public string Type { get; set; }

    public string Source { get; set; }

    public int Value { get; set; }
}