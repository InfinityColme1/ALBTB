using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [Header("Inventory Logic Objects")]
    [SerializeField] private InventoryObject _inventory;
    [SerializeField] private InteractionComponent _interaction;

    [Header("Inventory Settings")]
    [SerializeField] private bool _canCollect = true;


    private void Start()
    {
        _interaction.onCollectable.AddListener(AddCollectable);
    }

    public void AddCurrency(CurrencyType type, int amount)
    {
        _inventory.AddCurrency(type, amount);
    }

    public int GetCurrency(CurrencyType type)
    {
        return _inventory.GetCurrency(type);
    }

    public void AddCollectable(CollectableObject collectable)
    {
        if (_canCollect)
        {
            int amount = collectable.GetAmount();

            // Aquí se puede aumentar la cantidad obtenida

            _inventory.AddCollectable(collectable, amount);
        }
    } 
}
