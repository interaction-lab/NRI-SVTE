using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRadioLocation : MonoBehaviour
{
    public GameObject radio;
    private float yOffset = 0.09f;
    private Vector3 origin = new Vector3(0, 0, 0);
  
    void OnMouseDown()
    {
     
        radio.transform.position= new Vector3(transform.position.x,
            transform.position.y + yOffset, transform.position.z);
        radio.transform.LookAt(origin);

    }

    
   
}
