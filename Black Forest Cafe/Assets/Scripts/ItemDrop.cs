using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ItemDrop : MonoBehaviour
{
    public Item itemCopy;
    public SpriteListHolder sList;
    private Stats stats;
    private Vector3 originalPosition;
    private bool isMovingUp = true;
    private bool isWaiting = false;
    private bool isAdded = false;
    private SpriteRenderer sr;
    private Sprite randomSprite;

    private const float moveDistance = 0.5f;
    private const float moveDuration = 1f;
    private const float waitDuration = 0.2f;

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
        stats = GetComponent<Stats>();

        sList = GetComponent<SpriteListHolder>();
        sr = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        if (itemCopy == null)
        {
            itemCopy = (Item)ScriptableObject.CreateInstance(typeof(Item));
            randomSprite = sList.GetRandomSprite();
            itemCopy.icon = randomSprite;
            itemCopy.itemType = sList.itemKind;
            sr.sprite = randomSprite;
        }
        UpdateSprite();
    }
    public void UpdateSprite()
    {
        sr.sprite = itemCopy.icon;
    }

    private void Update()
    {
        if (Inventory.instance.full)
            canBePickedUp = false;
        if (!isAdded)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            pickupTimer += Time.deltaTime;
            if (distance <= 5f) //close to player
            {
                if (canBePickedUp)
                {
                    Inventory.instance.Add(itemCopy);
                    isAdded = true;
                    isMovingUp = true;
                    isWaiting = false;
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
            timer += Time.deltaTime;
            if (isWaiting)
            {
                if (timer >= waitDuration)
                {
                    isWaiting = false;
                    timer = 0f;
                }
            }
            else //floating anim
            {
                if (isMovingUp)
                {
                    transform.position = Vector3.Lerp(originalPosition, originalPosition + new Vector3(0f, moveDistance, 0f), timer / moveDuration);
                    if (timer >= moveDuration)
                    {
                        timer = 0f;
                        isMovingUp = false;
                        isWaiting = true;
                    }
                }
                else
                {
                    transform.position = Vector3.Lerp(originalPosition + new Vector3(0f, moveDistance, 0f), originalPosition, timer / moveDuration);
                    if (timer >= moveDuration)
                    {
                        timer = 0f;
                        isMovingUp = true;
                        isWaiting = true;
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
    }*/

    private void Destroy()
    {
        Destroy(itemCopy);
    }
}
