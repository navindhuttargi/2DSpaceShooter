using UnityEngine;

public class BulletFire : MonoBehaviour
{
    [SerializeField]
    Transform[] shootPoints;
    [SerializeField]
    BulletPool bulletPool;
    [SerializeField]
    float timeBtwShots, startTimeBtwShots;
    [SerializeField]
    Weapon defaultWeapon;
    [SerializeField]
    Weapon currentEquippedWeapon;
    [SerializeField]
    Transform bulletHolder;
    PlayerController playerController;

    private void Awake()
    {
        //    currentEquippedWeapon = defaultWeapon;
        //    SetDefaultWeapon();
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {
        if ((Input.GetKey(KeyCode.Space) && currentEquippedWeapon != null && playerController.playerState == PlayerController.PlayerState.alive))
        {
            currentEquippedWeapon.FireBullet();
        }
    }
    public void SetDefaultWeapon()
    {
        currentEquippedWeapon = defaultWeapon;
        currentEquippedWeapon.Equip(bulletHolder, bulletPool);
    }
    public void ChangeWeapon(Weapon weapon)
    {
        if (currentEquippedWeapon != null)
            currentEquippedWeapon.UnEquip();
        else
            defaultWeapon = weapon;
        currentEquippedWeapon = weapon;
        currentEquippedWeapon.Equip(bulletHolder, bulletPool);
    }
}
