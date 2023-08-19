using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ItemDrop : MonoBehaviour
{
    public Item itemCopy;
    public SpriteListHolder sList;
    private PlayerStats stats;
    private Vector3 originalPosition;
    private bool isMovingUp = true;
    private bool isWaiting = false;
    private bool isClicked = false;
    private SpriteRenderer sr;
    private Sprite randomSprite;

    private const float moveDistance = 0.5f;
    private const float moveDuration = 1f;
    private const float waitDuration = 0.2f;

    private GameObject player;
    private Transform playerPosition;

    private const float shrinkDuration = 0.5f;

    private float timer = 0f;
    private float despawnTimer = 0f;


    private void Start()
    {
        player = Inventory.instance.player;
        playerPosition = player.transform;
        stats = player.GetComponent<PlayerStats>();

        sList = GetComponent<SpriteListHolder>();
        sr = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        if (itemCopy == null)
        {
            Debug.Log("newrandom item");
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
        if (isClicked)
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
                Inventory.instance.Add(itemCopy);
                Destroy(gameObject);
            }
        }
        else if (isWaiting)
        {
            timer += Time.deltaTime;
            if (timer >= waitDuration)
            {
                isWaiting = false;
                timer = 0f;
            }
        }
        else
        {
            timer += Time.deltaTime;

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
        if (despawnTimer > 15)
        {
            Destroy(gameObject);
        }
    }



    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
            isMovingUp = true;
            isWaiting = false;
            timer = 0f;
        }
    }

    private void Destroy()
    {
        Destroy(itemCopy);
    }
}
