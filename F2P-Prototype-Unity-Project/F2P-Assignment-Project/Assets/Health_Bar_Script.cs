using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health_Bar_Script : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(int currentHealth, int MaxHealth)
    {
        slider.maxValue = MaxHealth;
        slider.value = currentHealth;
    }

    public void SetXP(int XP_Current, int XP_Required)
    {
        slider.maxValue = XP_Required;
        slider.value = XP_Current;
    }
}
