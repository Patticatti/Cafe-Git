using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooting : MonoBehaviour
{
    private float shootingRng = 10f;
    private float shootingIntrvl = 0.5f;
    public string enemyTag = "Enemy";
    public GameObject bullet;
    public Transform bulletPos;
    public bool isPlayer = false;

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
        findNearest = new FindNearest(bulletPos, enemyTag, isPlayer);
        target = findNearest.TargetEnemy();
        anim = GetComponent<Animator>();
    }

    private void OnShootingAnimationEnd()
    {
        if (isPlayer)
            anim.SetInteger("state", 0); 
    }

    public void Shoot()
    {
        if (target != null)
        {
            if (isPlayer)
                anim.SetInteger("state", 6);
            timer = 0;
            Instantiate(bullet, bulletPos.position, Quaternion.identity, transform);

        }
    }

    private void Update()
    {
        if (isPlayer)
        {
            shootingRng = stats.attackRange;
            shootingIntrvl = stats.attackInterval;
            if (Input.GetMouseButton(0) && (timer >= shootingIntrvl))
            {
                target = findNearest.TargetEnemy();
                Shoot();
            }
        }
        timer += Time.deltaTime;
    }
}
