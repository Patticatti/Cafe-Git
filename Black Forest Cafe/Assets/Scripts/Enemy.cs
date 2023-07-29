using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private PlayerHealth playerComponent;

    [SerializeField] float health, maxHealth = 5f;
    private void Start()
    {
        health = maxHealth;
        EventManager.Instance.generateEvent.AddListener(Destroy);
    }

    private void DropItem()
    {
        if (Random.value > 0)
            Debug.Log("dropped item");
    }

    public void TakeDamage(float damageAmount)
    {
        health = health - damageAmount;
        if (health <= 0)
        {
            DropItem();
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
