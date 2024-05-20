using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemStatGen : Stats
{//Base stats

    public List<(string, float)> statsContainer = new List<(string, float)>(); //stats container int(type), float(value)
    public List<int> defaultStats = new List<int>(); //default stats

    private void GenerateRandomStats() //default stats are atk+spd, hp, def
    {
        statsContainer.Clear();
        string text = "";
        float randVal;
        float smallRando = Random.value;
        foreach (int stat in defaultStats) //health,armor,atkDamage, atkIncrease,atkInterval
        {
            randVal = (smallRando * (Random.Range(Level.instance.level + 1, Level.instance.level + 4)));
            switch (stat)
            {
                case 0: //health, 5
                    randVal = randVal * 5f; //modifier for health
                    health = health + randVal;

                    text = "health";
                    break;
                case 1: //armor, 1
                    armor = armor + randVal;
                    text = "armor";
                    break;
                case 2: //1
                    atkDamage = atkDamage + randVal;
                    text = "attack damage";
                    break;
                case 3:
                    atkIncrease = atkIncrease + randVal;
                    text = "attack increase";
                    break;
                case 4:
                    attackInterval = attackInterval * (1 - smallRando);
                    text = "attack speed";
                    break;
            }
            statsContainer.Add((text, randVal));
        }
    }

    public string GenerateMessage()
    {
        GenerateDefaultStats();
        GenerateRandomStats();
        string message = "";
        foreach ((string txt, float value) item in statsContainer)
        {
            message = message + item.txt + ": " + item.value + "\n";
        }
        return message;
    }

    private void GenerateDefaultStats() //generate list of 2-3 default stats
    {
        defaultStats.Clear();
        for (int i = 0; i < (Random.Range(4, 6)); i++) //add 4/5 numbers 1-5
        {
            defaultStats.Add(Random.Range(0, 5));
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

