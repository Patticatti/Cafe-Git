using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUi : MonoBehaviour
{
    #region Singleton
    public static EquipmentUi instance;

    void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject equipmentParent;
    public InventorySlot[] slots;

    // Update is called once per frame
    EquipmentUi equipmentUi;

    void Start()
    {
        equipmentUi = EquipmentUi.instance;
        slots = equipmentParent.GetComponentsInChildren<InventorySlot>();
    }

    public void Add(Item item)
    { //item slot list
        if (item.showInInventory)
        {
            item.isEquipped = true;
            slots[item.itemType].AddItem(item);
            slots[item.itemType].removeButton.interactable = true;
        }
    }

    public void Remove(Item item)
    {
        slots[item.itemType].ClearSlot();
    }
}
