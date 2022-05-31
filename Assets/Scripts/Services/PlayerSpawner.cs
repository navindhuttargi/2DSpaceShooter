using System.Collections;
using UnityEngine;
using static PlayerController;

public interface IPlayerSpawner
{
    GameObject player { get; }
    void PlayerInstantiate();
    void InitiliazePlayer(GameObject prefab, int health, Vector3 position);
    IEnumerator RespawnPlayer();
    void SpawnPlayer();
}
public class PlayerSpawner : IPlayerSpawner
{
    GameObject playerPrefab;
    int playerHealth;
    Vector3 spawnPosition;
    Animator anim;
    float respawnTime = 1.3f;
    public GameObject player { get; private set; }
    PlayerController playerController;

    public PlayerSpawner()
    {
        GameManager.Instance.resetGame += ResetPlayer;
    }
    ~PlayerSpawner()
    {
        GameManager.Instance.resetGame -= ResetPlayer;
    }
    public void PlayerInstantiate()
    {
        player = Object.Instantiate(playerPrefab);
        //player.GetComponent<PlayerController>().SetPlayerHealth(playerHealth);
        anim = player.GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();
        playerController.InitlializePlayerHealth();
        
        player.SetActive(false);
    }
    public void ResetPlayer()
    {
        player.SetActive(false);
    }

    public void InitiliazePlayer(GameObject prefab, int health, Vector3 position)
    {
        playerPrefab = prefab;
        playerHealth = health;
        spawnPosition = position;
    }
    float time = 0;
    public IEnumerator RespawnPlayer()
    {
        anim.SetTrigger("hit");
        time = respawnTime;
        if (playerHealth <= 0)
        {
            playerController.playerState = PlayerState.dead;
            player.SetActive(false);
            yield break;
        }
        playerController.playerState = PlayerState.respawinig;
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        player.GetComponent<SpriteRenderer>().enabled = false;
        time = 1;
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        player.GetComponent<SpriteRenderer>().enabled = true;
        playerController.playerState = PlayerState.alive;
        anim.SetTrigger("respawn");
        //time = respawnTime;
        //while (time > 0)
        //{
        //    time -= Time.deltaTime;
        //    yield return null;
        //}
    }

    public void SpawnPlayer()
    {
        playerController._playerHealth.SetPlayerHealth(playerHealth);
        player.transform.position = spawnPosition;
        player.transform.rotation = Quaternion.identity;
        player.SetActive(true);
    }
}
