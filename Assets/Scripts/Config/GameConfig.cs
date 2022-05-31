using UnityEngine;
[CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/LevelConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Player")]
    public GameObject playerPrefab;
    public int playerHealth;
    public Vector3 playerSpawnPosition;

    [Header("Enemy")]
    public EnemyConfig enemyConfig;

    [Header("Bullet Pool")]
    public GameObject bulletPrefab;
    public int poolLength;

    [Header("PowerUp")]
    public PowerUpConfig powerUpConfig;

}