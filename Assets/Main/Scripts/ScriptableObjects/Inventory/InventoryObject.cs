using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "Scriptable Objects/InventoryObject")]
public class InventoryObject : ScriptableObject
{
    [Header("Currency Inventory")]
    public int favorCoins;
    public int pactCoins;
    public int soulCoins;

    [Header("Resources Inventory")]
    public int branches;
    public int perfectBranches;
    public int cursedFlowers;
    public int junk;

    public void AddCollectable(CollectableObject collectable, int amount)
    {
        switch(collectable.type)
        {
            case CollectableObject.CollectableType.BRANCHES:
                branches += amount;
                Debug.Log("Added " + amount + " branches. Total: " + branches);
                break;
            case CollectableObject.CollectableType.PERFECT:
                perfectBranches += amount;
                Debug.Log("Added " + amount + " perfect branches. Total: " + perfectBranches);
                break;
            case CollectableObject.CollectableType.FLOWER:
                cursedFlowers += amount;
                break;
            case CollectableObject.CollectableType.JUNK:
                junk += amount;
                break;
            default:
                AddOther(collectable);
                break;
        }
    }

    protected virtual void AddOther(CollectableObject collectable) 
    {
        // Esta función no la deben implementar clases hijos de esta
        // Sirve para gestionar la recolección única de recursos ligados a un personaje en particular
        // E.J Solo el jugador puede recoger herramientas
    }
}
