using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] MeleWeaponDamageCollider meleeDamageCollider;

    private void Awake()
    {
        meleeDamageCollider = GetComponentInChildren<MeleWeaponDamageCollider>();
    }

    public void SetWeaponDamage(CharacterManager characterWieldingWeapon, WeaponItem weapon)
    {
        meleeDamageCollider.characterCausingDamage = characterWieldingWeapon;
        meleeDamageCollider.physicalDamage   = weapon.physicalDamage;
        meleeDamageCollider.magicDamage      = weapon.magicDamage;
        meleeDamageCollider.fireDamage       = weapon.fireDamage;
        meleeDamageCollider.lightningDamage  = weapon.lightningDamage;
        meleeDamageCollider.holyDamage       = weapon.holyDamage;
    }
}
