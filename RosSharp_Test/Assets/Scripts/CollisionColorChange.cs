using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionColorChange : MonoBehaviour
{
    public Color sensorOn;
    public Color sensorOff;
    public int bumperPosition;  // right = 0, middle = 1, left = 2
    public bool pressedStatus;


    // Start is called before the first frame update
    void Start()
    {
        // default is sensor off
        transform.GetComponent<Renderer>().material.color = sensorOff;

        pressedStatus = false;
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
            pressedStatus = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // default is sensor off
        transform.GetComponent<Renderer>().material.color = sensorOff;

        pressedStatus = false;
    }

}
