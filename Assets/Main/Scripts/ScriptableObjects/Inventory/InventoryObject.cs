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

    [Header("Tool Inventory")]
    public GameObject machete;
    public GameObject axe;
    public GameObject chainsaw;


    private bool _canCollectTools;

    public void setCanCollectTools(bool newState) => _canCollectTools = newState;

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
                AddTool(collectable);
                break;
        }
    }

    private void AddTool(CollectableObject collectable)
    {

    }
}
