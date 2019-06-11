using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A component to handle Player UI.
/// </summary>
public class PlayerUI : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text ammoText;

    private Entity player;                  // player entity reference
    private WeaponManager weaponManager;    // WeaponManager reference

    /// <summary>
    /// A method to set the player and weaponManager references.
    /// </summary>
    /// <param name="_player">A reference to the player entity.</param>
    public void SetPlayer(Entity _player)
    {
        player = _player;
        weaponManager = player.GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        SetHealthAmount(player.GetHealthPercentage());
        SetAmmoAmount(weaponManager.GetCurrentWeapon().bullets);
    }

    /// <summary>
    /// A method to set the health bar GUI value.
    /// </summary>
    /// <param name="_amount">Health in percentage between 0 and 1.</param>
    private void SetHealthAmount(float _amount)
    {
        healthBar.value = _amount;
    }

    /// <summary>
    /// A method to set the ammo GUI text.
    /// </summary>
    /// <param name="_amount">Ammo amount.</param>
    private void SetAmmoAmount(int _amount)
    {
        ammoText.text = _amount.ToString();
    }
}
