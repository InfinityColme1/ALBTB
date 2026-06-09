using UnityEngine;

[CreateAssetMenu(fileName = "CollectableObject", menuName = "Scriptable Objects/CollectableObject")]
public class CollectableObject : ScriptableObject
{
    public enum CollectableType { BRANCHES, PERFECT, FLOWER, JUNK, OTHER }

    public string objectName;
    public Texture2D icon;
    public CollectableType type;
    [SerializeField] private int amount;

    [Header("Other Type's information")]
    // Aquí se puede poner la info de otro tipo de objetos recogibles.
    public ToolBehaviour toolInformation;

    public int GetAmount() => amount;

    //Aqui se puede aumentar la cantidad base de lo recogido

}
