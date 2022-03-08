using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Melee_Hitbox_Script : MonoBehaviour
{
    public Player_Handler_Script PHS;
    public Player_Equipped_Weapon PEW;
    private int CurrentDMG;

    private void Update()
    {
        CurrentDMG = (int)PHS.CurrentDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && PEW.Attacking == true)
        {
            collision.GetComponent<Enemy_Handler_Script>().TakeDamage(CurrentDMG);
        }
    }
}
