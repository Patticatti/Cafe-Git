using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stats : MonoBehaviour
{//Base stats
    public float health = 10f; //health is flat health
    public float armor = 2f;
    public float moveSpeed = 6f;
    public float dashSpeed;

    public float attackRange = 10f;
    public float attackInterval = 0.5f;

    public float critRate = 0.05f;
    public float critDmg = 2f;

    public float dropChance = 0.5f;
    public float atkDamage = 2f;
    public float atkIncrease = 1f; //percent
    public float atkTotal;

    public int projectileAmnt = 1;
    public int projectileBounces = 0;

    public Dictionary<int,float> statsContainer= new Dictionary<int,float>(); //stats container
    public List<int> defaultStats = new List<int>();

    public bool iFrames = false;

    private void Start()
    {
        UpdateStats();
    }

    public void GenerateRandomStats(int itemType) //default stats are atk+spd, hp, def
    {
        statsContainer.Clear();
        string a, b, c, d;
        List<int> statSpread = GenerateDefaultStats();
        foreach (int stat in statSpread) //health,armor,atkDamage, atkIncrease,atkInterval
        {
            switch (stat)
            {
                case 0: //health, 5
                    health = Random.value * (Random.Range(0, 2) + Level.instance.level * 5);

                    break;
                case 1: //armor, 1
                    armor = Random.value * (Random.Range(0, 2) + Level.instance.level * 1);
                    break;
                case 2: //1
                    atkDamage = Random.value * (Random.Range(0, 2) + Level.instance.level);
                    break;
                case 3:
                    atkIncrease = Random.value * (Random.Range(0, 2) + Level.instance.level);
                    break;
                case 4:
                    attackInterval = Random.value * (Random.Range(0, 2) + Level.instance.level);
                    break;


            }
        }

        switch (itemType)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;

        }
    }

    private List<int> GenerateDefaultStats() //generate list of 2-3 default stats
    {
        defaultStats.Clear();
        for (int i = 0; i < (Random.Range(4,6)); i++) //add 4/5 numbers 1-5
        {
            defaultStats.Add(i);
        }
        if (defaultStats.Count >= 2)
        {
            // Generate two unique random indices
            int indexToRemove1 = Random.Range(0, defaultStats.Count);
            int indexToRemove2;

            do
            {
                indexToRemove2 = Random.Range(0, defaultStats.Count);
            } while (indexToRemove2 == indexToRemove1); // Ensure index2 is different from index1

            // Remove the elements at the generated indices
            defaultStats.RemoveAt(indexToRemove1);
            defaultStats.RemoveAt(indexToRemove2 > indexToRemove1 ? indexToRemove2 - 1 : indexToRemove2);
        }
        return defaultStats;
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

