using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatBar : MonoBehaviour
{
    private Slider slider;
    // Var to scale bar side depending endurance stat
    // Will add yellow bar to show how much stamina lost

    protected virtual void Awake()
    {     
        slider = GetComponent<Slider>();       
    }

    public virtual void SetStat(int newValue)
    {
        slider.value = newValue;
    }

    public virtual void SetMaxStat(int maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }

}
