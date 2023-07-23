using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowScript : MonoBehaviour
{
    private GameObject enemy;

    private Rigidbody2D rb;
    public float force = 12f;
    private float timer;
    private Vector3 mousePos;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            Destroy(gameObject);
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D other) //disappear on hit
    {
        if ((other.gameObject.CompareTag("Enemy")) || (other.gameObject.CompareTag("Terrain")))
        {
            Destroy(gameObject);
            //if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent)) //checks to see if has enemy game object
        }
    }*/
}
