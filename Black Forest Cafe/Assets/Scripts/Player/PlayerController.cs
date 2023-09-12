using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public bool inCombat;
}
