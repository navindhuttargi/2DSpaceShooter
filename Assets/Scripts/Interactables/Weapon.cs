using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Weapon : MonoBehaviour
{
    public WeaponPowerupConfig powerup;
    Sequence sequence;
    SpriteRenderer[] sprites;
    IBulletPool bulletPool;
    [SerializeField]
    Transform[] shootPoints;
    float timeBetweenShots;
    private void Awake()
    {
        sequence = DOTween.Sequence();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        bulletPool = ServiceLocator.GetService<IBulletPool>();// FindObjectOfType<BulletPool>();
    }
    public void MoveWeapon()
    {
        
        sequence.Append(transform.DOMoveY(-5.5f, 5)).OnComplete(() => { gameObject.SetActive(false); });
    }
    public void Equip(Transform parent, BulletPool pool)
    {
        sequence.Kill();
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].enabled = false;
        }
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        transform.position = parent.position;
        transform.parent = parent;

    }
    public void FireBullet(bool isShootByPlayer = true)
    {
        if (!isShootByPlayer || timeBetweenShots <= 0)
        {
            for (int i = 0; i < shootPoints.Length; i++)
            {
                Transform bullet = GetBullet(isShootByPlayer, i);
                bullet.position = shootPoints[i].position;
                bullet.gameObject.SetActive(true);
                bullet.GetComponent<Bullet>().SetDirection(shootPoints[i], powerup._bulletSpeed, isShootByPlayer);
                timeBetweenShots = powerup._startTimeBtwShots;
            }
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
    Transform GetBullet(bool isShootByPlayer, int i)
    {
        if (isShootByPlayer)
            return bulletPool.GetPlayerBullet();
        return bulletPool.GetEnemyBullet();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
            collision.GetComponent<BulletFire>().ChangeWeapon(this);
    }
    public void UnEquip()
    {
        gameObject.SetActive(false);
    }
}
