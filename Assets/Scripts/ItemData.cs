using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string itemName;
    public string itemID;
    public Sprite icon;
    public int maxStack = 1;
    public EquipmentType equipmentType;
}


