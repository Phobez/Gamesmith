using System.Collections;
using UnityEngine;

/// <summary>
/// A component to manage weapon equipping and reload.
/// </summary>
public class WeaponManager : MonoBehaviour
{
    public Weapon primaryWeapon;
    public Transform weaponHolder;              // weapon holder game object reference

    private Weapon currentWeapon;
    private GameObject currentGraphics;
    private Animator currentGraphicsAnimator;

    private WaitForSeconds reloadDelay;

    public bool isReloading = false;

    // Start is called before the first frame update
    private void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    /// <summary>
    /// A method to equip a weapon.
    /// </summary>
    /// <param name="_weapon">Weapon to equip.</param>
    private void EquipWeapon(Weapon _weapon)
    {
        currentWeapon = _weapon;

        reloadDelay = new WaitForSeconds(currentWeapon.reloadTime);

        GameObject _weaponInstance = (GameObject)Instantiate(_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponInstance.transform.SetParent(weaponHolder);

        currentGraphics = _weaponInstance;
        currentGraphicsAnimator = _weaponInstance.GetComponent<Animator>();
    }

    /// <summary>
    /// A method to get the currently equipped weapon.
    /// </summary>
    /// <returns>Currently equipped weapon.</returns>
    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    /// <summary>
    /// A method to reload the weapon.
    /// </summary>
    public void Reload()
    {
        if (isReloading)
        {
            return;
        }

        StartCoroutine(CReload());
    }

    /// <summary>
    /// A coroutine to handle weapon reload.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CReload()
    {
        Debug.Log("Reloading...");

        isReloading = true;

        currentGraphicsAnimator.SetTrigger("Reload");

        yield return reloadDelay;

        Debug.Log("Setting bullets to max bullets: " + currentWeapon.maxBullets);
        currentWeapon.bullets = currentWeapon.maxBullets;
        Debug.Log("Bullets set to: " + currentWeapon.bullets);

        isReloading = false;
    }
}
