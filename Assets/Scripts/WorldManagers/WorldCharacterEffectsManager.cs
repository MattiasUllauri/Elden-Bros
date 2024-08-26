using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCharacterEffectsManager : MonoBehaviour
{
    public static WorldCharacterEffectsManager Instance;

    [Header("TakeDamage")]
    public TakeDamage takeDamage;

    [SerializeField] List<InstantCharacterEffect> instantEffects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GenerateEffectIDs();
    }

    private void GenerateEffectIDs()
    {
        for (int i = 0; i < instantEffects.Count; ++i)
        {
            instantEffects[i].instantEffectID = i;
        }
    }
}
