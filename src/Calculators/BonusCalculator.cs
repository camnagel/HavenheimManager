using System;
using System.Collections.Generic;
using System.Linq;
using AssetManager.Enums;
using AssetManager.Extensions;

namespace AssetManager.Calculators;

internal class BonusCalculator
{
    internal readonly Dictionary<string, int> AlchemicalBonusList = new();
    internal readonly Dictionary<string, int> ArmorBonusList = new();
    internal readonly Dictionary<string, int> BabBonusList = new();
    internal readonly Dictionary<string, KeyValuePair<string, int>> CircumstanceBonusList = new();
    internal readonly Dictionary<string, int> CompetenceBonusList = new();
    internal readonly Dictionary<string, int> DeflectionBonusList = new();
    internal readonly Dictionary<string, int> DodgeBonusList = new();
    internal readonly Dictionary<string, int> EnhanceBonusList = new();
    internal readonly Dictionary<string, int> InherentBonusList = new();
    internal readonly Dictionary<string, int> InsightBonusList = new();
    internal readonly Dictionary<string, int> LuckBonusList = new();
    internal readonly Dictionary<string, int> MoraleBonusList = new();
    internal readonly Dictionary<string, KeyValuePair<string, int>> NatArmorBonusList = new();
    internal readonly Dictionary<string, int> ProfaneBonusList = new();
    internal readonly Dictionary<string, int> RacialBonusList = new();
    internal readonly Dictionary<string, int> ResistanceBonusList = new();
    internal readonly Dictionary<string, int> SacredBonusList = new();
    internal readonly Dictionary<string, int> ShieldBonusList = new();
    internal readonly Dictionary<string, int> SizeBonusList = new();
    internal readonly Dictionary<string, int> TraitBonusList = new();
    internal readonly Dictionary<string, int> UntypedBonusList = new();
    
    internal int CurrentBonus => CalculateBonuses();

    internal void AddBonus(Bonus bonusType, string bonusSource, int bonusValue)
    {
        switch (bonusType)
        {
            case Bonus.Alchemical:
                AlchemicalBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Armor:
                ArmorBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Bab:
                BabBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Competence:
                CompetenceBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Deflection:
                DeflectionBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Dodge:
                DodgeBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Enhancement:
                EnhanceBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Inherent:
                InherentBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Insight:
                InsightBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Luck:
                LuckBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Morale:
                MoraleBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Profane:
                ProfaneBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Racial:
                RacialBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Resistance:
                ResistanceBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Sacred:
                SacredBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Shield:
                ShieldBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Size:
                SizeBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Trait:
                TraitBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Untyped:
                UntypedBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.None:
            case Bonus.Circumstance:
            case Bonus.NaturalArmor:
            default:
                throw new ArgumentOutOfRangeException(nameof(bonusType), "Cannot add requested bonus type");
        }
    }

    internal void AddCircumstanceBonus(string bonusSource, string circumstance, int bonusValue)
    {
        CircumstanceBonusList[bonusSource] = new KeyValuePair<string, int>(circumstance, bonusValue);
    }

    internal void AddNaturalArmorBonus(string bonusSource, string circumstance, int bonusValue)
    {
        NatArmorBonusList[bonusSource] = new KeyValuePair<string, int>(circumstance, bonusValue);
    }

    internal void Clear()
    {
        AlchemicalBonusList.Clear();
        ArmorBonusList.Clear();
        BabBonusList.Clear();
        CompetenceBonusList.Clear();
        DeflectionBonusList.Clear();
        DodgeBonusList.Clear();
        EnhanceBonusList.Clear();
        InherentBonusList.Clear();
        InsightBonusList.Clear();
        LuckBonusList.Clear();
        MoraleBonusList.Clear();
        ProfaneBonusList.Clear();
        RacialBonusList.Clear();
        ResistanceBonusList.Clear();
        SacredBonusList.Clear();
        ShieldBonusList.Clear();
        SizeBonusList.Clear();
        TraitBonusList.Clear();
        UntypedBonusList.Clear();
        CircumstanceBonusList.Clear();
        NatArmorBonusList.Clear();
    }

