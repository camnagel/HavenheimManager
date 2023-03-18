using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManager.Containers;

namespace AssetManager.Enums
{
    /// <summary>
    /// Describes the source of an <see cref="ISummary"/>
    /// </summary>
    public enum Source
    {
        [Description("Standard")]
        Standard,
        [Description("Rework")]
        Rework,
        [Description("Homebrew")]
        Homebrew
    }
}
