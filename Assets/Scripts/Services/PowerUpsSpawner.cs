using System.Collections;
using UnityEngine;
public interface IPowerUpsSpawner
{
    void InitializePowerUpPsawner(PowerUpConfig powerUpConfig);
    void StartSpawn();
}
public class PowerUpSpawner : IPowerUpsSpawner
{
    [SerializeField]
    GameObject[] powerPrefabs;
    [SerializeField]
    Vector2 xValue;
    [SerializeField]
    float yValue = 0;
    int currentIndex = 0;
    Vector2 spawnTimeRange;
    Coroutine SpawnRoutine;
    public PowerUpSpawner()
    {
        GameManager.Instance.resetGame += ResetSpawner;
    }
    ~PowerUpSpawner()
    {
        GameManager.Instance.resetGame -= ResetSpawner;
    }
    public void InitializePowerUpPsawner(PowerUpConfig powerUpConfig)
    {
        powerPrefabs = powerUpConfig.powerPrefabs;
        xValue = powerUpConfig.xValue;
        yValue = powerUpConfig.yValue;
        spawnTimeRange = powerUpConfig.spawnTimeRange;
    }
    public void StartSpawn()
    {
        SpawnRoutine = GameManager.Instance.StartCoroutine(SpawnPowerups(0));
    }
    public void ResetSpawner()
    {
        GameManager.Instance.StopCoroutine(SpawnRoutine);
    }
    IEnumerator SpawnPowerups(float waitSpawnTime)
    {
        while (waitSpawnTime >= 0)
        {
            waitSpawnTime -= Time.deltaTime;
            yield return null;
        }
        Vector3 position = new Vector3(Random.Range(xValue.x, xValue.y), yValue, 0);

        if (currentIndex != 0)
            currentIndex = Random.Range(1, powerPrefabs.Length);
        GameObject go = Object.Instantiate(powerPrefabs[currentIndex], position, Quaternion.identity);
        currentIndex = 1;
        if (go.GetComponent<Weapon>())
            go.GetComponent<Weapon>().MoveWeapon();
        SpawnRoutine = GameManager.Instance.StartCoroutine(SpawnPowerups(Random.Range(spawnTimeRange.x, spawnTimeRange.y)));
    }
}
