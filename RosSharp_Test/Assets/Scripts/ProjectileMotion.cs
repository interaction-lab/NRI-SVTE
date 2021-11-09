using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    public float movementDelta = .10f;
    private float pos = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * movementDelta;
        pos += movementDelta;
        if (pos >= 5){
          transform.position -= transform.forward * pos;
          pos = 0f;
        }
    }
}
