using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{//Base stats
    public float health = 10f;
    public float armor = 2f;
    public float moveSpeed = 6f;
    public float dashSpeed;

    public float attackRange = 10f;
    public float attackInterval = 0.5f;

    public float dropChance = 0.5f;
    public float atkDamage = 2f;
    public float atkIncrease = 1f;
    public float atkTotal;

    public int projectileAmnt = 1;
    public int projectileBounces = 0;

    public bool iFrames = false;

    private void Start()
    {
        atkTotal = atkDamage * atkIncrease;
        dashSpeed = moveSpeed * 2f;
    }

    private void Update()
    {
        atkTotal = atkDamage * atkIncrease;
        dashSpeed = moveSpeed * 2f;
    }

}
