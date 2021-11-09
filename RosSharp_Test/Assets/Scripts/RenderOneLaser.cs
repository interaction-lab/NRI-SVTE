using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOneLaser : MonoBehaviour
{
    public LineRenderer laserLine;
    // Start is called before the first frame update
    void Start()
    {
      GetComponent<LineRenderer>();
      laserLine.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
