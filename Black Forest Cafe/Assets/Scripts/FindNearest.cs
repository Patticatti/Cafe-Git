using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearest 
{
    
    public string enemyTag;
    public Transform objectPos;
    public float detectDistance = 10f;

    private List<GameObject> enemiesInRange = new List<GameObject>();
    private List<string> tagList = new List<string>();
    private GameObject targetEnemy;

    public FindNearest(Transform gameObject, string tag)
    {
        tagList.Add("Object");
        tagList.Add("enemyTag");
        enemyTag = tag;
        objectPos = gameObject;
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
