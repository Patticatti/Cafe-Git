using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{
    public GameObject itemDrop;
    public ItemDrop itemDropComponent;
    public Image icon;
    public Transform playerPosition;
    public Button removeButton;

    public Item item;  // Current item in the slot

    public void Awake()
    {
        itemDropComponent = itemDrop.GetComponent<ItemDrop>();
        removeButton.interactable = false;
    }

    // Add item to the slot
    public void AddItem(Item newItem)
    {
        item = null;
        item = newItem;
        itemDropComponent.itemCopy = item; //modifying 
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void RemoveItem()
    {
        itemDropComponent.itemCopy = item;
        GameObject newItem = Instantiate(itemDrop, Inventory.instance.playerPosition.position, Quaternion.identity);
        ClearSlot();
        RemoveItemFromInventory();
        Inventory.instance.onItemChangedCallback.Invoke();
    }
    // Clear the slot
    public void ClearSlot()
    {
        if (item != null)
        {
            itemDropComponent.itemCopy = null;
            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
        }

    }

    // If the remove button is pressed, this function will be called.
    public void RemoveItemFromInventory()
    {
        Inventory.instance.Remove(item);
        item = null;
    }

    // Use the item
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    private void OnApplicationQuit()
    {
        item = null;
        itemDropComponent.itemCopy = null;
    }


}