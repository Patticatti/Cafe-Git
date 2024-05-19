using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderScript : MonoBehaviour
{
    private GameObject player;

    private SpriteRenderer sr;
    private SpriteRenderer render;
    private Vector3 direction;


    // Start is called before the first frame update
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) //disappear on hit
    {
        if (other.gameObject.CompareTag("Player"))
        {
            render = other.GetComponent<SpriteRenderer>();
            direction = other.transform.position - transform.position;
            if (direction.y > 0) //above
            {
                sr.sortingLayerName = render.sortingLayerName;
                sr.sortingOrder = render.sortingOrder + 1;
                sr.color = new Color(1f, 1f, 1f, .5f);
            }
            else
            {
                sr.sortingLayerName = render.sortingLayerName;
                sr.sortingOrder = render.sortingOrder - 1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) //disappear on hit
    {
        if (render != null)
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
            sr.sortingOrder = render.sortingOrder - 1;
        }
    }

}
