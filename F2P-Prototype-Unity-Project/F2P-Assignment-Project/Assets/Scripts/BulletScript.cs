using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Player_Handler_Script PHS;
    public Player_Equipped_Weapon PEW;
    private int CurrentDMG;

    public float AliveTime = 2f;

    private void Start()
    {
        PHS = FindObjectOfType<Player_Handler_Script>();
        PEW = FindObjectOfType<Player_Equipped_Weapon>();

        StartCoroutine(StartDie());
    }

    private void Update()
    {
        CurrentDMG = (int)PHS.CurrentDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy_Handler_Script>().TakeDamage(CurrentDMG);
            Destroy(gameObject);
        }
    }

    IEnumerator StartDie()
    {
        float starttime = Time.time;

        while (Time.time < starttime + AliveTime)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
