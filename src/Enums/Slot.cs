using System.ComponentModel;

namespace AssetManager.Enums;

public enum Slot
{
    [Description("Head")] Head,
    [Description("Headband")] Headband,
    [Description("Eyes")] Eyes,
    [Description("Shoulders")] Shoulders,
    [Description("Neck")] Neck,
    [Description("Chest")] Chest,
    [Description("Body")] Body,
    [Description("Armor")] Armor,
    [Description("Belt")] Belt,
    [Description("Wrists")] Wrists,
    [Description("Hands")] Hands,
    [Description("Ring")] Ring,
    [Description("Feet")] Feet,
    [Description("Slotless")] Slotless
}