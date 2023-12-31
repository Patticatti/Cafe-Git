using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    // Declare the shared event
    public static EventManager Instance;

    public UnityEvent generateEvent;

    public UnityEvent destroyEntities;

    public UnityEvent aggroEnemies;

    public EventManager()
    {
        if (Instance == null)
        {
            Instance = this;
            aggroEnemies = new UnityEvent();
            generateEvent = new UnityEvent();
            destroyEntities = new UnityEvent();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // You can add any additional utility functions related to event management here
}
