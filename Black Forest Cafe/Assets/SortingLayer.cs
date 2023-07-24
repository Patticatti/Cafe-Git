using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
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
        if ((other.gameObject.CompareTag("Player")))
        {
            render = other.GetComponent<SpriteRenderer>();
        }
        else if ((other.gameObject.CompareTag("Enemy")))
        {
            render = other.GetComponent<SpriteRenderer>();
        }
        direction = other.transform.position - transform.position;
    }

    private void OnTriggerStay2D(Collider2D other) //disappear on hit
    {
        if (direction.y > 0) //above
        {
            sr.sortingLayerName = render.sortingLayerName;
            sr.sortingOrder = render.sortingOrder + 1;
            if (other.gameObject.CompareTag("Player"))
            {
                sr.color = new Color(1f, 1f, 1f, .5f);
            }
            else 
            {
                sr.color = new Color(1f, 1f, 1f, 1f);
                sr.sortingLayerName = render.sortingLayerName;
                sr.sortingOrder = render.sortingOrder + 1;
            }
        }
        else
        {
            sr.sortingLayerName = render.sortingLayerName;
            sr.sortingOrder = render.sortingOrder - 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other) //disappear on hit
    {
        sr.color = new Color(1f, 1f, 1f, 1f);
        sr.sortingOrder = render.sortingOrder - 1;
    }

}
