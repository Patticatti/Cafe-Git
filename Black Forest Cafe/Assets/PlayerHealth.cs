using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 10f;
    private SpriteRenderer sr;
    private float timer;
    public bool canTakeDmg = true;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        if (canTakeDmg)
        {
            canTakeDmg = false;
            sr.color = new Color(1, 0, 0, 1);
            timer = 0;
            health = health - damageAmount;
            if (health <= 0)
            {
                Debug.Log("you died");
                sr.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5)
        {
            canTakeDmg = true;
            sr.color = new Color(1, 1, 1, 1);
        }
    }
}
