using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCam : MonoBehaviour
{
    public Canvas canvas;

    private void Update()
    {
        if(canvas.worldCamera == null)
        {
            canvas.worldCamera = Camera.main;
        }
    }
}
