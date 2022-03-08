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
}
