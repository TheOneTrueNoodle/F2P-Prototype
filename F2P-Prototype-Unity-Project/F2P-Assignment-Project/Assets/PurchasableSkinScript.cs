using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchasableSkinScript : MonoBehaviour
{
    public bool IsBought;

    public int Price;

    public Sprite Sprite;

    private Player_Movement_Script PMS;
    public TMP_Text Text;

    private void Start()
    {
        PMS = FindObjectOfType<Player_Movement_Script>();
    }

    private void Update()
    {
        if(IsBought != true)
        {
            Text.text = "BUY FOR " + Price.ToString();
        }
        else
        {
            Text.text = "EQUIP";
        }
    }

    public void OnInteract()
    {
        if (IsBought != true)
        {
            Buy();
        }
        else
        {
            Equip();
        }
    }

    public void Equip()
    {
        PMS.PlayerSprite.GetComponent<SpriteRenderer>().sprite = Sprite;
    }

    public void Buy()
    {
        if (FindObjectOfType<GoldManager>().GoldCount < Price)
        {

        }
        else if(FindObjectOfType<GoldManager>().GoldCount >= Price)
        {
            FindObjectOfType<GoldManager>().GoldCount -= Price;
            IsBought = true;
        }
    }
}
