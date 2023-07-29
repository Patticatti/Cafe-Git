using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoArrow : MonoBehaviour
{
    public string enemyTag = "Enemy";
    private FindNearest findNearest;
    private GameObject enemy;

    private Rigidbody2D rb;
    public float force = 3;
    private float timer;
    private Vector3 direction;
    private Enemy enemyComponent;
    private bool triggered = false;

    // Start is called before the first frame update

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        findNearest = new FindNearest(transform, enemyTag);
        enemy = findNearest.TargetEnemy();

        direction = enemy.transform.position - transform.position;
        Debug.Log(direction);
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
                enemyComponent = other.GetComponent<Enemy>();
                enemyComponent.TakeDamage(1f);
                Destroy(gameObject);
            }
            else if ((other.gameObject.CompareTag("Terrain")))
            {
                Destroy(gameObject);
            }
        }
    }
}
