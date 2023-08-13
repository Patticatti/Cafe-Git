using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{
    public GameObject itemDrop;
    public Image icon;
    public Transform playerPosition;
    public Button removeButton;

    public Item item;  // Current item in the slot

    public void Awake()
    {
        //itemDropScript = itemDrop.GetComponent<ItemDrop>();
        removeButton.interactable = false;
    }

    // Add item to the slot
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void RemoveItem()
    {
        //itemDropScript.itemCopy.generateAsRandom = false;
        GameObject newItem = Instantiate(itemDrop, Inventory.instance.playerPosition.position, Quaternion.identity);
        //newItem.GetComponent<ItemDrop>().itemCopy.generateAsRandom = false;
        //newItem.GetComponent<ItemDrop>().UpdateSprite();
        RemoveItemFromInventory();
        ClearSlot();
    }
    // Clear the slot
    public void ClearSlot()
    {
        if (item != null)
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
        }

    }

    // If the remove button is pressed, this function will be called.
    public void RemoveItemFromInventory()
    {
        Inventory.instance.Remove(item);
    }

    // Use the item
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

}