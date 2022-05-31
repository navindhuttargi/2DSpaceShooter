using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BossMovement : BaseEnemyMovement
{
    [SerializeField]
    Ease ease;
    void Start()
    {
        SetDefaultPosition();
    }
    void SetDefaultPosition()
    {
        transform.position = Vector3.up * 7.8f;
        GetToCenterInY();
    }
    void GetToCenterInY()
    {
        transform.DOMoveY(3.5f, 2).OnComplete(MoveToLeft);
    }
    void MoveToLeft()
    {
        transform.DOMoveX(-6.5f, 1).OnComplete(MoveVerticallyInLoops);
    }
    void MoveVerticallyInLoops()
    {
        transform.DOMoveX(6.5f, 4).SetLoops(4, LoopType.Yoyo).OnComplete(GetToCenterInX);
    }
    void GetToCenterInX()
    {
        transform.DOMoveX(0, 2).OnComplete(MoveBackWard);
    }
    public void MoveBackWard()
    {
        transform.DOMoveY(7.8f, 4).OnComplete(GetToFront);
    }

    private void GetToFront()
    {
        transform.DOMoveY(-3.5f, 4).SetEase(ease).OnComplete(GetToCenterInY);
    }
    public override void GetDamage(int damage)
    {
        health -= damage;
        uimanager.slider.value = health;
        uimanager.updateScore(damage);
        if (IsDead())
        {
            ResetEnemy();
            uimanager.updateKills();
            GameManager.Instance.GameStatus(true);
            GameManager.Instance.controller.ChangeState(StateController.GameStates.end);
        }
    }
}
