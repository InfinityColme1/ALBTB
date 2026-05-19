using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventoryObject", menuName = "Scriptable Objects/PlayerInventoryObject")]
public class PlayerInventoryObject : InventoryObject
{
    [Header("Tool Inventory")]
    public List<ToolAbstractObject> toolList;

    protected override void AddOther(CollectableObject collectable)
    {
        ToolAbstractObject newTool = collectable.toolInformation;
        if (newTool) toolList.Add(newTool);
    }
}
