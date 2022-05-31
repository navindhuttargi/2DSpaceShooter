using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyMovement : BaseEnemyMovement
{
    [SerializeField]
    float duration = 5;
    private const int rangeLimit = 6;
    //[SerializeField]
    //float health = 10;
    //ScoreCounter scoreCounter;
    Sequence sequence;
    //[SerializeField]
    //float speed = 5;
    public void InitializeEnemy()
    {
        sequence = DOTween.Sequence();
    }
    public void MoveEnemy()
    {
        duration = Random.Range(3, rangeLimit);
        sequence.Append(transform.DOMoveY(-6, 6).OnComplete(ResetEnemy));
        uimanager = FindObjectOfType<UIManager>();
    }
    //private void ResetEnemy()
    //{
    //    health = 10;
    //    gameObject.SetActive(false);
    //    sequence.Kill();
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.transform.GetComponent<PlayerController>())
    //    {
    //        Debug.Log(collision.gameObject.name);
    //        collision.transform.GetComponent<PlayerController>().GetDamage();
    //    }
    //}
    //public void GetDamage(int damage)
    //{
    //    health -= damage;
    //    scoreCounter.UpdateScore(damage);
    //    if (health <= 0)
    //    {
    //        scoreCounter.UpdateEnemyKills();
    //        ResetEnemy();
    //    }
    //}
}
