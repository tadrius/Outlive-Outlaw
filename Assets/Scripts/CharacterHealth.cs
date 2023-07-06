using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] int MaxHitPoints = 100;
    int currentHitPoints;

    public int CurrentHitPoints { get { return currentHitPoints; } }

    private void Start()
    {
        currentHitPoints = MaxHitPoints;
    }

    public void TakeDamage(int amount)
    {
        AddHealth(-amount);
    }


    public void Heal(int amount)
    {

        AddHealth(amount);
    }

    private void AddHealth(int amount)
    {
        currentHitPoints = Mathf.Min(currentHitPoints + amount, MaxHitPoints);
        if (currentHitPoints <= 0)
        {
            Die();
        } else
        {
            Debug.Log($"Health: {currentHitPoints}");
        }
    }
    
    private void Die()
    {
        GetComponent<DeathHandler>().HandleDeath();
    }

}
