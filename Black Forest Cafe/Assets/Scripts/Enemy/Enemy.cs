using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private EnemyScriptableObject enemyScriptableObject;
    //[SerializeField]
    //private AudioSource audioSource;
    private PlayerHealth playerComponent;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PlayerStats stats;
    [SerializeField]
    public GameObject item;

    [SerializeField] float health, maxHealth = 5f;
    private float timer;
    private float spd = 3f;
    private float distance;
    private Vector3 direction;

    private void Start()
    {
        player = Inventory.instance.player;
        stats = player.GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        health = maxHealth;
        playerComponent = player.GetComponent<PlayerHealth>();
        EventManager.Instance.generateEvent.AddListener(Destroy);
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < enemyScriptableObject.enemyAttackType.attackRange)
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
            rb.velocity = new Vector2(direction.x, direction.y).normalized * spd;
        }
        //Vector2 direction = player.transform.position;

        //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(float damageAmount)
    {
        //sr.material = whiteMaterial;
        health = health - damageAmount;
        if (health <= 0)
        {
            if (Random.value <= stats.dropChance)
            {
                GameObject newItem = Instantiate(item, transform.position, Quaternion.identity);
                newItem.transform.parent = null;
                Debug.Log("dropped");
            }
            Destroy(gameObject);
        }
        //Invoke("ResetSpriteColor", flashDuration);
    }

    private void ResetSpriteColor()
    {
        //sr.material = originalMaterial;
    }


    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) //disappear on hit
    {
        if (other.gameObject == player)
            playerComponent.TakeDamage(1f);
    }
}
    // Update is called once per frame
