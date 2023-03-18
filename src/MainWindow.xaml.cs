using AssetManager.Enums;
using System;
using System.Windows;

namespace AssetManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();
            foreach (var item in Enum.GetValues(typeof(Core)))
            {
                vm.CoreTraitFilterList.Add(((Core)item).GetEnumDescription());
            }

            foreach (var item in Enum.GetValues(typeof(Source)))
            {
                vm.SourceTraitFilterList.Add(((Source)item).GetEnumDescription());
            }

            foreach (var item in Enum.GetValues(typeof(Skill)))
            {
                vm.SkillTraitFilterList.Add(((Skill)item).GetEnumDescription());
            }

            foreach (var item in Enum.GetValues(typeof(Combat)))
            {
                vm.CombatTraitFilterList.Add(((Combat)item).GetEnumDescription());
            }

            foreach (var item in Enum.GetValues(typeof(Role)))
            {
                vm.RoleTraitFilterList.Add(((Role)item).GetEnumDescription());
            }

            foreach (var item in Enum.GetValues(typeof(School)))
            {
                vm.SchoolTraitFilterList.Add(((School)item).GetEnumDescription());
            }

            foreach (var item in Enum.GetValues(typeof(Class)))
            {
                vm.ClassTraitFilterList.Add(((Class)item).GetEnumDescription());
            }
        }
    }
}
