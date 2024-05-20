using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string message = "defailt";
    private Stats stats;
    public bool isShowing = false;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (isShowing)
            TooltipManager.instance.SetAndShowToolTip(message);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        TooltipManager.instance.HideToolTip();
    }
    /*
    public string WriteMessage()
    {
        stats = transform.parent.GetComponent<InventorySlot>().GetComponent<Stats>();
    }*/
}
