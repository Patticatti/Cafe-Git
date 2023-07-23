using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    private float dirY = 0f;
    private float modifierr = 1f;

    private Vector2 mousePosition;
    private Vector2 moveDirection;
    private float aimAngle = 0f;
    private int facing = 0; //need for updating animation and interacting

    [SerializeField] private float moveSpeed = 6f;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 12f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 0.5f;
    bool isDashing;
    bool canDash;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        canDash = true;
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
        if (dirX != 0f && dirY != 0f)
        {
            modifierr = 0.707f;
        }
        else
        {
            modifierr = 1f;
        }
        if (Input.GetKey(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
        moveDirection = new Vector2(dirX, dirY).normalized * modifierr;
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
            anim.SetInteger("state", 3);
        }
        else
        {
            anim.SetInteger("state", 2);
        }
        if (dirX < 0f) //left
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
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * moveSpeed;

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
