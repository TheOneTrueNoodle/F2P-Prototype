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
    private float currentSpeed;
    public Rigidbody2D rb;

    //Dodge Variables
    public float DodgeSpeed = 10f;
    public float DodgeTime = 0.3f;
    public float DodgeCooldown = 10f;
    private float DodgeCooldownCount;
    public bool isDodging;

    //Aim Variables
    public Camera cam;
    Vector2 mousePos;

    private void Awake()
    {
        pInput = new Player_Controls();
        pInput.Gameplay.Dodge.performed += _ => StartCoroutine(Dodge());
    }

    private void Update()
    {
        if (DodgeCooldownCount < 0) { DodgeCooldownCount = 0; }
        else if (DodgeCooldownCount > 0) { currentSpeed = moveSpeed / DodgeCooldownCount; }
        else { currentSpeed = moveSpeed; }
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

        rb.velocity = new Vector2(moveDirection.x * currentSpeed, moveDirection.y * currentSpeed);
        float xScale = 1f;
        if(MoveInput.x == 0.5f) { xScale = 1f; } else if(MoveInput.x == -0.5) { xScale = -1f; }
        if (MoveInput.x != 0) { transform.localScale = new Vector3(xScale, 1f, 1f); }
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
