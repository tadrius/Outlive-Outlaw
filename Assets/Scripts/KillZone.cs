using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered kill zone.");
        CharacterHealth health = other.GetComponentInParent<CharacterHealth>();
        if (health != null )
        {
            health.TakeDamage(health.CurrentHitPoints);
        }
    }
}
