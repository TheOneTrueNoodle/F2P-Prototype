using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Handler_Script : MonoBehaviour
{
    public float Damage = 1f;
    public float MaxHealth = 3f;
    public float CurrentHealth = 1f;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if(CurrentHealth <= 0)
        {
            FindObjectOfType<Player_Handler_Script>().XP_Acquired += 20f;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Player_Handler_Script>().PlayerTakeDamage((int)Damage);
        }
    }

    public void TakeDamage(int DamageTaken)
    {
        CurrentHealth -= DamageTaken;
    }
}
