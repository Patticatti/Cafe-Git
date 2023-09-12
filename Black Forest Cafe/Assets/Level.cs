using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    #region Singleton
    public static Level instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public int level = 1;

    public int dungeongen = 1;

    [SerializeField]
    private GameObject gameObjParentReference;

    public GameObject gameObjParentObject;
    public GameObject itemParentObject;
    public Transform gameObjParent;
    public Transform itemParent;

    private GameObject newGameObject;
    private GameObject newItem;

    public List<GameObject> gameObjectsList = new List<GameObject>(); //list of gameobjects to delete when regenerating dungeon
    public List<GameObject> enemiesList = new List<GameObject>(); 

    public List<GameObject> itemsList = new List<GameObject>(); //list of gameobjects to delete when regenerating dungeon


    public void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        dungeongen++;
        Inventory.instance.player.transform.position = Vector3.zero;
        Inventory.instance.ClearInventory();
        GenerateLevelObjects();
        GenerateLevelItems();
    }


    public void GenerateLevelObjects()
    {
        ClearGameObjects();
        gameObjParentObject = Instantiate(gameObjParentReference);
        gameObjParent = gameObjParentObject.transform;
    }

    public void GenerateLevelItems()
    {
        ClearLevelItems();
        //itemParentObject = Instantiate(gameObjParentReference);
        //itemParentObject.name = "Items Parent";
        //itemParent = itemParentObject.transform;
    }

    public void DropItem(GameObject item, Vector3 position, Quaternion rotation)
    {
        newItem = Instantiate(item, position, rotation);
        newItem.transform.parent = gameObjParent;
        itemsList.Add(newItem);
    }

    public void ClearGameObjects()
    {
        foreach (GameObject obj in gameObjectsList)
        {
            Destroy(obj);
        }
        Destroy(gameObjParentObject);
        gameObjectsList.Clear();
    }

    public void ClearLevelItems()
    {
        foreach (GameObject obj in itemsList)
        {
            Destroy(obj);
        }
        //Destroy(itemParentObject);
        itemsList.Clear();
    }

    public void Add(GameObject gameObject, Vector2Int position, int groupNo = 0)
    {
        newGameObject = Instantiate(gameObject, new Vector3(position.x + 0.5f, position.y + 0.5f, 0), Quaternion.identity);
        newGameObject.transform.SetParent(gameObjParent);
        if (groupNo!=0 )  //if enemy
        {
            newGameObject.GetComponent<Enemy>().groupNumber = groupNo;
            enemiesList.Add(newGameObject);
        }
        gameObjectsList.Add(newGameObject);
    }

    public void AggroGroup(int groupNo)
    {
        Enemy enemyComponent;
        foreach (GameObject obj in enemiesList)
        {
            if (obj != null)
            {
                enemyComponent = obj.GetComponent<Enemy>();
                if (enemyComponent.groupNumber == groupNo)
                {
                    enemyComponent.isAggro = true;
                }
            }
        }
    }
}
