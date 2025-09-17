using CsvHelper.Configuration;
using HavenheimManager.Containers;

namespace AssetManager.Import;

public class DescriptorMap : ClassMap<Descriptor>
{
    public DescriptorMap()
    {
        Map(x => x.Name).Index(0);
        Map(x => x.Description).Index(1);
        Map(x => x.AdditionalInformation).Index(2);
        Map(x => x.Reference).Index(3);
        Map(x => x.Reference2).Index(4);
        Map(x => x.Reference3).Index(5);
    }
}