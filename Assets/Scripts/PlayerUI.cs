using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text ammoText;

    private Entity player;
    private WeaponManager weaponManager;

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

    private void SetHealthAmount(float _amount)
    {
        healthBar.value = _amount;
    }

    private void SetAmmoAmount(int _amount)
    {
        ammoText.text = _amount.ToString();
    }
}
