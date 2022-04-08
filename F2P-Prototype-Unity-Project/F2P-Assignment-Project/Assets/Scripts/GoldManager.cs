using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    private static bool Exists;

    public int GoldCount = 0;

    public TMP_Text GoldDisp;

    public GameObject UI;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (!Exists)
        {
            Exists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Update()
    {
        if(GoldDisp == null)
        {
            GoldDisp = FindObjectOfType<Player_Handler_Script>().GoldDisp;
        }
        else
        {
            GoldDisp.text = GoldCount.ToString();
        }
    }
}
