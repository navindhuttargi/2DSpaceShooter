using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletPool
{
    void InitializeBulletPool(GameObject _bulletPrefab, int _poolLength);
    Transform GetPlayerBullet();
    Transform GetEnemyBullet();
}

public class BulletPool : IBulletPool
{
    GameObject bulletPrefab;
    int poolLength;
    public List<Transform> playerBullets;
    public List<Transform> enemyBullets;
    Transform parent;
    public BulletPool()
    {
        GameManager.Instance.resetGame += ResetPool;
    }
    ~BulletPool()
    {
        GameManager.Instance.resetGame -= ResetPool;
    }
    public void InitializeBulletPool(GameObject _bulletPrefab,int _poolLength)
    {
        parent = new GameObject("BulletPool").transform;
        poolLength = _poolLength;
        bulletPrefab = _bulletPrefab;
        playerBullets = new List<Transform>();
        for (int i = 0; i < poolLength; i++)
        {
            playerBullets.Add(Object.Instantiate(bulletPrefab, parent.transform).GetComponent<Transform>());
            playerBullets[i].gameObject.SetActive(false);
        }
        enemyBullets = new List<Transform>();
        for (int i = 0; i < poolLength / 2; i++)
        {
            enemyBullets.Add(Object.Instantiate(bulletPrefab, parent.transform).GetComponent<Transform>());
            enemyBullets[i].gameObject.SetActive(false);
        }
    }
    public Transform GetPlayerBullet()
    {
        for (int i = 0; i < playerBullets.Count; i++)
        {
            if (!playerBullets[i].gameObject.activeInHierarchy)
            {
                return playerBullets[i];
            }
        }
        Transform rgb = Object.Instantiate(bulletPrefab, parent.transform).GetComponent<Transform>();
        rgb.gameObject.SetActive(false);
        playerBullets.Add(rgb);
        return rgb;
    }
    public Transform GetEnemyBullet()
    {
        for (int i = 0; i < playerBullets.Count; i++)
        {
            if (!enemyBullets[i].gameObject.activeInHierarchy)
            {
                return enemyBullets[i];
            }
        }
        Transform rgb = Object.Instantiate(bulletPrefab, parent.transform).GetComponent<Transform>();
        rgb.gameObject.SetActive(false);
        enemyBullets.Add(rgb);
        return rgb;
    }
    void ResetPool()
    {
        for (int i = 0; i < playerBullets.Count; i++)
        {
            playerBullets[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemyBullets.Count; i++)
        {
            enemyBullets[i].gameObject.SetActive(false);
        }
    }
}
