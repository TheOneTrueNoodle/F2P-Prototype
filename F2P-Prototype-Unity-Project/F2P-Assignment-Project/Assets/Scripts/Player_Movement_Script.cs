using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_Script : MonoBehaviour
{
    //Variables for Controls
    public Player_Controls pInput;
    private Vector2 MoveInput;
    private Vector2 moveDirection;

    //Speed Variables
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    //Dodge Variables
    public float DodgeSpeed = 7f;
    public float DodgeTime;
    public float DodgeCooldown;
    private float DodgeCooldownCount;
    public bool isDodging;

    private void Awake()
    {
        pInput = new Player_Controls();
        pInput.Gameplay.Dodge.performed += _ => StartCoroutine(Dodge());
    }

    void FixedUpdate()
    {
        if (isDodging != true) { movement(); }
        DodgeCooldownCount--;
    }

    private void movement()
    {
        MoveInput = pInput.Gameplay.Movement.ReadValue<Vector2>();
        moveDirection = MoveInput.normalized;

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    IEnumerator Dodge()
    {
        Vector2 DashDir = pInput.Gameplay.Movement.ReadValue<Vector2>();

        if (DodgeCooldownCount <= 0 && (DashDir.x != 0 || DashDir.y != 0))
        {
            isDodging = true;
            float starttime = Time.time;

            while (Time.time < starttime + DodgeTime)
            {
                rb.velocity = new Vector2(DashDir.x * DodgeSpeed, DashDir.y * DodgeSpeed);

                yield return null;
            }
            DodgeCooldownCount = DodgeCooldown;
            isDodging = false;
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
