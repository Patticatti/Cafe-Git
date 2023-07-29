using UnityEngine;
using UnityEngine.Events;

public class ChangeSpriteOnClick : MonoBehaviour
{
    public Sprite newSprite; // The sprite to change to

    private SpriteRenderer spriteRenderer;

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
        if (Input.GetMouseButtonDown(0))
        {
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
        }
    }
}
