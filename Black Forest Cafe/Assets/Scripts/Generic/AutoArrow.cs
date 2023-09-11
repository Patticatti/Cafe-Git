using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoArrow : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public bool isplayer;
    private FindNearest findNearest;
    private GameObject enemy;

    private Rigidbody2D rb;
    private Stats stats;
    public float force = 3;
    private float timer;
    private Vector3 direction;
    private Health enemyHealth;
    private bool triggered = false;


    private void Start()
    {
        stats = Inventory.instance.player.GetComponent<Stats>();
        rb = GetComponent<Rigidbody2D>();
        findNearest = new FindNearest(transform, enemyTag, isplayer);
        enemy = findNearest.TargetEnemy();

        direction = enemy.transform.position - transform.position;
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
            if ((other.gameObject.CompareTag(enemyTag)))
            {
                triggered = true;
                enemyHealth = other.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(stats.atkTotal);
                }
                Destroy(gameObject);
            }
        }
        if ((other.gameObject.CompareTag("Terrain")))
        {
            Destroy(gameObject);
        }
    }
}
