using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyMovement : MonoBehaviour
{
    public float health = 10;
    public float initialHealth=0;
    [SerializeField]
    protected UIManager uimanager;
    [SerializeField]
    protected float speed = 5;
    private void Awake()
    {
        uimanager = FindObjectOfType<UIManager>();
        initialHealth = health;
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<PlayerController>())
        {
            //collision.transform.GetComponent<PlayerController>().GetDamage();
            Debug.Log(gameObject.name);
            collision.transform.GetComponent<PlayerController>()._playerHealth.GetDamage();
        }
    }
    public virtual void GetDamage(int damage)
    {
        health -= damage;
        uimanager.updateScore(damage);

        if (IsDead())
        {
            uimanager.updateKills();
            ResetEnemy();
        }
    }
    protected bool IsDead()
    {
        return health <= 0;
    }
    protected void ResetEnemy()
    {
        health = initialHealth;
        gameObject.SetActive(false);
    }
}
