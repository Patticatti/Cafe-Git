using UnityEngine;

public class MoveSpriteUpAndDown : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isMovingUp = true;
    private bool isWaiting = false;
    private bool isClicked = false;

    private const float moveDistance = 0.2f;
    private const float moveDuration = 1f;
    private const float waitDuration = 0.5f;

    [SerializeField]
    private Transform playerPosition;

    private const float shrinkDuration = 0.5f;

    private float timer = 0f;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (isClicked)
        {
            timer += Time.deltaTime;
            float t = timer / shrinkDuration;
            if (t <= 1f)
            {
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
                transform.position = Vector3.Lerp(originalPosition, playerPosition.position, t);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (isWaiting)
        {
            timer += Time.deltaTime;
            if (timer >= waitDuration)
            {
                isWaiting = false;
                timer = 0f;
            }
        }
        else
        {
            timer += Time.deltaTime;

            if (isMovingUp)
            {
                transform.position = Vector3.Lerp(originalPosition, originalPosition + new Vector3(0f, moveDistance, 0f), timer / moveDuration);
                if (timer >= moveDuration)
                {
                    timer = 0f;
                    isMovingUp = false;
                    isWaiting = true;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(originalPosition + new Vector3(0f, moveDistance, 0f), originalPosition, timer / moveDuration);
                if (timer >= moveDuration)
                {
                    timer = 0f;
                    isMovingUp = true;
                    isWaiting = true;
                }
            }
        }
    }


    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
            isMovingUp = true;
            isWaiting = false;
            timer = 0f;
        }
    }
}
