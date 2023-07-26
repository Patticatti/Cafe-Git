using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    // Declare the shared event
    public static EventManager Instance;

    public UnityEvent generateEvent;

    public EventManager()
    {
        if (Instance == null)
        {
            Instance = this;
            generateEvent = new UnityEvent();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // You can add any additional utility functions related to event management here
}
