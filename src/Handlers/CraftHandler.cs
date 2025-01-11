using AssetManager.Enums;

namespace AssetManager.Handlers;

public class CraftHandler
{
    private readonly MainWindowViewModel _vm;

    private string _itemName;

    public CraftHandler(MainWindowViewModel vm)
    {
        _vm = vm;
    }

    internal void SetItemName(string itemName)
    {
        _itemName = itemName;
    }

    internal void CraftToolAction(object arg)
    {
        if (arg is string filter)
        {
            Core toggleCore = filter.StringToCore();

        }
    }
}