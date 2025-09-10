using HavenheimManager.Editors;

namespace HavenheimManager.Containers;

public class BonusScv
{
    private readonly BonusCalcViewModel _vm;
    private string _circumstance;
    private string _source;

    private int _value;

    internal BonusScv(BonusCalcViewModel vm, string source, string circumstance, int value)
    {
        _vm = vm;
        _source = source;
        _circumstance = circumstance;
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
    
    public string Circumstance
    {
        get => _circumstance;
        set
        {
            _circumstance = value;
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