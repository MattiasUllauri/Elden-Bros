using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [Header("Damage")]
    public float physicalDamage = 0;    // Standard/Strike/Slash/Pierce
    public float magicDamage = 0;
    public float fireDamage = 0;
    public float holyDamage = 0;
    public float lightningDamage = 0;

    [Header("Contact Point")]
    private Vector3 contactPoint;

    [Header("Character Damaged")]
    protected List<CharacterManager> charactersDamage = new List<CharacterManager>();

    private void OnTriggerEnter(Collider other)
    {
         CharacterManager damageTargert = other.GetComponent<CharacterManager>();

        if (damageTargert != null )
        {
            contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

            // friendly fire

            // check blocking

            // check invulnerable

            DamageTarget(damageTargert);
        }
    }

    protected virtual void DamageTarget(CharacterManager damageTarget)
    {
        if (charactersDamage.Contains(damageTarget))
            return;

        charactersDamage.Add(damageTarget);

        TakeDamage damageEffect = Instantiate(WorldCharacterEffectsManager.Instance.takeDamage);
        damageEffect.physicalDamage = physicalDamage;
        damageEffect.magicDamage = magicDamage;
        damageEffect.fireDamage = fireDamage;
        damageEffect.lightningDamage = lightningDamage;
        damageEffect.holyDamage = holyDamage;

        damageTarget.characterEffectsManager.ProcessInstantEffects(damageEffect);
    }
}