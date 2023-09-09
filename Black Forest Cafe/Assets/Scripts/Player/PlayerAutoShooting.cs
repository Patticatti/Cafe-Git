using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoShooting : MonoBehaviour
{
    public float shootingRng = 10f;
    public float shootingIntrvl = 0.5f;
    public string enemyTag = "Enemy";
    public GameObject bullet;
    public Transform bulletPos;

    private FindNearest findNearest;
    private GameObject target;
    private Animator anim;
    private Stats stats;
    private float timer;

    private void Start()
    {
        stats = GetComponent<Stats>();
        shootingRng = stats.attackRange;
        shootingIntrvl = stats.attackInterval;
        findNearest = new FindNearest(bulletPos, enemyTag);
        anim = GetComponent<Animator>();
    }

    private void OnShootingAnimationEnd()
    {
        anim.SetInteger("state", 0); 
    }

    private void Shoot()
    {
        if (target != null)
        {
            anim.SetInteger("state", 6);
            timer = 0;
            Instantiate(bullet, bulletPos.position, Quaternion.identity, transform);

        }
    }

    private void Update()
    {
        shootingRng = stats.attackRange;
        shootingIntrvl = stats.attackInterval; 
        if (Input.GetMouseButton(0) && (timer >= shootingIntrvl))
        {
            target = findNearest.TargetEnemy();
            Shoot();
        }
        timer += Time.deltaTime;
    }
}
