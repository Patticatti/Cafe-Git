using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableObject : MonoBehaviour
{
    private void Start()
    {
        EventManager.Instance.generateEvent.AddListener(Destroy);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) //disappear on hit
    {

            if ((other.gameObject.CompareTag("Weapon")))
            {
                Destroy(gameObject);
            }
    }
}
