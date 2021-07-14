using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionColorChange : MonoBehaviour
{
    public Color sensorOn;
    public Color sensorOff;

    // Start is called before the first frame update
    void Start()
    {
        // default is sensor off
        transform.GetComponent<Renderer>().material.color = sensorOff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if sensor collides with an obstacle
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // print out a message indicating which sensor has been bumped
            Debug.Log(gameObject.name + " has been bumped");

            // change the color of the sensor to green 
            transform.GetComponent<Renderer>().material.color = sensorOn;
        }
    }
}
