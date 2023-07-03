using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAttack : MonoBehaviour
{
    [SerializeField] int damage = 10;

    CharacterHealth target;

    readonly public static string playerTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(playerTag).GetComponent<CharacterHealth>();
    }

    public void AttackHitEvent()
    {
        if (target != null)
        {
            DealDamage();
        }
    }

    void DealDamage()
    {
        target.TakeDamage(damage);
    }

}
