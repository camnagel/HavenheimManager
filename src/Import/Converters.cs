using AssetManager.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace AssetManager.Import
{
    public class CoreConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string core, IReaderRow row, MemberMapData data) => core.StringToCore();
    }

    public class SourceConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string source, IReaderRow row, MemberMapData data) => source.StringToSource();
    }
}
