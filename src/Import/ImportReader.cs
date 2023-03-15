using System.Globalization;
using System.IO;
using System.Linq;
using AssetManager.Containers;
using CsvHelper;

namespace AssetManager.Import
{
    internal class ImportReader
    {
        internal static void ReadTraitCsv(string filePath)
        {
            using (var stream = new StreamReader(filePath))
            using (var reader = new CsvReader(stream, CultureInfo.InvariantCulture))
            {
                reader.Context.RegisterClassMap<TraitMap>();
                var traits = reader.GetRecords<Trait>().ToList();
                int a = 0;
            }
        }

        internal static void ReadFeatCsv(string filePath)
        {
            using (var stream = new StreamReader(filePath))
            using (var reader = new CsvReader(stream, CultureInfo.InvariantCulture))
            {
            }
        }

        internal static void ReadItemCsv(string filePath)
        {
            using (var stream = new StreamReader(filePath))
            using (var reader = new CsvReader(stream, CultureInfo.InvariantCulture))
            {
            }
        }

        internal static void ReadspellCsv(string filePath)
        {
            using (var stream = new StreamReader(filePath))
            using (var reader = new CsvReader(stream, CultureInfo.InvariantCulture))
            {
            }
        }
    }
}
