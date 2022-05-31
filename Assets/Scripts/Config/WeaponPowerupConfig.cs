using UnityEngine;
[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Config/WeaponConfig")]
public class WeaponPowerupConfig : ScriptableObject
{
    [SerializeField]
    float bulletSpeed;
    [SerializeField]
    float startTimeBtwShots;
    [HideInInspector]
    public float _bulletSpeed => bulletSpeed;
    [HideInInspector]
    public float _startTimeBtwShots=>startTimeBtwShots;
}
