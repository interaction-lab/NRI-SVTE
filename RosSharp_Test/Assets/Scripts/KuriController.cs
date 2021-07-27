using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuriController : MonoBehaviour
{
    private Rigidbody arrowRb;
    private Rigidbody obstacleRb;
    public float speed = 1.0f;
    public float turnSpeed = 15.0f;

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

        // turn arrow left or right
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

        // move arrow forward or backward
        transform.Translate(Vector3.left * Time.deltaTime * speed * forwardInput);

    }

    private void OnCollisionEnter(Collision collision)
    {
        obstacleRb = collision.gameObject.GetComponent<Rigidbody>();

        Debug.Log("Collided with " + collision.gameObject.name);
    }
}
