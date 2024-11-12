using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleWeaponDamageCollider : DamageCollider
{
    [Header("Attacking Character")]
    public CharacterManager characterCausingDamage;
}
