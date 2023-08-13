using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    private GameObject player;
    public float timer;
    public float force = 3f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float distance;
    private Vector3 direction;
    private Enemy enemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= 5)
        {
            direction = player.transform.position - transform.position;
            if (direction.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        }
        //Vector2 direction = player.transform.position;

        //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
