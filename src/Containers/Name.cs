using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace AssetManager.Containers
{
    public class Name
    {
        public Name(string name, ObservableCollection<Head> headList, ObservableCollection<string> rejected)
        {
            NameName = name;
            Heads = headList;
            Rejected = rejected;
        }

        public ObservableCollection<Head> Heads { get; set; }

        public ObservableCollection<string> Rejected { get; set; }

        public ObservableCollection<string> Exported { get; set; } = new();

        public ObservableCollection<string> Ready { get; set; } = new();

        public string NameName { get; set; }

        [JsonConstructor]
        public Name() {}
    }
}
