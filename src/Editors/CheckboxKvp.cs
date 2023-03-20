namespace AssetManager.Editors
{
    public class CheckboxKvp
    {
        public string Key { get; set; }

        public bool Value { get; set; }

        internal CheckboxKvp(string key, bool value)
        {
            Key = key;
            Value = value;
        }
    }
}
