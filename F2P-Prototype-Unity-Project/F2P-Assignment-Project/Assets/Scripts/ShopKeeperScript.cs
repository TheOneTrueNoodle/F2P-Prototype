using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeperScript : MonoBehaviour
{
    public Player_Controls pInput;
    public Canvas ShopUI;

    public GameObject PlayerUI;

    [HideInInspector] public bool ShopOpen;
    [HideInInspector] public bool InRange;

    private void Awake()
    {
        pInput = new Player_Controls();
        pInput.Gameplay.Interact.performed += _ => OpenShop();
    }

    public void Start()
    {
    }

    public void OpenShop()
    {
        if (InRange == true)
        {
            if (ShopOpen != true)
            {
                ShopOpen = true;
                Time.timeScale = 0f;
                FindObjectOfType<Player_Movement_Script>().isPaused = true;

                ShopUI.gameObject.SetActive(true);
                PlayerUI.SetActive(false);
            }
            else
            {
                ShopOpen = false;
                Time.timeScale = 1f;
                FindObjectOfType<Player_Movement_Script>().isPaused = false;

                ShopUI.gameObject.SetActive(false);
                PlayerUI.SetActive(true);
            }
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            InRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            InRange = false;
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
