using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Enemies
{
    public List<GameObject> enemies = new List<GameObject>();
}
public interface IEnemySpawner
{
    int totalEnemies { get; }
    void InitializeEnemySpawner(EnemyConfig enemyConfig);
    void StartSpawningEnemies();
}
public class EnemySpawner : IEnemySpawner
{
    public int totalEnemies { get; private set; }

    GameObject[] enemyPrefabs;
    Vector2Int xValue;
    int enemyCountRange = 4;
    UIManager uiManager;
    Vector2Int SpawnRange;
    LineRenderer[] lines;
    Enemies[] enemies = new Enemies[5];
    Dictionary<int, System.Action> Actions = new Dictionary<int, System.Action>();
    bool isBossSpawned = false;
    GameObject bossPrefab;
    GameObject bossGO;
    Coroutine spawnRoutine;
    float bossHealth;
    public EnemySpawner()
    {
        GameManager.Instance.resetGame += ResetEnemySpawner;
    }
    ~EnemySpawner()
    {
        GameManager.Instance.resetGame += ResetEnemySpawner;
    }
    public void ResetEnemySpawner()
    {
        GameManager.Instance.StopCoroutine(spawnRoutine);
        for (int j = 0; j < enemies.Length; j++)
        {
            for (int i = 0; i < enemies[j].enemies.Count; i++)
            {
                enemies[j].enemies[i].SetActive(false);
            }
        }
        Object.Destroy(bossGO);
        isBossSpawned = false;
    }
    public void InitializeEnemySpawner(EnemyConfig enemyConfig)
    {
        enemyPrefabs = enemyConfig.enemyPrefabs;
        enemyCountRange = enemyConfig.enemyCountRange;
        totalEnemies = enemyConfig.totalEnemies;
        SpawnRange = enemyConfig.SpawnRange;
        bossPrefab = enemyConfig.bossPrefab;
        bossHealth = enemyConfig.bossHealth;
        xValue = enemyConfig.minVal;

        InitializeEnemies();

        uiManager = Object.FindObjectOfType<UIManager>();
        uiManager.slider.maxValue = enemyConfig.bossHealth;
        uiManager.slider.value = enemyConfig.bossHealth;
        lines = GameManager.Instance.pathParent.GetComponentsInChildren<LineRenderer>(true);
    }
    public void StartSpawningEnemies()
    {
        spawnRoutine = GameManager.Instance.StartCoroutine(SpawnEnemiesRandomly());
        uiManager.slider.gameObject.SetActive(false);
    }
    private void InitializeEnemies()
    {
        Transform parent = new GameObject("EnemySpawnParent").GetComponent<Transform>();

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = new Enemies();
        }

        for (int i = 0; i < enemyCountRange * 3; i++)
        {
            enemies[0].enemies.Add(Object.Instantiate(enemyPrefabs[0], parent));
            enemies[1].enemies.Add(Object.Instantiate(enemyPrefabs[1], parent));
            enemies[2].enemies.Add(Object.Instantiate(enemyPrefabs[2], parent));
            enemies[3].enemies.Add(Object.Instantiate(enemyPrefabs[3], parent));
            enemies[4].enemies.Add(Object.Instantiate(enemyPrefabs[4], parent));

            enemies[0].enemies[i].SetActive(false);
            enemies[1].enemies[i].SetActive(false);
            enemies[2].enemies[i].SetActive(false);
            enemies[3].enemies[i].SetActive(false);
            enemies[4].enemies[i].SetActive(false);
        }
        Actions.Add(0, () => { SpawnEnemy(enemies[0].enemies); });
        Actions.Add(1, () => { SpawnEnemy(enemies[1].enemies); });
        Actions.Add(2, () => { SpawnEnemy(enemies[2].enemies); });
        Actions.Add(3, () => { SpawnEnemy(enemies[3].enemies); });
        Actions.Add(4, SpawnWonderingEnemy);

    }



    private void SpawnWonderingEnemy()
    {
        EnemyWonder enemyWonder = GetAvailableEnemy(enemies[4].enemies).GetComponent<EnemyWonder>();
        enemyWonder.SetWonderingPath(lines[Random.Range(0, lines.Length)]);
    }
    float randimTime;
    IEnumerator SpawnEnemiesRandomly()
    {
        randimTime = Random.Range(SpawnRange.x, SpawnRange.y);
        while (randimTime >= 0)
        {
            randimTime -= Time.deltaTime;
            yield return null;
        }
        if (!uiManager.inGameUIHandler.IsAllEnimiesDied())
        {
            GetSpawnLength();
            spawnRoutine = GameManager.Instance.StartCoroutine(SpawnEnemiesRandomly());
        }
        else if (!isBossSpawned)
        {
            SpawnBoss();
        }
    }
    public void GetSpawnLength()
    {
        int _case = Random.Range(0, enemies.Length);
        Actions[_case].Invoke();
    }
    private void SpawnBoss()
    {
        bossGO = Object.Instantiate(bossPrefab);
        bossGO.GetComponent<BossMovement>().health = bossHealth;
        isBossSpawned = true;
        uiManager.slider.gameObject.SetActive(true);
    }
    Vector3 spawnPosition;
    int enemyCount = 0;
    void SpawnEnemy(List<GameObject> enemies)
    {
        enemyCount = Random.Range(1, this.enemyCountRange);
        for (int i = 0; i < enemyCount; i++)
        {
            spawnPosition = new Vector3(Random.Range(xValue.x, xValue.y), 5.1f, 0);
            Debug.Log(spawnPosition);
            GameObject enemy = GetAvailableEnemy(enemies);
            enemy.transform.position = spawnPosition;
            enemy.GetComponent<EnemyMovement>().MoveEnemy();
            enemy.SetActive(true);
        }
    }
    GameObject GetAvailableEnemy(List<GameObject> enemyList)
    {
        foreach (GameObject enemy in enemyList)
        {
            if (!enemy.activeInHierarchy)
                return enemy;
        }
        return default;
    }
}
