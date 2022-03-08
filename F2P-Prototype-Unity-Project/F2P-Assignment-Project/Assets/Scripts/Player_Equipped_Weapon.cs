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

    private Animator Anim;
    public bool Attacking;

    private Queue<IEnumerator> AttackQueue = new Queue<IEnumerator>();


    private void Awake()
    {
        Anim = this.GetComponent<Animator>();
        pInput = new Player_Controls();
        pInput.Gameplay.BasicAttack.performed += _ => TriggerAttack();
    }

    void Start()
    {
        StartCoroutine(CoroutineCoordinator());
        body = transform.parent.transform;
        CurrentEquippedWeapon.transform.parent = gameObject.transform;
        CurrentEquippedWeapon.transform.position = gameObject.transform.position;
    }

    private void TriggerAttack()
    {
        if (CurrentEquippedWeapon.CompareTag("Melee Weapon"))
        {
            AttackQueue.Enqueue(MeleeAttack());
        }
    }

    IEnumerator MeleeAttack()
    {
        Attacking = true;
        Anim.Play("Melee_Attack_Animation");

        while(Anim.GetCurrentAnimatorStateInfo(0).IsName("Melee_Attack_Animation"))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        Attacking = false;
    }

    IEnumerator RangeAttack()
    {
        Anim.Play("Melee_Attack_Animation");
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
