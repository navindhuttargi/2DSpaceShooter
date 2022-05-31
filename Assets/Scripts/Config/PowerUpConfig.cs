using UnityEngine;
[CreateAssetMenu(fileName ="PowerUpConfig",menuName = "Config/PowerUpConfig")]
public class PowerUpConfig : ScriptableObject
{
    public GameObject[] powerPrefabs;
    public Vector2Int xValue;
    public float yValue = 0;
    public Vector2 spawnTimeRange;
}
