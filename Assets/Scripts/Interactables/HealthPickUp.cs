using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DOMoveY(-5.5f, 5).OnComplete(() => { gameObject.SetActive(false); });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            //collision.GetComponent<PlayerController>().AddHealth();
            collision.GetComponent<PlayerController>()._playerHealth.AddHealth();
            Destroy(gameObject);
        }
    }
}
