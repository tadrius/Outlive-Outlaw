using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoStorage : MonoBehaviour
{
    [SerializeField] List<AmmoSlot> slots;

    Dictionary<AmmoType, AmmoSlot> ammoSlotByType;

    void Awake()
    {
        ammoSlotByType = CreateAmmoSlotDictionary();
    }

    Dictionary<AmmoType, AmmoSlot> CreateAmmoSlotDictionary()
    {
        Dictionary<AmmoType, AmmoSlot> ammoSlotByType = new Dictionary<AmmoType, AmmoSlot>();
        foreach (AmmoSlot slot in slots)
        {
            ammoSlotByType.Add(slot.type, slot);
        }
        return ammoSlotByType;
    }

    public void AddAmmo(AmmoType type, int amount)
    {
        AmmoSlot slot = ammoSlotByType[type];
        if (slot == null)
        {
            slots.Add(new AmmoSlot(type, amount));
            ammoSlotByType.Add(type, new AmmoSlot(type, amount));
        }

        slot.amount += amount;
    }

    public int RemoveAmmo(AmmoType type, int amount)
    {
        AmmoSlot slot = ammoSlotByType[type];
        if (slot == null)
        {
            return 0;
        }

        if (slot.amount < amount)
        {
            int remainingAmmo = slot.amount;
            slot.amount = 0;
            return remainingAmmo;
        }

        slot.amount -= amount;
        return amount;
    }

    public int GetAmmoAmount(AmmoType type)
    {
        return ammoSlotByType[type].amount;
    }

    [System.Serializable]
    class AmmoSlot
    {
        public AmmoType type;
        public int amount;

        public AmmoSlot(AmmoType type, int amount)
        {
            this.type = type;   
            this.amount = amount;
        }
    }
}
