using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    #region Singleton
    public static TooltipManager instance;

    void Awake()
    {
        if (instance != null && instance != this) //already tooltipmanager
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetAndShowToolTip(string message)
    {
        gameObject.SetActive(true);
        if (message != null)
        {
            textComponent.text = message;
        }
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
