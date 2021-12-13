using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOneLaser : MonoBehaviour
{
    public LineRenderer laserLine;

    void Start()
    {
      GetComponent<LineRenderer>();
      laserLine.enabled = true;
    }

    void Update()
    {
    }
}
