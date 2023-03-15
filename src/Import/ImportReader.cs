using System.Globalization;
using System.IO;
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
