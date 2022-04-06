using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Equipped_Weapon : MonoBehaviour
{
    public Camera cam;

    public GameObject CurrentEquippedWeapon;
    private Player_Controls pInput;

    public Transform body;
    public float armLength = 0.5f;
    public float AttackCooldown = 0.2f;

    private Animator Anim;
    public bool Attacking;

    private Queue<IEnumerator> AttackQueue = new Queue<IEnumerator>();

    public bool AtkButtonHeld;


    private void Awake()
    {
        Anim = this.GetComponent<Animator>();
        pInput = new Player_Controls();
        pInput.Gameplay.BasicAttack.performed += _ => AtkButtonHeld = true;
        pInput.Gameplay.BasicAttack.canceled += _ => AtkButtonHeld = false;
    }

    void Start()
    {
        StartCoroutine(CoroutineCoordinator());
        body = transform.parent.transform;
        CurrentEquippedWeapon.transform.parent = gameObject.transform;
        CurrentEquippedWeapon.transform.position = gameObject.transform.position;
    }

    private void Update()
    {
        if(AtkButtonHeld == true && Attacking != true)
        {
            TriggerAttack();
        }
        else if(Attacking == true)
        {
            AttackQueue.Clear();
            AttackQueue.Enqueue(EndAttack());
        }
    }

    private void TriggerAttack()
    {
        Attacking = true;
        if (CurrentEquippedWeapon.CompareTag("Melee Weapon"))
        {
            AttackQueue.Enqueue(MeleeAttack());
        }
        else if (CurrentEquippedWeapon.CompareTag("Range Weapon"))
        {
            AttackQueue.Enqueue(RangeAttack());
        }
    }

    IEnumerator MeleeAttack()
    {
        Anim.Play("Melee_Attack_Animation");

        while (Anim.GetCurrentAnimatorStateInfo(0).IsName("Melee_Attack_Animation"))
        {
            yield return null;
        }
        yield return new WaitForSeconds(AttackCooldown);
        if(AtkButtonHeld == true)
        {
            AttackQueue.Enqueue(MeleeAttack());
        }
    }

    IEnumerator RangeAttack()
    {
        Anim.Play("Melee_Attack_Animation");

        while (Anim.GetCurrentAnimatorStateInfo(0).IsName("Melee_Attack_Animation"))
        {
            yield return null;
        }
        CurrentEquippedWeapon.GetComponentInChildren<Ranged_Projectile_Spawner>().FireProjectile();
        yield return new WaitForSeconds(AttackCooldown);
        if (AtkButtonHeld == true)
        {
            AttackQueue.Enqueue(RangeAttack());
        }
    }

    IEnumerator EndAttack()
    {
        Attacking = false;
        yield return null;
    }

    IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (AttackQueue.Count > 0)
                yield return StartCoroutine(AttackQueue.Dequeue());
            yield return null;
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
