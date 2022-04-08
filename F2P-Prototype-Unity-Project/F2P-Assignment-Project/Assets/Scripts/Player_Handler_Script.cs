using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Handler_Script : MonoBehaviour
{
    //Variables for Controls
    public Player_Controls pInput;

    //HP, Damage, and increase bonus
    public float MaxHealth = 10f;
    [HideInInspector] public float CurrentHealth;
    public float CurrentDamage;
    [HideInInspector] public float CurrentDamageBonus;
    public float IncreaseBonus = 5f;

    //UI Variables
    public Canvas LevelUI;

    public GameObject HealthButton;
    public GameObject DamageButton;
    public GameObject MeleeButton;
    public GameObject RangeButton;

    public TMP_Text HPDisp;
    public TMP_Text LVDisp;
    public TMP_Text XPDisp;

    public Health_Bar_Script healthBar;
    public Health_Bar_Script XPBar;

    public float LV = 1f;
    [HideInInspector] public float XP_Needed;
    [HideInInspector] public float XP_Acquired = 0f;
    public float XP_Scale = 1.2f;

    public Player_Equipped_Weapon PEW;
    public GameObject MeleeWeapon;
    public GameObject RangeWeapon;
    public GameObject Stick;

    private void Awake()
    {
        pInput = new Player_Controls();
    }

    private void Start()
    {
        XP_Needed = (float)(LV * 100 * XP_Scale);
        CurrentHealth = MaxHealth;
        CurrentDamage = CurrentDamageBonus + FindObjectOfType<Player_Equipped_Weapon>().CurrentEquippedWeapon.GetComponent<Weapon_Script>().WeaponData.Damage;
    }

    private void Update()
    {
        //HP
        HPDisp.text = CurrentHealth.ToString() + "/" + MaxHealth.ToString();
        healthBar.SetHealth((int)CurrentHealth, (int)MaxHealth);

        //XP
        XPDisp.text = XP_Acquired.ToString() + "/" + XP_Needed.ToString();
        XPBar.SetXP((int)XP_Acquired, (int)XP_Needed);

        //Level
        LVDisp.text = "LV " + LV.ToString();

        if (XP_Acquired >= XP_Needed)
        {
            LevelUp();
        }

        if(CurrentHealth <= 0)
        {
            //GG WP SOOON
            Destroy(gameObject);
        }
    }

    public void ChooseMelee()
    {
        PEW.CurrentEquippedWeapon = MeleeWeapon;
        MeleeWeapon.SetActive(true);
        RangeWeapon.SetActive(false);
        Stick.SetActive(false);
        FinishLevelUp();
    }

    public void ChooseRange()
    {
        PEW.CurrentEquippedWeapon = RangeWeapon;
        RangeWeapon.SetActive(true);
        MeleeWeapon.SetActive(false);
        Stick.SetActive(false);
        FinishLevelUp();
    }

    public void IncreaseHealth()
    {
        MaxHealth += IncreaseBonus * 2f;
        FinishLevelUp();
    }

    public void IncreaseDamage()
    {
        CurrentDamageBonus += IncreaseBonus;
        FinishLevelUp();
    }

    public void PlayerTakeDamage(int DamageTaken)
    {
        CurrentHealth -= DamageTaken;
    }

    public void LevelUp()
    {
        Time.timeScale = 0f;
        GetComponentInChildren<Player_Movement_Script>().isPaused = true;

        if (LV == 1)
        {
            LevelUI.gameObject.SetActive(true);
            MeleeButton.SetActive(true);
            RangeButton.SetActive(true);
        }
        else
        {
            LevelUI.gameObject.SetActive(true);
            DamageButton.SetActive(true);
            HealthButton.SetActive(true);
        }
    }

    public void FinishLevelUp()
    {
        LV++;

        XP_Acquired -= XP_Needed;
        XP_Needed = (float)(LV * 100 * XP_Scale);

        CurrentHealth = MaxHealth;

        LevelUI.gameObject.SetActive(false);
        MeleeButton.SetActive(false);
        RangeButton.SetActive(false);
        DamageButton.SetActive(false);
        HealthButton.SetActive(false);

        Time.timeScale = 1f;
        GetComponentInChildren<Player_Movement_Script>().isPaused = false;
        CurrentDamage = CurrentDamageBonus + FindObjectOfType<Player_Equipped_Weapon>().CurrentEquippedWeapon.GetComponent<Weapon_Script>().WeaponData.Damage;
    }
}
