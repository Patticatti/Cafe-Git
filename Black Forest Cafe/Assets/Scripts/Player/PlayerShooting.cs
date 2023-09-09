using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private Vector2 mousePosition;
    private Animator anim;

    private float atkSpeed = 0.5f;
    private float timer;

    private float distance;

    private GameObject bulletcopy;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //distance = Vector2.Distance(transform.position, mousePosition);
        if ((timer >= atkSpeed) && (Input.GetMouseButton(0)))
        {
            anim.SetInteger("state", 6);
            timer = 0;
            Instantiate(bullet, bulletPos.position, Quaternion.identity, transform);
        }
        timer += Time.deltaTime;
    }
}