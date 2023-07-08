using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPowerPickup : MonoBehaviour
{
    [SerializeField] float powerAmount = 25;

    private void OnTriggerEnter(Collider other)
    {
        Character player = other.GetComponentInParent<Character>();
        if (player == null) { return; }
        
        FlashlightPower flashlightPower = player.GetComponentInChildren<FlashlightPower>();
        if (flashlightPower == null) { return; }

        flashlightPower.AddPower(powerAmount);
        Destroy(gameObject);
    }
}
