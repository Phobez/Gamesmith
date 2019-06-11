using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public PlayerWeapon primaryWeapon;
    public Transform weaponHolder;

    private PlayerWeapon currentWeapon;

    // Start is called before the first frame update
    private void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void EquipWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;

        GameObject _weaponInstance = (GameObject) Instantiate(_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponInstance.transform.SetParent(weaponHolder);
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
