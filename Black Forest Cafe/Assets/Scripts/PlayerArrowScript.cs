using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowScript : MonoBehaviour
{
    private GameObject enemy;

    private Rigidbody2D rb;
    public float force;
    private float timer;
    private Vector3 mousePos;
    private Vector3 direction;
    private Enemy enemyComponent;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //disappear on hit
    {
        if ((other.gameObject.CompareTag("Enemy")))
        {
            enemyComponent = other.GetComponent<Enemy>();
            enemyComponent.TakeDamage(1);
            Destroy(gameObject);
            //if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent)) //checks to see if has enemy game object
        }
        else if ((other.gameObject.CompareTag("Terrain")))
        {
            Destroy(gameObject);
        }
    }/*

    private void OnCollisionEnter2D(Collision2D collision) //disappear on hit
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(1);
        }
        Destroy(gameObject);
    }*/
}
