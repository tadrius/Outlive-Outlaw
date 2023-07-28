using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.WSA;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] int MaxHitPoints = 100;
    [SerializeField] ResourceDisplay healthDisplay;

    int currentHitPoints;

    DamageScreenDisplay[] damageDisplays;
    int currentDamageDisplayIndex = 0;

    public int CurrentHitPoints { get { return currentHitPoints; } }

    private void Awake()
    {
        damageDisplays = FindObjectsOfType<DamageScreenDisplay>();
    }

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
        ShowDamageDisplay();
    }

    void ShowDamageDisplay()
    {
        if (currentDamageDisplayIndex >= damageDisplays.Count())
        {
            currentDamageDisplayIndex = 0;
        }
        damageDisplays[currentDamageDisplayIndex].ShowDisplay();
        currentDamageDisplayIndex++;
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
        }
    }
    
    public void Die()
    {
        foreach (DamageScreenDisplay display in damageDisplays)
        {
            display.ShowDisplay();
        }
        GetComponent<DeathHandler>().HandleDeath();
    }

}
