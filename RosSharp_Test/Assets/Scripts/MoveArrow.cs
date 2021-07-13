using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrow : MonoBehaviour
{
    private Rigidbody arrowRb;
    private Rigidbody obstacleRb;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // get the arrow's rigidbody
        arrowRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // get input from up and down arrow keys
        float forwardInput = Input.GetAxis("Vertical");

        // get input from left and right arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");


        // move arrow left or right
        transform.Translate(Vector3.left * Time.deltaTime * speed * horizontalInput);

        // move arrow forward or backward
        transform.Translate(Vector3.up * Time.deltaTime * speed * forwardInput);

        //transform.Rotate(0, 0, Time.deltaTime * speed * horizontalInput);


    }

    private void OnCollisionEnter(Collision collision)
    {
        obstacleRb = collision.gameObject.GetComponent<Rigidbody>();

        Debug.Log("Collided with " + collision.gameObject.name);
    }
}
