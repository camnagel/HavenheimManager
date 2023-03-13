using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace AssetManager.Containers
{
    public class Clip
    {
        public Clip(string clip, ObservableCollection<Name> nameList)
        {
            ClipName = clip;
            Names = nameList;
        }

        public ObservableCollection<Name> Names { get; set; }

        public string ClipName { get; set; }

        public string ClipImagePath { get; set; }

        [JsonConstructor]
        public Clip() {}
    }
}
