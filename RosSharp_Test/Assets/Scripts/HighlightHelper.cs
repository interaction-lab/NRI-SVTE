using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightHelper : MonoBehaviour
{
    public MeshRenderer mesh;
    public Material off;
    public Material on;
    private bool isOn;

   void Start()
    {
        isOn = false;
    }



    // Update is called once per frame
    void Update()
    {
        if(isOn)
        {
            mesh.material = on;
            isOn = false;
        }else
        {
            mesh.material = off;
        }
    }

    public void highlight()
    {
        isOn = true;
    }
}
