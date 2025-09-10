using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using HavenheimManager.Containers;

namespace HavenheimManager.Import;

internal class ImportReader
{
    internal static IEnumerable<Trait> ReadTraitCsv(string filePath)
    {
        using (StreamReader stream = new(filePath))
        using (CsvReader reader = new(stream, CultureInfo.InvariantCulture))
        {
            reader.Context.RegisterClassMap<TraitMap>();
            foreach (Trait trait in reader.GetRecords<Trait>())
            {
                if (trait.Name.Length > 0)
                {
                    yield return trait;
                }
            }
        }
    }

    internal static IEnumerable<Feat> ReadFeatCsv(string filePath)
    {
        using (StreamReader stream = new(filePath))
        using (CsvReader reader = new(stream, CultureInfo.InvariantCulture))
        {
            reader.Context.RegisterClassMap<FeatMap>();
            foreach (Feat feat in reader.GetRecords<Feat>())
            {
                if (feat.Name.Length > 0)
                {
                    yield return feat;
                }
            }
        }
    }

    internal static IEnumerable<Item> ReadItemCsv(string filePath)
    {
        using (StreamReader stream = new(filePath))
        using (CsvReader reader = new(stream, CultureInfo.InvariantCulture))
        {
            yield break;
        }
    }

    internal static IEnumerable<Spell> ReadSpellCsv(string filePath)
    {
        using (StreamReader stream = new(filePath))
        using (CsvReader reader = new(stream, CultureInfo.InvariantCulture))
        {
            yield break;
        }
    }
}