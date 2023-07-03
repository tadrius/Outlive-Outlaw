using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureHealth : MonoBehaviour
{

    [SerializeField] int hitPoints = 100;

    CreatureAI creatureAI;

    private void Awake()
    {
        creatureAI = GetComponent<CreatureAI>();
    }

    public void TakeDamage(int damage)
    {
        if (hitPoints > 0)
        {
            hitPoints -= damage;
            creatureAI.OnDamageTaken();
            if (hitPoints <= 0)
            {
                Die();
            }
        }

    }

    void Die()
    {
        creatureAI.Die();
    }

}
