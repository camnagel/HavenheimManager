namespace AssetManager.Containers;

public class BonusSvp
{
    internal BonusSvp(string source, int value)
    {
        Source = source;
        Value = value;
    }

    public string Source { get; set; }
    public int Value { get; set; }
}