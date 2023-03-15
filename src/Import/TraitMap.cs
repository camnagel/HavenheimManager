using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManager.Containers;
using AssetManager.Enums;
using CsvHelper.Configuration;

namespace AssetManager.Import
{
    public class TraitMap : ClassMap<Trait>
    {
        public TraitMap()
        {
            Map(x => x.Name);
            Map(x => x.Prereqs);
            Map(x => x.Effects);
            Map(x => x.Description);
            Map(x => x.Url);
            Map(x => x.Source).TypeConverter<SourceConverter<Source>>();
        }
    }
}
