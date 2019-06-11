using System.Collections;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public PlayerWeapon primaryWeapon;
    public Transform weaponHolder;

    private PlayerWeapon currentWeapon;
    private GameObject currentGraphics;
    private Animator currentGraphicsAnimator;

    private WaitForSeconds reloadDelay;

    public bool isReloading = false;

    // Start is called before the first frame update
    private void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    private void EquipWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;

        reloadDelay = new WaitForSeconds(currentWeapon.reloadTime);

        GameObject _weaponInstance = (GameObject) Instantiate(_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponInstance.transform.SetParent(weaponHolder);

        currentGraphics = _weaponInstance;
        currentGraphicsAnimator = _weaponInstance.GetComponent<Animator>();
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void Reload()
    {
        if (isReloading)
        {
            return;
        }

        StartCoroutine(CReload());
    }

    private IEnumerator CReload()
    {
        Debug.Log("Reloading...");

        isReloading = true;

        currentGraphicsAnimator.SetTrigger("Reload");

        yield return reloadDelay;

        currentWeapon.bullets = currentWeapon.maxBullets;

        isReloading = false;
    }
}
