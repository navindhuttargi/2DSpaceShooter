using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    protected SpriteRenderer[] sprites;
    protected virtual void Awake()
    {
        GetSprites();
    }
    private void OnEnable()
    {
        transform.DOMoveY(-5.5f, 5).OnComplete(() => { gameObject.SetActive(false); });
    }
    public void GetSprites()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }
}
