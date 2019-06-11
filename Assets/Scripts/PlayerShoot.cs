using UnityEngine;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : MonoBehaviour
{
    private PlayerWeapon currentWeapon;
    private WeaponManager weaponManager;

    public Camera cam;

    public LayerMask layerMask;

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

        Debug.Log("Remaining bullets: " + currentWeapon.bullets);

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, currentWeapon.range, layerMask))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Entity>().TakeDamage(currentWeapon.damage);
            }
        }

        if (currentWeapon.bullets <= 0)
        {
            weaponManager.Reload();
        }

    }
}
