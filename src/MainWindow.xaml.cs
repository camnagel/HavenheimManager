using System;
using System.Windows;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;
using Condition = HavenheimManager.Enums.Condition;

namespace HavenheimManager;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();

        foreach (Core item in Enum.GetValues(typeof(Core)))
        {
            vm.CoreTraitFilterList.Add(item.GetEnumDescription());
            vm.CoreFeatFilterList.Add(item.GetEnumDescription());
        }

        foreach (Source item in Enum.GetValues(typeof(Source)))
        {
            vm.SourceTraitFilterList.Add(item.GetEnumDescription());
            vm.SourceFeatFilterList.Add(item.GetEnumDescription());
        }

        foreach (Skill item in Enum.GetValues(typeof(Skill)))
        {
            vm.SkillTraitFilterList.Add(item.GetEnumDescription());
            vm.SkillFeatFilterList.Add(item.GetEnumDescription());
        }

        foreach (Combat item in Enum.GetValues(typeof(Combat)))
        {
            vm.CombatTraitFilterList.Add(item.GetEnumDescription());
            vm.CombatFeatFilterList.Add(item.GetEnumDescription());
        }

        foreach (Role item in Enum.GetValues(typeof(Role)))
        {
            vm.RoleTraitFilterList.Add(item.GetEnumDescription());
            vm.RoleFeatFilterList.Add(item.GetEnumDescription());
        }

        foreach (Magic item in Enum.GetValues(typeof(Magic)))
        {
            vm.MagicTraitFilterList.Add(item.GetEnumDescription());
            vm.MagicFeatFilterList.Add(item.GetEnumDescription());
        }

        foreach (Bonus item in Enum.GetValues(typeof(Bonus)))
        {
            vm.BonusTraitFilterList.Add(item.GetEnumDescription());
            vm.BonusFeatFilterList.Add(item.GetEnumDescription());
        }

        foreach (Condition item in Enum.GetValues(typeof(Condition)))
        {
            vm.ConditionTraitFilterList.Add(item.GetEnumDescription());
            vm.ConditionFeatFilterList.Add(item.GetEnumDescription());
        }

        foreach (Class item in Enum.GetValues(typeof(Class)))
        {
            vm.ClassTraitFilterList.Add(item.GetEnumDescription());
            vm.ClassFeatFilterList.Add(item.GetEnumDescription());
        }

        foreach (Tool item in Enum.GetValues(typeof(Tool)))
        {
            vm.CraftingToolSelectionList.Add(item.GetEnumDescription());
        }

        foreach (Workshop item in Enum.GetValues(typeof(Workshop)))
        {
            vm.CraftingWorkshopSelectionList.Add(item.GetEnumDescription());
        }
    }
}