using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stats : MonoBehaviour
{//Base stats
    public float health = 0f; //health is flat health
    public float armor = 0f;
    public float moveSpeed = 0;
    public float dashSpeed;

    public float attackRange = 0f;
    public float attackInterval = 1f;

    public float critRate = 0f;
    public float critDmg = 0f;

    public float dropChance = 0f;
    public float atkDamage = 0f;
    public float atkIncrease = 0f; //percent
    public float atkTotal;

    public int projectileAmnt = 0;
    public int projectileBounces = 0;

    public bool iFrames = false;

    private void Start()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        atkTotal = atkDamage * atkIncrease;
        dashSpeed = moveSpeed * 2f;
    }
    /*
    private enum DefaultStats
    {
        health,
        armor,
        atkDamage,
        atkIncrease,
        atkInterval
    }*/
}

