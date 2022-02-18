using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {
    public Transform mainCamera;

    // Start is called before the first frame update
    void Start() {

        if (!mainCamera) {
            mainCamera = Camera.main.transform;
        }

    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.down,
                          mainCamera.transform.rotation * Vector3.back);

    }
}
