using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearest 
{
    
    public string enemyTag;
    public Transform objectPos;
    public float detectDistance = 10f;

    private List<GameObject> enemiesInRange = new List<GameObject>();
    private GameObject targetEnemy;
    private bool isPlayer;

    public FindNearest(Transform gameObject, string tag, bool isplayer)
    {
        enemyTag = tag;
        objectPos = gameObject;
        isPlayer = isplayer;
    }


    public GameObject TargetEnemy()
    {
        FindObjectsInRange();
        if (enemiesInRange.Count > 0)
        {
            float closestDistance = Mathf.Infinity;
            GameObject closestEnemy = null;

            foreach (GameObject enemy in enemiesInRange)
            {
                float distanceToEnemy = Vector3.Distance(objectPos.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }

            targetEnemy = closestEnemy;
        }
        else
        {
            targetEnemy = null;
        }
        return targetEnemy;
    }

    private void FindObjectsInRange()
    {
        enemiesInRange.Clear();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(objectPos.position, enemy.transform.position);
            if (distanceToEnemy <= detectDistance)
            {
                enemiesInRange.Add(enemy);
            }
        }
        if (isPlayer)
        {
            foreach (GameObject obj in objects)
            {
                float distanceToObj = Vector3.Distance(objectPos.position, obj.transform.position);
                if (distanceToObj <= detectDistance)
                {
                    enemiesInRange.Add(obj);
                }
            }
        }
    }
}
