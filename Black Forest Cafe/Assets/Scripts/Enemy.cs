using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private PlayerHealth playerComponent;
    private SpriteRenderer sr;
    //private Color originalColor;
    //private const float flashDuration = 0.2f;
    //private Material originalMaterial;
   // private Material whiteMaterial;

    [SerializeField] float health, maxHealth = 5f;
    private void Start()
    {
        //sr = GetComponent<SpriteRenderer>();
        //originalMaterial = sr.material;
        //whiteMaterial = new Material(Shader.Find("Custom/WhiteFlash"));
        //whiteMaterial.color = Color.white;
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
        //sr.material = whiteMaterial;
        health = health - damageAmount;
        if (health <= 0)
        {
            DropItem();
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
