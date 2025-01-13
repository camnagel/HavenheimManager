using System;
using System.Collections.Generic;
using System.Linq;
using AssetManager.Enums;
using AssetManager.Extensions;

namespace AssetManager.Calculators;

internal class BonusCalculator
{
    private readonly Dictionary<string, int> _alchemicalBonusList = new();
    private readonly Dictionary<string, int> _armorBonusList = new();
    private readonly Dictionary<string, int> _babBonusList = new();
    private readonly Dictionary<string, KeyValuePair<string, int>> _circumstanceBonusList = new();
    private readonly Dictionary<string, int> _competenceBonusList = new();
    private readonly Dictionary<string, int> _deflectionBonusList = new();
    private readonly Dictionary<string, int> _dodgeBonusList = new();
    private readonly Dictionary<string, int> _enhanceBonusList = new();
    private readonly Dictionary<string, int> _inherentBonusList = new();
    private readonly Dictionary<string, int> _insightBonusList = new();
    private readonly Dictionary<string, int> _luckBonusList = new();
    private readonly Dictionary<string, int> _moraleBonusList = new();
    private readonly Dictionary<string, KeyValuePair<string, int>> _natArmorBonusList = new();
    private readonly Dictionary<string, int> _profaneBonusList = new();
    private readonly Dictionary<string, int> _racialBonusList = new();
    private readonly Dictionary<string, int> _resistanceBonusList = new();
    private readonly Dictionary<string, int> _sacredBonusList = new();
    private readonly Dictionary<string, int> _shieldBonusList = new();
    private readonly Dictionary<string, int> _sizeBonusList = new();
    private readonly Dictionary<string, int> _traitBonusList = new();
    private readonly Dictionary<string, int> _untypedBonusList = new();

    internal int CurrentBonus => CalculateBonuses();

    internal void AddBonus(Bonus bonusType, string bonusSource, int bonusValue)
    {
        switch (bonusType)
        {
            case Bonus.Alchemical:
                _alchemicalBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Armor:
                _armorBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Bab:
                _babBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Competence:
                _competenceBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Deflection:
                _deflectionBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Dodge:
                _dodgeBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Enhancement:
                _enhanceBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Inherent:
                _inherentBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Insight:
                _insightBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Luck:
                _luckBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Morale:
                _moraleBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Profane:
                _profaneBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Racial:
                _racialBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Resistance:
                _resistanceBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Sacred:
                _sacredBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Shield:
                _shieldBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Size:
                _sizeBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Trait:
                _traitBonusList[bonusSource] = bonusValue;
                return;
            case Bonus.Untyped:
                _untypedBonusList[bonusSource] = bonusValue;
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
        _circumstanceBonusList[bonusSource] = new KeyValuePair<string, int>(circumstance, bonusValue);
    }

    internal void AddNaturalArmorBonus(string bonusSource, string circumstance, int bonusValue)
    {
        _natArmorBonusList[bonusSource] = new KeyValuePair<string, int>(circumstance, bonusValue);
    }

    internal void RemoveBonus(Bonus bonusType, string bonusSource)
    {
        switch (bonusType)
        {
            case Bonus.Alchemical:
                _alchemicalBonusList.Remove(bonusSource);
                return;
            case Bonus.Armor:
                _armorBonusList.Remove(bonusSource);
                return;
            case Bonus.Bab:
                _babBonusList.Remove(bonusSource);
                return;
            case Bonus.Competence:
                _competenceBonusList.Remove(bonusSource);
                return;
            case Bonus.Deflection:
                _deflectionBonusList.Remove(bonusSource);
                return;
            case Bonus.Dodge:
                _dodgeBonusList.Remove(bonusSource);
                return;
            case Bonus.Enhancement:
                _enhanceBonusList.Remove(bonusSource);
                return;
            case Bonus.Inherent:
                _inherentBonusList.Remove(bonusSource);
                return;
            case Bonus.Insight:
                _insightBonusList.Remove(bonusSource);
                return;
            case Bonus.Luck:
                _luckBonusList.Remove(bonusSource);
                return;
            case Bonus.Morale:
                _moraleBonusList.Remove(bonusSource);
                return;
            case Bonus.Profane:
                _profaneBonusList.Remove(bonusSource);
                return;
            case Bonus.Racial:
                _racialBonusList.Remove(bonusSource);
                return;
            case Bonus.Resistance:
                _resistanceBonusList.Remove(bonusSource);
                return;
            case Bonus.Sacred:
                _sacredBonusList.Remove(bonusSource);
                return;
            case Bonus.Shield:
                _shieldBonusList.Remove(bonusSource);
                return;
            case Bonus.Size:
                _sizeBonusList.Remove(bonusSource);
                return;
            case Bonus.Trait:
                _traitBonusList.Remove(bonusSource);
                return;
            case Bonus.Untyped:
                _untypedBonusList.Remove(bonusSource);
                return;
            case Bonus.Circumstance:
                _circumstanceBonusList.Remove(bonusSource);
                return;
            case Bonus.NaturalArmor:
                _natArmorBonusList.Remove(bonusSource);
                return;
            case Bonus.None:
            default:
                throw new ArgumentOutOfRangeException(nameof(bonusType), "Cannot add requested bonus type");
        }
    }

    private int CalculateBonuses()
    {
        int totalBonus = CalculateSimpleBonuses();
        totalBonus += _untypedBonusList.Values.Sum();
        totalBonus += _dodgeBonusList.Values.Sum();

        totalBonus += CalculateComplexBonuses(_circumstanceBonusList);
        totalBonus += CalculateComplexBonuses(_natArmorBonusList);

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
        totalBonus += _alchemicalBonusList.DefaultMax();
        totalBonus += _armorBonusList.DefaultMax();
        totalBonus += _babBonusList.DefaultMax();
        totalBonus += _competenceBonusList.DefaultMax();
        totalBonus += _deflectionBonusList.DefaultMax();
        totalBonus += _enhanceBonusList.DefaultMax();
        totalBonus += _inherentBonusList.DefaultMax();
        totalBonus += _insightBonusList.DefaultMax();
        totalBonus += _moraleBonusList.DefaultMax();
        totalBonus += _luckBonusList.DefaultMax();
        totalBonus += _profaneBonusList.DefaultMax();
        totalBonus += _racialBonusList.DefaultMax();
        totalBonus += _resistanceBonusList.DefaultMax();
        totalBonus += _sacredBonusList.DefaultMax();
        totalBonus += _shieldBonusList.DefaultMax();
        totalBonus += _sizeBonusList.DefaultMax();
        totalBonus += _traitBonusList.DefaultMax();

        return totalBonus;
    }
}