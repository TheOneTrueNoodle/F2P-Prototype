using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Projectile_Spawner : MonoBehaviour
{
    public Player_Controls pInput;

    public Transform firePoint;
    public GameObject BulletPrefab;

    public float bulletForce = 20f;

    private void Awake()
    {
        pInput = new Player_Controls();
    }

    public void FireProjectile()
    {
        GameObject bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
