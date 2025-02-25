using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsManager : MonoBehaviour
{
    //Process Instance effect (damage, heal)

    //process timed effects (Poison, Build ups)

    //Process static effects (Buffs, talismans)

    CharacterManager character;

    [Header("VFX")]
    [SerializeField] GameObject bloodSplateterVFX;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public virtual void ProcessInstantEffects( InstantCharacterEffect effect)
    {
        effect.ProcessEffect(character);
    }

    public void PlayBloodSplatterVFX(Vector3 contactPoint)
    {
        if (bloodSplateterVFX != null)
        {
            GameObject bloodSplatter = Instantiate(bloodSplateterVFX, contactPoint, Quaternion.identity);
        }
        else
        {
            //put other vfx other then blood splatter
            GameObject bloodSplatter = Instantiate(WorldCharacterEffectsManager.instance.bloodSplatterVFX, contactPoint, Quaternion.identity);
        }
    }

}
