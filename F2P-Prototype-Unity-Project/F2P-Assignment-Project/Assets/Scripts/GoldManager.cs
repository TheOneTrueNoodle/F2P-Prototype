using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public int GoldCount;

    public TMP_Text GoldDisp;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        GoldDisp.text = GoldCount.ToString();
    }
}
