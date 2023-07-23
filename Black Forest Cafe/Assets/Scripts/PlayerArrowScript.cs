using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowScript : MonoBehaviour
{
    private GameObject enemy;

    private Rigidbody2D rb;
    public float force = 1f;
    private float timer;
    private Vector3 mousePos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        Debug.Log("velocity");
        Debug.Log(rb.velocity);
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
            Destroy(gameObject);

        }
    }
}
