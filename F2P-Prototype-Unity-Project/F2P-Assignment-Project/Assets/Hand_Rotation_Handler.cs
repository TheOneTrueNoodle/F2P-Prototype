using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Rotation_Handler : MonoBehaviour
{
    public Player_Controls pInput;
    public float speed = 5f;

    private void Awake()
    {
        pInput = new Player_Controls();
    }

    private void Update()
    {
        lookDir();
    }

    private void lookDir()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(pInput.Gameplay.AimDir.ReadValue<Vector2>()) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
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
