using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private PlayerHealth playerComponent;
    private SpriteRenderer sr;
    private PlayerStats stats;
    [SerializeField]
    public GameObject item;

    [SerializeField] float health, maxHealth = 5f;
    private void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        health = maxHealth;
        EventManager.Instance.generateEvent.AddListener(Destroy);
    }
    /*
    private void DropItem()
    {
        if (Random.value <= stats.dropChance)
        {
            Instantiate(item, transform);
            Debug.Log("dropped");
        }
    }*/

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
        if ((other.gameObject.CompareTag("Player")))
        {
            playerComponent = other.GetComponent<PlayerHealth>();
            playerComponent.TakeDamage(1f);
        }
    }
}
    // Update is called once per frame
