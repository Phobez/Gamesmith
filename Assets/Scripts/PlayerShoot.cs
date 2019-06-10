using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public PlayerWeapon weapon;

    public Camera cam;

    [SerializeField]
    private LayerMask layerMask;

    private RaycastHit hit;

    private void Start()
    {
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced!");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, layerMask))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Entity>().TakeDamage(weapon.damage);
            }
        }
    }
}
