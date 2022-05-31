using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5;
    [SerializeField]
    int playerHealth = 3;
    Vector3 position;
    Animator anim;
    [SerializeField]
    int health;
    Vector3 tempPos;

    [SerializeField]
    int maxHealth;
    public enum PlayerState
    {
        alive,
        respawinig,
        dead
    }
    [SerializeField]
    public PlayerState playerState;
    public PlayerHealth _playerHealth;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void InitlializePlayerHealth()
    {
        _playerHealth = new PlayerHealth();
    }
    void Update()
    {
        if (playerState !=PlayerState.alive) return;
        GetInputs();
        MovePlayer();
    }
    private void MovePlayer()
    {
        tempPos = transform.position;
        tempPos += position * moveSpeed * Time.deltaTime;
        tempPos.x = Mathf.Clamp(tempPos.x, -8, 8);
        tempPos.y = Mathf.Clamp(tempPos.y, -4, 0);
        transform.position = tempPos;
    }

    public void GetInputs()
    {
        position.x = Input.GetAxisRaw("Horizontal");
        position.y = Input.GetAxisRaw("Vertical");
    }
    public void GetDamage()
    {
        if (playerState == PlayerState.alive)
        {
            playerHealth -= 1;
            playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);
            if (playerHealth > 0)
            {
                UIManager.instance.updatelayerHealth?.Invoke(playerHealth);
                StartCoroutine(ServiceLocator.GetService<IPlayerSpawner>().RespawnPlayer());
            }
            else
            {
                GameManager.Instance.GameStatus(true);
                GameManager.Instance.controller.ChangeState(StateController.GameStates.end);
            }
        }
    }
    public void SetPlayerHealth(int _maxHealth)
    {
        maxHealth = health = _maxHealth;
        UIManager.instance.updatelayerHealth?.Invoke(health);
    }
    public void AddHealth()
    {
        health += 1;
        health = Mathf.Clamp(health, 1, maxHealth);
        UIManager.instance.updatelayerHealth?.Invoke(health);
    }
}
