using System.Text.Json.Serialization;

namespace AssetManager.Containers
{
    public class Head
    {
        public Head(string head)
        {
            HeadName = head;
        }

        public string HeadName { get; set; }

        public string HeadImagePath { get; set; } = "";

        [JsonConstructor]
        public Head() { }
    }
}
