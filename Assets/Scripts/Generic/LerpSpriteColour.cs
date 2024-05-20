using UnityEngine;

public class LerpSpriteColour : MonoBehaviour
{
    // Blends between two materials

    public Material original;
    public Material whiteFlash;
    float duration = 2.0f;
    private SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        // At start, use the first material
        rend.material = original;
    }

    void Update()
    {
        // ping-pong between the materials over the duration
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.Lerp(original, whiteFlash, lerp);
    }
}