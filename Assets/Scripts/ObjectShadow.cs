using UnityEngine;

public class ObjectShadow : MonoBehaviour
{

    public Vector3 Offset = new Vector3(0f, -1f);
    private SpriteRenderer sr;
    private SpriteRenderer render;

    private GameObject _shadow;

    private void Start()
    {
        _shadow = new GameObject("Shadow");
        _shadow.transform.parent = transform;

        _shadow.transform.localPosition = Offset;
        _shadow.transform.localRotation = Quaternion.identity;

        render = GetComponent<SpriteRenderer>();
        sr = _shadow.AddComponent<SpriteRenderer>();
        sr.sprite = render.sprite;
        sr.color = new Color(0f, 0f, 0f, .5f);
        sr.flipY = true;
        _shadow.transform.localScale = (sr.bounds.size);

        sr.sortingLayerName = render.sortingLayerName;
        sr.sortingOrder = render.sortingOrder - 1;
    }

    // Update is called once per frame
    private void Update()
    {
        sr.sprite = render.sprite;
        sr.flipX = render.flipX;
//_shadow.transform.localPosition = Offset;
    }
}
