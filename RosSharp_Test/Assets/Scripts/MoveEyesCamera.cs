using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEyesCamera : MonoBehaviour
{
    public GameObject eyesCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // have eyes camera move as kuri moves and turns
        transform.position = eyesCamera.transform.position;
    }
}
