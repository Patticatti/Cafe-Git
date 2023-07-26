using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private PlayerHealth playerComponent;

    [SerializeField] float health, maxHealth = 5f;
    private void Start()
    {
        health = maxHealth;
        EventManager.Instance.generateEvent.AddListener(Destroy);
    }

    public void TakeDamage(float damageAmount)
    {
        health = health - damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) //disappear on hit
    {
        if ((other.gameObject.CompareTag("Player")))
        {
            playerComponent = other.GetComponent<PlayerHealth>();
            playerComponent.TakeDamage(1f);
        }
    }
}
    // Update is called once per frame
