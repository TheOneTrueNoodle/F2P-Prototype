using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Equipped_Weapon : MonoBehaviour
{
    public GameObject CurrentEquippedWeapon;
    private Player_Controls pInput;

    public Transform body;
    public float armLength = 0.5f;

    private void Awake()
    {
        pInput = new Player_Controls();
    }

    void Start()
    {
        body = transform.parent.transform;
        CurrentEquippedWeapon.transform.parent = gameObject.transform;
        CurrentEquippedWeapon.transform.position = gameObject.transform.position;
    }
    void Update()
    {
        Vector3 bodyToMouseDir = Camera.main.ScreenToWorldPoint(pInput.Gameplay.AimDir.ReadValue<Vector2>()) - body.position;
        bodyToMouseDir.z = 0;
        if (CurrentEquippedWeapon.CompareTag("Melee Weapon")) 
        {
            transform.position = body.position - (armLength * bodyToMouseDir.normalized);
            CurrentEquippedWeapon.transform.position = gameObject.transform.position - (CurrentEquippedWeapon.GetComponent<Weapon_Script>().WeaponData.WeaponOffset * bodyToMouseDir.normalized);
            CurrentEquippedWeapon.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else 
        { 
            transform.position = body.position + (armLength * bodyToMouseDir.normalized);
            CurrentEquippedWeapon.transform.position = gameObject.transform.position + (CurrentEquippedWeapon.GetComponent<Weapon_Script>().WeaponData.WeaponOffset * bodyToMouseDir.normalized);
            CurrentEquippedWeapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void OnEnable()
    {
        pInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        pInput.Gameplay.Disable();
    }
}
