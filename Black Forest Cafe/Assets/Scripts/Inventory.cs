using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }
    #endregion
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public Transform playerPosition;
    public GameObject player;
    public GameObject itemDrop;
    public bool full = false;

    public int space = 14; //invspace
    public List<Item> items = new List<Item>(); 

    private void Update()
    {
        full = (items.Count >= space);
    }

    public void ClearInventory()
    {
        items.Clear();
        onItemChangedCallback.Invoke();
    }

    public void Add(Item item)
    {
        if (item.showInInventory)
        {
            item.isEquipped = false;
            if (items.Count > space)
            {
                    return; 
            }
            items.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null )
        {
            onItemChangedCallback.Invoke();
        }
    }
}
