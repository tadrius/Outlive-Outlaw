using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AmmoPickup : MonoBehaviour
{

    [SerializeField] int ammoAmount = 10;
    [SerializeField] AmmoType ammoType;

    private void OnTriggerEnter(Collider other)
    {
        AmmoStorage playerAmmo = other.GetComponentInParent<AmmoStorage>();
        if (playerAmmo == null)
        {
            return;
        }

        playerAmmo.AddAmmo(ammoType, ammoAmount);
        Destroy(gameObject);
    }
}
