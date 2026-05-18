using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [Header("Inventory Logic Objects")]
    [SerializeField] private InventoryObject _inventory;
    [SerializeField] private InteractionComponent _interaction;

    [Header("Inventory Settings")]
    [SerializeField] private bool _canCollect = true;
    [SerializeField] private bool _canCollectTools = true;


    private void Start()
    {
        _inventory.setCanCollectTools(_canCollectTools);
        _interaction.onCollectable.AddListener(AddCollectable);
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
