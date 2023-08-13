using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI; //UI
    public GameObject itemsParent;

    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        InventorySlot[] slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
