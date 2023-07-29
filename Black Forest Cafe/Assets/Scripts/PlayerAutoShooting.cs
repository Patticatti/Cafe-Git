using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoShooting : MonoBehaviour
{
    public float shootingRange = 10f;
    public float shootingInterval = 0.5f;
    public string enemyTag = "Enemy";
    public GameObject bullet;
    public Transform bulletPos;

    private FindNearest findNearest;
    private GameObject target;
    private Animator anim;
    private float timer;

    private void Start()
    {
        findNearest = new FindNearest(bulletPos, enemyTag);
        anim = GetComponent<Animator>();
    }

    private void OnShootingAnimationEnd()
    {
        anim.SetInteger("state", 0); // Switch to the Idle state (or any other state you want)
    }

    // Replace this with your shooting logic, this is just a simple example
    private void Shoot()
    {
        if (target != null)
        {
            anim.SetInteger("state", 6);
            timer = 0;
            Instantiate(bullet, bulletPos.position, Quaternion.identity, transform);

        }
    }

    // Replace this with your input handling or shooting method trigger
    private void Update()
    {
        if (Input.GetMouseButton(0) && (timer >= shootingInterval))
        {
            target = findNearest.TargetEnemy();
            Shoot();
        }
        timer += Time.deltaTime;
    }
}
