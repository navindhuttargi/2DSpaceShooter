using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Config/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public GameObject[] enemyPrefabs;
    public float repeatRate = 2;
    public Vector2Int minVal;
    public int enemyCountRange = 4;
    public int totalEnemies = 10;
    public int enemyPoolLength;
    public Vector2Int SpawnRange;
    public GameObject bossPrefab;
    public float bossHealth = 1000;
}
