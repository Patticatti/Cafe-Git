using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private Stats stats;

    private float dirX = 0f;
    private float dirY = 0f;

    private Vector2 mousePosition;
    private Vector2 moveDirection;
    private float aimAngle = 0f;
    private int facing = 0; //need for updating animation and interacting

    private float moveSpd;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 0.5f;
    bool isDashing;
    bool canDash;


    private void Start()
    {
        stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        canDash = true;
    }

    private void Update()
    {
        moveSpd = stats.moveSpeed;
        dashSpeed = moveSpd * 2f;


        if (isDashing)
        {
            return;
        }
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
        moveDirection = new Vector2(dirX, dirY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (dirX == 0f && dirY == 0f)
        {
            FaceMouse();
        }
        else
        {
            UpdateAnimationState();
        }
    }

    private void UpdateAnimationState()
    {
        if (dirY > 0f) //up
        {
            if (isDashing == true)
            {
                anim.SetInteger("state", 5);
            }
            else
            {
                anim.SetInteger("state", 3);
            }
        }
        else
        {
            if (isDashing == true)
            {
                anim.SetInteger("state", 4);
            }
            else
            {
                anim.SetInteger("state", 2);
            }
        }
        if (dirX< 0f) //left
        {
            sprite.flipX = true;
        }
        else
{
    sprite.flipX = false;
}
    }

    private void FaceMouse()

{
    if (aimAngle < 45f && aimAngle > -45f) //up
    {
        facing = 0;
        anim.SetInteger("state", 0);
    }
    else if (aimAngle > -135f) //right
    {
        facing = 3;
        anim.SetInteger("state", 1);
        sprite.flipX = false;
    }
    else if (aimAngle > -225f)
    {
        facing = 1;
        anim.SetInteger("state", 1);
    }
    else //left
    {
        facing = 2;
        anim.SetInteger("state", 1);
        sprite.flipX = true;
    }
}

private void FixedUpdate()
{
    if (isDashing)
    {
        return;
    }
    rb.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * moveSpd;

    Vector2 aimDirection = mousePosition - rb.position;


    aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
}

private IEnumerator Dash()
{
    canDash = false;
    isDashing = true;
    rb.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * dashSpeed;
    yield return new WaitForSeconds(dashDuration);
    isDashing = false;

    yield return new WaitForSeconds(dashCooldown);
    canDash = true;
}

}
