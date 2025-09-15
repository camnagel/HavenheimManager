using System.ComponentModel;

namespace HavenheimManager.Descriptors;

internal enum DescSkill
{
    [Description("Acrobatics")] Acrobatics,
    [Description("Bluff")] Bluff,
    [Description("Climb")] Climb,
    [Description("Craft")] Craft,
    [Description("Diplomacy")] Diplomacy,
    [Description("Disable Device")] DisableDevice,
    [Description("Disguise")] Disguise,
    [Description("Fly")] Fly,
    [Description("Handle Creature")] HandleCreature,
    [Description("Heal")] Heal,
    [Description("Intimidate")] Intimidate,
    [Description("Knowledge")] Knowledge,
    [Description("Linguistics")] Linguistics,
    [Description("Perception")] Perception,
    [Description("Perform")] Perform,
    [Description("Profession")] Profession,
    [Description("Ride")] Ride,
    [Description("Sense Motive")] SenseMotive,
    [Description("Sleight of Hand")] SleightOfHand,
    [Description("Stealth")] Stealth,
    [Description("Swim")] Swim,
    [Description("UMD")] Umd,
    [Description("All")] All,
    [Description("Signature Skill")] Signature,
    
    [Description("Spellcraft")] Spellcraft,
    [Description("Appraise")] Appraise,
    [Description("Survival")] Survival,
    [Description("Escape Artist")] EscapeArtist,
}