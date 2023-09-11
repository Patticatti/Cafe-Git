using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool isPlayer = false;
    public float health, maxHealth = 10f;
    public Material hitMaterial;
    public Material spriteMaterial;
    private Enemy enemyComponent;
    private SpriteRenderer sr;
    private Stats stats; //self stats
    public float timer;
    public bool canTakeDmg = true;
    public HealthBar healthBar;

    private void Start()
    {
        stats = GetComponent<Stats>();
        if (!isPlayer)
        {
            enemyComponent = GetComponent<Enemy>();
        }
        else
        {
            healthBar.SetMaxHealth(maxHealth);
        }

        //iframes = stats.iFrames;
        maxHealth = stats.health;
        health = maxHealth;
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damageAmount)
    {
        //if (canTakeDmg || iFrames == false) //false for enemy but iframes is false so works
        if (canTakeDmg) //doesnt have iframes to block dmg
        {
            health = health - damageAmount;
            //healthBar.SetHealth(health);
            if (health <= 0)
            {
                if (!isPlayer)
                {
                    enemyComponent.Destroy();
                    return;
                }
                Debug.Log("you died");
                sr.color = new Color(0.5f, 0.5f, 0.5f, 1f);

            }
            if (isPlayer)
            {
                sr.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                healthBar.SetHealth(health);
                canTakeDmg = false;
            }
            else
            {
                sr.material = hitMaterial;
            }
            timer = 0;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isPlayer)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                canTakeDmg = true;
                sr.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else
        {
            if (timer > 0.2)
            {
                sr.material = spriteMaterial;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
