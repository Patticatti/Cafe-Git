using UnityEngine;

public class ProjectileShadow : MonoBehaviour
{

    public Vector3 Offset = new Vector3(0f, -0.2f);
    private SpriteRenderer sr;
    private SpriteRenderer render;
    private float timer = 0;

    private GameObject _shadow;

    private void Start()
    {
        _shadow = new GameObject("Shadow");
        _shadow.transform.parent = transform;
        _shadow.transform.position = transform.position + Offset;
        _shadow.transform.rotation = transform.rotation;

        render = GetComponent<SpriteRenderer>();
        sr = _shadow.AddComponent<SpriteRenderer>();
        sr.sprite = render.sprite;
        sr.color = new Color(0f, 0f, 0f, .5f);
        _shadow.transform.localScale = (sr.bounds.size);

        sr.sortingLayerName = render.sortingLayerName;
        sr.sortingOrder = render.sortingOrder - 1;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            Destroy(gameObject);
        }
    }
}
