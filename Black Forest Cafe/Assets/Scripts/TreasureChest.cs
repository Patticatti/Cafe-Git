using UnityEngine;
using UnityEngine.Events;

public class ChangeSpriteOnClick : MonoBehaviour
{
    public Sprite newSprite; // The sprite to change to
    public GameObject item;

    private SpriteRenderer spriteRenderer;
    private bool isClick = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventManager.Instance.generateEvent.AddListener(Destroy);
    }


    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !isClick)
        {
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
                GameObject newItem = Instantiate(item, transform.position, Quaternion.identity);
                isClick = true;
                //newItem.transform.parent = null;
            }
        }
    }
}
