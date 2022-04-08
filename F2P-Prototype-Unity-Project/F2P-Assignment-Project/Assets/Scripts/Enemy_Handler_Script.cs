using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Handler_Script : MonoBehaviour
{
    public float Damage = 1f;
    public float MaxHealth = 3f;
    public float CurrentHealth = 1f;

    public Sprite[] SpriteOptions;

    public float XPMaxRange = 30f;
    public float XPMinRange = 15f;

    public float GoldMaxRange = 5f;
    public float GoldMinRange = 1f;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        int Skin = Random.Range(0, SpriteOptions.Length);
        GetComponent<SpriteRenderer>().sprite = SpriteOptions[Skin];
    }

    private void Update()
    {
        if (Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position).x < Camera.main.WorldToScreenPoint(gameObject.transform.position).x && FindObjectOfType<Player_Movement_Script>().isPaused != true)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (FindObjectOfType<Player_Movement_Script>().isPaused != true)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if (CurrentHealth <= 0)
        {
            int XPDrop = Random.Range((int)XPMaxRange, (int)XPMinRange);
            int GoldDrop = Random.Range((int)GoldMaxRange, (int)GoldMinRange);
            FindObjectOfType<Player_Handler_Script>().XP_Acquired += XPDrop;
            FindObjectOfType<GoldManager>().GoldCount += GoldDrop;
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
