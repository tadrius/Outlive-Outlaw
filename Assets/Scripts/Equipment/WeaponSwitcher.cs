using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class WeaponSwitcher : MonoBehaviour
{

    [SerializeField] int currentWeaponIndex = 0;
    [SerializeField] ResourceDisplay ammoDisplay;

    int previousWeaponIndex;
    StarterAssetsInputs inputs;
    List<Weapon> weapons;
    AmmoStorage ammoStorage;

    private void Awake()
    {
        inputs = GetComponentInParent<StarterAssetsInputs>();
        weapons = GetComponentsInChildren<Weapon>(true).ToList();
        ammoStorage = GetComponentInParent<AmmoStorage>();

        previousWeaponIndex = currentWeaponIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetWeaponActive();
    }

    void Update()
    {
        if (ammoDisplay != null)
        {
            UpdateAmmoDisplay();
        }
        ProcessInput();
    }

    private void UpdateAmmoDisplay()
    {
        if (ammoDisplay != null)
        {
            ammoDisplay.resourceAmounts[0] = weapons[currentWeaponIndex].LoadedAmmoAmount;
            ammoDisplay.resourceAmounts[1] = ammoStorage.GetAmmoAmount(weapons[currentWeaponIndex].AmmoType);
            ammoDisplay.UpdateDisplay();
        }
    }

    void ProcessInput()
    {
        if (inputs.cycleEquipment < 0f)
        {
            currentWeaponIndex = Mathf.Min(weapons.Count - 1, currentWeaponIndex + 1);
        }
        else if (inputs.cycleEquipment > 0f)
        {
            currentWeaponIndex = Mathf.Max(0, currentWeaponIndex - 1);
        }

        if (previousWeaponIndex != currentWeaponIndex)
        {
            SetWeaponActive();
        }

    }

    void SetWeaponActive()
    {
        previousWeaponIndex = currentWeaponIndex;
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].gameObject.SetActive(i == currentWeaponIndex);
        }
    }
}
