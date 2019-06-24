using UnityEngine;

/// <summary>
/// A component to handle player shooting.
/// </summary>
[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : MonoBehaviour
{
    private Weapon currentWeapon;
    private WeaponManager weaponManager;    // WeaponManager reference

    public Camera cam;                      // player camera reference

    public LayerMask layerMask;             // enemy team layer mask

    // cached variables
    private RaycastHit hit;

    private void Start()
    {
        weaponManager = GetComponent<WeaponManager>();

        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced!");
            this.enabled = false;
        }
    }

    private void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();

        if (currentWeapon.bullets < currentWeapon.maxBullets)
        {
            if (Input.GetButtonDown("Reload"))
            {
                weaponManager.Reload();
                return;
            }
        }

        if (currentWeapon.fireRate <= 0.0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0.0f, 1.0f / currentWeapon.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }
    }

    private void Shoot()
    {
        if (weaponManager.isReloading)
        {
            return;
        }

        if (currentWeapon.bullets <= 0)
        {
            weaponManager.Reload();
            return;
        }

        currentWeapon.bullets--;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, currentWeapon.range, layerMask))
        {
            if (hit.transform.gameObject.CompareTag("EnemyTeam"))
            {
                hit.transform.GetComponent<Entity>().TakeDamage(currentWeapon.damage);
            }
            Debug.Log("hit");
        }

        if (currentWeapon.bullets <= 0)
        {
            weaponManager.Reload();
        }

    }
}
