using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsManager : MonoBehaviour
{
    //Process Instance effect (damage, heal)

    //process timed effects (Poison, Build ups)

    //Process static effects (Buffs, talismans)

    CharacterManager character;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public virtual void ProcessInstantEffects( InstantCharacterEffect effect)
    {
        effect.ProcessEffect(character);
    }

}
