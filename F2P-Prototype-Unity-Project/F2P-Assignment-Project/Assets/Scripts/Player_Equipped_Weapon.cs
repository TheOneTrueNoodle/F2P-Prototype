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
    void Update()
    {
       /* Vector3 bodyToMouseDir = Camera.main.ScreenToWorldPoint(pInput.Gameplay.AimDir.ReadValue<Vector2>()) - body.position;
        bodyToMouseDir.z = 0;
        if (CurrentEquippedWeapon.CompareTag("Melee Weapon") && Attacking != true) 
        {
            gameObject.transform.position = body.position - (armLength * bodyToMouseDir.normalized);
            CurrentEquippedWeapon.transform.position = gameObject.transform.position - (CurrentEquippedWeapon.GetComponent<Weapon_Script>().WeaponData.WeaponOffset * bodyToMouseDir.normalized);
            CurrentEquippedWeapon.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if(Attacking != true)
        { 
            gameObject.transform.position = body.position + (armLength * bodyToMouseDir.normalized);
            CurrentEquippedWeapon.transform.position = gameObject.transform.position + (CurrentEquippedWeapon.GetComponent<Weapon_Script>().WeaponData.WeaponOffset * bodyToMouseDir.normalized);
            CurrentEquippedWeapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }*/
    }

    private void TriggerAttack()
    {
        if(CurrentEquippedWeapon.CompareTag("Melee Weapon"))
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
