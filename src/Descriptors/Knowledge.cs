using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum Knowledge
{
    [Description("Arcana")] Arcana,
    [Description("Dungeoneering")] Dungeoneering,
    [Description("Engineering")] Engineering,
    [Description("Geography")] Geography,
    [Description("History")] History,
    [Description("Local")] Local,
    [Description("Nature")] Nature,
    [Description("Occult")] Occult,
    [Description("Planes")] Planes,
    [Description("Religion")] Religion
}