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
    public string message;
    public int type = 0; //0 is ingredient, 1 is accessory

    // Called when the item is pressed in the inventory
    public virtual void Use()
    {
        if (!isEquipped)
        {
            if (EquipmentUi.instance.slots[itemType].item != null) //already in slot
            {
                return;
            }
            EquipmentUi.instance.Add(this);
            Inventory.instance.Remove(this);
        }
        else if (Inventory.instance.full == false)
        {
            Inventory.instance.Add(this);
            EquipmentUi.instance.Remove(this);
        }

    }

    public string GenerateMessage()
    {
         return message;
    }
    /*
    // Call this method to remove the item from inventory
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }*/

}
