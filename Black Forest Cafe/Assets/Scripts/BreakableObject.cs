using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    public GameObject item;
    private Stats stats;
    private void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Stats>();
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
            if (Random.value <= stats.dropChance)
            {
                item.GetComponent<ItemDrop>().itemCopy = null;
                GameObject newItem = Instantiate(item, transform.position, Quaternion.identity);
                newItem.transform.parent = null;
            }
            Destroy(gameObject);
        }
    }
}
