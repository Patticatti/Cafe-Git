using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private Vector2 mousePosition;

    private float atkSpeed = 0.5f;
    private float timer;

    private float distance;

    private GameObject bulletcopy;

    private void LateUpdate()
    {
        if ((distance < 10) && (timer >= atkSpeed) && (Input.GetMouseButton(0)))
        {
            Debug.Log("shoot");
            timer = 0;
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
        timer += Time.deltaTime;
    }
    /*
    private IEnumerator ManualShoot()
    {
        while ((!Input.GetMouseButton(0)))
        {
            yield return null;
        }
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

        private IEnumerator ManualShoot()
        {
            for (; ; )
            {
                if (Input.GetMouseButton(0))
                {
                    waitForShoot = false;
                    Instantiate(bullet, bulletPos.position, Quaternion.identity);
                    break;
                }
                yield return null;
            }
            yield return null;
        }*/
}