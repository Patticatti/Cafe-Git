using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    private GameObject player;

    private SpriteRenderer sr;
    private SpriteRenderer render;
    private Vector3 direction;
    private float distance;


    // Start is called before the first frame update
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        render = player.GetComponent<SpriteRenderer>();
    }

    private void Update() //disappear on hit
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 1)
        {
            direction = player.transform.position - transform.position;
            if (direction.y > 0)
            {
                sr.sortingLayerName = render.sortingLayerName;
                sr.sortingOrder = render.sortingOrder + 1;
                sr.color = new Color(1f, 1f, 1f, .5f);
            }
            else
            {
                sr.sortingLayerName = render.sortingLayerName;
                sr.sortingOrder = render.sortingOrder - 1;
                sr.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
            sr.sortingOrder = render.sortingOrder - 1;
        }
    }
}
