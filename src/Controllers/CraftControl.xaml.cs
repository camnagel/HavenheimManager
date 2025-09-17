using System;
using System.Windows.Controls;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;

namespace HavenheimManager.Controllers;

/// <summary>
///     Interaction logic for CraftControl.xaml
/// </summary>
public partial class CraftControl : UserControl
{
    public CraftControl()
    {
        InitializeComponent();

        CraftItemSourceBox.Items.Add("Select Source");
        foreach (Source item in Enum.GetValues(typeof(Source)))
        {
            CraftItemSourceBox.Items.Add(item.GetEnumDescription());
        }
    }
}