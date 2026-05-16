using UnityEngine;

[CreateAssetMenu(fileName = "CollectableObject", menuName = "Scriptable Objects/CollectableObject")]
public class CollectableObject : ScriptableObject
{
    public enum CollectableType { BRANCHES, PERFECT, FLOWER, JUNK, TOOL }

    public string objectName;
    public Texture2D icon;
    public CollectableType type;
    [SerializeField] private int amount;

    public int GetAmount() => amount;

    //Aqui se puede aumentar la cantidad base de lo recogido

}
