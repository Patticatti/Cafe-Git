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

    private void OnTriggerEnter2D(Collider2D other) //disappear on hit
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;
        if (direction.y > 0)
        {
            sr.sortingLayerName = render.sortingLayerName;
            sr.sortingOrder = render.sortingOrder - 1;
        }
        else
        {
            sr.sortingLayerName = render.sortingLayerName;
            sr.sortingOrder = render.sortingOrder + 1;
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
        if ((other.gameObject.CompareTag("Player")))
        {
            sr.color = new Color(1f, 1f, 1f, .5f);
        }
    }
}
