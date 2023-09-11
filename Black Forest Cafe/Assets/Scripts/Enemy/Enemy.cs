using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Health playerHealth;
    //[SerializeField]
    //private EnemyScriptableObject enemyScriptableObject;
    //[SerializeField]
    //private AudioSource audioSource;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField]
    public GameObject item;
    private Stats stats; //self enemy stats
    private AutoShooting autoShooting;

    [SerializeField] float health, maxHealth = 5f;
    private float timer;
    private float spd = 3f;
    private float distance;
    private Vector3 direction;
    private bool isAggro = false;
    public bool isRanged;

    private void Start()
    {
        stats = GetComponent<Stats>();
        if (isRanged)
        {
            autoShooting = GetComponent<AutoShooting>();
        }
        player = Inventory.instance.player;
        playerHealth = player.GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        health = maxHealth;
        spd = stats.moveSpeed;
        EventManager.Instance.generateEvent.AddListener(Destroy);
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position); //distance is distance from player
        if (!isAggro && distance < stats.attackRange)
        {
            isAggro = true;
        }
        if (isAggro)
        {
            if (isRanged)
            {
                timer += Time.deltaTime;
                direction = player.transform.position - transform.position;
                if (distance < stats.attackRange) //within range to attack
                {
                    if (direction.x < 0) //face player
                    {
                        sr.flipX = true;
                    }
                    else
                    {
                        sr.flipX = false;
                    }
                    if (timer >= stats.attackInterval) //attack player
                    {
                        timer = 0;
                        autoShooting.Shoot();
                        rb.velocity = new Vector2(0, 0);
                    }

                }
                else
                {
                    rb.velocity = new Vector2(direction.x, direction.y).normalized * spd;
                }
            }
            else
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
        }
        //Vector2 direction = player.transform.position;

        //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void AttackPlayer()
    {

    }
    /*
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
    }*/


    public void Destroy()
    {
        if (Random.value <= player.GetComponent<Stats>().dropChance)
        {
            GameObject newItem = Instantiate(item, transform.position, Quaternion.identity);
            newItem.transform.parent = null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other) //disappear on hit
    {
        if (other.gameObject == player && playerHealth.canTakeDmg)
            player.GetComponent<Health>().TakeDamage(stats.atkTotal);
    }
}
// Update is called once per frame
