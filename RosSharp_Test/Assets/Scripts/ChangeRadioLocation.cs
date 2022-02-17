using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRadioLocation : MonoBehaviour
{
    private GameObject radio;
    private float yOffset = 0.09f;
    private Vector3 origin;
   

    void OnMouseDown()
    {
     
        radio.transform.position= new Vector3(transform.position.x,
            transform.position.y + yOffset, transform.position.z);
        radio.transform.LookAt(origin);
        

    }

    public void SetRadio(GameObject audioSource)
    {
        radio = audioSource;
    }

    public void SetOrigin(Vector3 point) {
        
        origin = new Vector3(point.x, point.y, point.z);
    }

   

}
