using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    public Transform direction;
    [SerializeField]
    int damage = 10;
    bool isShootByPlayer = false;
    SpriteRenderer sprite;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        Invoke("Deactivate", 2);
    }

    public void SetDirection(Transform t, float speed, bool shootByPlayer = true)
    {
        direction = t;
        isShootByPlayer = shootByPlayer;
        this.speed = speed;
        if (isShootByPlayer)
            sprite.color = Color.blue;
        else
            sprite.color = Color.red;
    }
    private void Update()
    {
        transform.position += direction.up * speed * Time.deltaTime;
    }
    private void FixedUpdate()
    {
        // transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<BaseEnemyMovement>())
        {
            if (isShootByPlayer == true)
            {
                collision.transform.GetComponent<BaseEnemyMovement>().GetDamage(damage);

                Deactivate();
            }
        }
        if (collision.transform.GetComponent<PlayerController>())
        {
            if (!isShootByPlayer)
            {
                //collision.transform.GetComponent<PlayerController>().GetDamage();
                Debug.Log(gameObject.name);
                collision.transform.GetComponent<PlayerController>()._playerHealth.GetDamage();
                Deactivate();
            }
        }
        if (collision.GetComponent<Bullet>() && isShootByPlayer)
            Deactivate();

    }
}
