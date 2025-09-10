namespace HavenheimManager.Containers;

public class CheckboxKvp
{
    internal CheckboxKvp(string key, bool value)
    {
        Key = key;
        Value = value;
    }

    public string Key { get; set; }

    public bool Value { get; set; }
}