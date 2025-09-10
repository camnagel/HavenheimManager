using HavenheimManager.Editors;

namespace HavenheimManager.Containers;

public class BonusSvp
{
    private readonly BonusCalcViewModel _vm;
    private string _source;

    private int _value;

    internal BonusSvp(BonusCalcViewModel vm, string source, int value)
    {
        _vm = vm;
        _source = source;
        _value = value;
    }

    public string Source
    {
        get => _source;
        set
        {
            _source = value;
            _vm.UpdateActiveBonuses();
        }
    }

    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            _vm.UpdateActiveBonuses();
        }
    }
}