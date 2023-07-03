using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float range = 10f;
    [SerializeField] float recoverySpeed = .1f;
    [SerializeField] float reloadSpeed = 1f;
    [SerializeField] int damage = 25;
    [SerializeField] ActionType actionType = ActionType.Automatic;
    [SerializeField] AmmoType ammoType = AmmoType.Rifle;
    [SerializeField] int ammoCapacity = 10;
    [SerializeField] List<ParticleSystem> attackParticles;
    [SerializeField] ParticleSystem impactParticles;

    Camera playerCamera;
    StarterAssetsInputs input;
    AmmoStorage ammoStorage;
    int loadedAmmoAmount;
    WeaponState state;

    public enum ActionType { Automatic, Manual }

    enum WeaponState { Ready, Recovery, Reload, Delay }

    void Awake()
    {
        playerCamera = GetComponentInParent<Camera>();
        input = GetComponentInParent<StarterAssetsInputs>();
        ammoStorage = GetComponentInParent<AmmoStorage>();
    }

    void Start()
    {
        loadedAmmoAmount = ammoCapacity;
        state = WeaponState.Ready;
    }

    void Update()
    {
        switch (state)
        {
            case WeaponState.Ready:
                ProcessInput();
                break;
            case WeaponState.Recovery:
                StartCoroutine(Recover());
                break;
            case WeaponState.Reload:
                StartCoroutine(Reload());
                break;
            case WeaponState.Delay:
                break;
        }
        input.attack = false;
    }

    void ProcessInput()
    {
        if ((actionType == ActionType.Automatic && input.autoAttack) 
            || (actionType == ActionType.Manual && input.attack))
        {
            Attack();
        }
        // TODO - add reload input
    }

    void Attack()
    {
        if (0 < loadedAmmoAmount)
        {
            loadedAmmoAmount--;
            PlayAttackFX();
            RaycastHit hit = RaycastAttack();
            if (hit.collider)
            {
                PlayHitFX(hit);
                ApplyAttackEffects(hit);
            }
            state = WeaponState.Recovery;
        } else
        {
            state = WeaponState.Reload;
        }
    }

    IEnumerator Reload()
    {
        state = WeaponState.Delay;
        yield return new WaitForSeconds(reloadSpeed);
        loadedAmmoAmount += ammoStorage.RemoveAmmo(ammoType, ammoCapacity - loadedAmmoAmount);
        state = WeaponState.Ready;
    }

    IEnumerator Recover()
    {
        state = WeaponState.Delay;
        yield return new WaitForSeconds(recoverySpeed);
        state = WeaponState.Ready;
    }

    RaycastHit RaycastAttack()
    {
        RaycastHit hit;
        Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range);
        return hit;
    }

    void ApplyAttackEffects(RaycastHit hit)
    {
        CreatureHealth target = hit.transform.GetComponentInParent<CreatureHealth>();
        if (target != null)
        {
            target.TakeDamage(damage);
        }
    }

    void PlayAttackFX()
    {
        foreach (ParticleSystem system in attackParticles)
        {
            system.Play();
        }
    }

    void PlayHitFX(RaycastHit hit)
    {
        impactParticles.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
        impactParticles.Play();
    }

    IEnumerator DelayedDestroy(GameObject gameObject, float secondsDelay)
    {
        yield return new WaitForSeconds(secondsDelay);
        Destroy(gameObject);
    }

}
