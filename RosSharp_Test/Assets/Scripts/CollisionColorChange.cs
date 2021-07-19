using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionColorChange : MonoBehaviour
{
    public Color sensorOn;
    public Color sensorOff;
    public int bumperPosition;  // right = 0, middle = 1, left = 2
    public int[] bumperSensors = new int[] { 0, 0, 0 };  // 0 = sensor not bumped ; 1 = sensor bumped


    // Start is called before the first frame update
    void Start()
    {
        // default is sensor off
        transform.GetComponent<Renderer>().material.color = sensorOff;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Bumper sensor status: " + bumperSensors[0] + " " + bumperSensors[1] + " " + bumperSensors[2]);
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if sensor collides with an obstacle
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // print out a message indicating which sensor has been bumped
            Debug.Log(gameObject.name + " position " + bumperPosition + " has been bumped");

            // change the color of the sensor to green 
            transform.GetComponent<Renderer>().material.color = sensorOn;

            // change sensor status to bumped
            bumperSensors[bumperPosition] = 1;

            // print sensor status after being bumped
            Debug.Log("Bumper sensor status: " + bumperSensors[0] + " " + bumperSensors[1] + " " + bumperSensors[2]);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        // when sensor is no longer touching the obstacle
        transform.GetComponent<Renderer>().material.color = sensorOff;

        // change sensor status to not bumped
        bumperSensors[bumperPosition] = 0;

        // print sensor status after no longer touching obstacle
        Debug.Log("Bumper sensor status: " + bumperSensors[0] + " " + bumperSensors[1] + " " + bumperSensors[2]);
    }

}
