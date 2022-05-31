using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBullet : MonoBehaviour
{
    public float waitTimeToFire = 1;
    public float fireTime = 0;
    Transform player;
    [SerializeField]
    Weapon defaultWeapon;

    private void Awake()
    {
        //player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
    }
    void Update()
    {
        FireBullet();
    }

    private void FireBullet()
    {
        if (fireTime > defaultWeapon.powerup._startTimeBtwShots)
        {
            defaultWeapon.FireBullet(false);
            fireTime = 0;
        }
        else
        {
            fireTime += Time.deltaTime;
        }
    }
}
