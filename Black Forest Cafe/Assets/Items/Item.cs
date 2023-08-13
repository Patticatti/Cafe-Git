using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public bool showInInventory = true;
    public int itemType;
    public bool isEquipped = false;
    public bool generateAsRandom = true;

    // Called when the item is pressed in the inventory
    public virtual void Use()
    {
        if (!isEquipped)
        {
            if (EquipmentUi.instance.slots[itemType].item != null) //already in slot
            {
                Debug.Log("not enough room");
                return;
            }
            EquipmentUi.instance.Add(this);
            Inventory.instance.Remove(this);
        }
        else
        {
            Inventory.instance.Add(this);
            EquipmentUi.instance.Remove(this);
        }
    }
    /*
    // Call this method to remove the item from inventory
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }*/

}
