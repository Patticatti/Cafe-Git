using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowScript : MonoBehaviour
{
    public GameObject enemy;

    private Rigidbody2D rb;
    public float force;
    private float timer;
    private Vector3 mousePos;
    private Vector3 direction;
    private Stats stats;
    private Health enemyComponent;
    private bool triggered = false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
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
        if (triggered == false)
        {
            if ((other.gameObject.CompareTag("Enemy")))
            {
                triggered = true;
                enemyComponent = other.GetComponent<Health>();
                enemyComponent.TakeDamage(stats.atkTotal);
                Destroy(gameObject);
            }
            else if ((other.gameObject.CompareTag("Terrain")))
            {
                Destroy(gameObject);
            }
        }
    }
}