    internal void RemoveBonus(Bonus bonusType, string bonusSource)
    {
        switch (bonusType)
        {
            case Bonus.Alchemical:
                AlchemicalBonusList.Remove(bonusSource);
                return;
            case Bonus.Armor:
                ArmorBonusList.Remove(bonusSource);
                return;
            case Bonus.Bab:
                BabBonusList.Remove(bonusSource);
                return;
            case Bonus.Competence:
                CompetenceBonusList.Remove(bonusSource);
                return;
            case Bonus.Deflection:
                DeflectionBonusList.Remove(bonusSource);
                return;
            case Bonus.Dodge:
                DodgeBonusList.Remove(bonusSource);
                return;
            case Bonus.Enhancement:
                EnhanceBonusList.Remove(bonusSource);
                return;
            case Bonus.Inherent:
                InherentBonusList.Remove(bonusSource);
                return;
            case Bonus.Insight:
                InsightBonusList.Remove(bonusSource);
                return;
            case Bonus.Luck:
                LuckBonusList.Remove(bonusSource);
                return;
            case Bonus.Morale:
                MoraleBonusList.Remove(bonusSource);
                return;
            case Bonus.Profane:
                ProfaneBonusList.Remove(bonusSource);
                return;
            case Bonus.Racial:
                RacialBonusList.Remove(bonusSource);
                return;
            case Bonus.Resistance:
                ResistanceBonusList.Remove(bonusSource);
                return;
            case Bonus.Sacred:
                SacredBonusList.Remove(bonusSource);
                return;
            case Bonus.Shield:
                ShieldBonusList.Remove(bonusSource);
                return;
            case Bonus.Size:
                SizeBonusList.Remove(bonusSource);
                return;
            case Bonus.Trait:
                TraitBonusList.Remove(bonusSource);
                return;
            case Bonus.Untyped:
                UntypedBonusList.Remove(bonusSource);
                return;
            case Bonus.Circumstance:
                CircumstanceBonusList.Remove(bonusSource);
                return;
            case Bonus.NaturalArmor:
                NatArmorBonusList.Remove(bonusSource);
                return;
            case Bonus.None:
            default:
                throw new ArgumentOutOfRangeException(nameof(bonusType), "Cannot add requested bonus type");
        }
    }

    private int CalculateBonuses()
    {
        int totalBonus = CalculateSimpleBonuses();
        totalBonus += UntypedBonusList.Values.Sum();
        totalBonus += DodgeBonusList.Values.Sum();

        totalBonus += CalculateComplexBonuses(CircumstanceBonusList);
        totalBonus += CalculateComplexBonuses(NatArmorBonusList);

        return totalBonus;
    }

    private int CalculateComplexBonuses(Dictionary<string, KeyValuePair<string, int>> bonusList)
    {
        // Filter bonuses to eliminate duplicates
        Dictionary<string, int> filteredBonuses = new();
        foreach (KeyValuePair<string, int> bonus in bonusList.Values)
        {
            // Handle multiple bonuses from same circumstance
            if (filteredBonuses.ContainsKey(bonus.Key))
            {
                if (filteredBonuses[bonus.Key] < bonus.Value)
                {
                    filteredBonuses[bonus.Key] = bonus.Value;
                }
            }

            // Just add it
            else
            {
                filteredBonuses.Add(bonus.Key, bonus.Value);
            }
        }

        return filteredBonuses.Values.Sum();
    }

    private int CalculateSimpleBonuses()
    {
        int totalBonus = 0;
        totalBonus += AlchemicalBonusList.DefaultMax();
        totalBonus += ArmorBonusList.DefaultMax();
        totalBonus += BabBonusList.DefaultMax();
        totalBonus += CompetenceBonusList.DefaultMax();
        totalBonus += DeflectionBonusList.DefaultMax();
        totalBonus += EnhanceBonusList.DefaultMax();
        totalBonus += InherentBonusList.DefaultMax();
        totalBonus += InsightBonusList.DefaultMax();
        totalBonus += MoraleBonusList.DefaultMax();
        totalBonus += LuckBonusList.DefaultMax();
        totalBonus += ProfaneBonusList.DefaultMax();
        totalBonus += RacialBonusList.DefaultMax();
        totalBonus += ResistanceBonusList.DefaultMax();
        totalBonus += SacredBonusList.DefaultMax();
        totalBonus += ShieldBonusList.DefaultMax();
        totalBonus += SizeBonusList.DefaultMax();
        totalBonus += TraitBonusList.DefaultMax();

        return totalBonus;
    }
}