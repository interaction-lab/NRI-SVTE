using Microsoft.MixedReality.Toolkit.Experimental.ColorPicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorVisualizer : MonoBehaviour
{
    MeshRenderer rend;
    public ColorPicker colorPicker;
    
    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        enabled = false;
    }

    private void Update()
    {
        //rend.material.color = colorPicker.CustomColor;
    }
}
