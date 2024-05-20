using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class IngredientDrop : MonoBehaviour
{
    public Item itemCopy;
    private Vector3 originalPosition;

    private bool isAdded = false;
    private SpriteRenderer sr;

    private GameObject player;
    private Transform playerPosition;

    private const float shrinkDuration = 0.5f;

    private float distance;
    private float timer = 0f;
    public float pickupTimer = 0f;
    private float despawnTimer = 0f;

    public bool canBePickedUp = true;


    private void Start()
    {
        player = Inventory.instance.player;
        playerPosition = player.transform;

        sr = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (Inventory.instance.full)
            canBePickedUp = false;
        if (!isAdded)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            pickupTimer += Time.deltaTime;
            if ((distance <= 5) && (Input.GetMouseButtonDown(0) && !Inventory.instance.full)) //close to player
            {
                if (canBePickedUp)
                {
                    Inventory.instance.Add(itemCopy);
                    isAdded = true;
                    timer = 0f;
                }
                else //not picked up
                {
                    if (pickupTimer >= 5f)
                    {
                        canBePickedUp = true;
                    }
                }
            }
            despawnTimer += Time.deltaTime;
            if (despawnTimer > 180)
            {
                Destroy(gameObject);
            }
        }
        else //is clicked
        {
            timer += Time.deltaTime;
            float t = timer / shrinkDuration;
            if (t <= 1f)
            {
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
                transform.position = Vector3.Lerp(originalPosition, playerPosition.position, t);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }




    /*
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !Inventory.instance.full && !isClicked)
        {
            Inventory.instance.Add(itemCopy);
            isClicked = true;
            isMovingUp = true;
            isWaiting = false;
            timer = 0f;
        }
    }

    private void Destroy()
    {
        Destroy(itemCopy);
    }*/
}
