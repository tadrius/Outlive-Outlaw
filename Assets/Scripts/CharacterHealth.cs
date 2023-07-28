using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] int MaxHitPoints = 100;
    [SerializeField] ResourceDisplay healthDisplay;

    int currentHitPoints;

    public int CurrentHitPoints { get { return currentHitPoints; } }

    private void Start()
    {
        currentHitPoints = MaxHitPoints;
    }

    private void Update()
    {
        UpdateHealthDisplay();
    }

    private void UpdateHealthDisplay()
    {
        if (healthDisplay != null)
        {
            healthDisplay.resourceAmounts[0] = currentHitPoints;
            healthDisplay.UpdateDisplay();
        }
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
