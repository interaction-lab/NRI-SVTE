using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCameraLine : MonoBehaviour
{
    public GameObject anchor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      LineRenderer cameraLine = GetComponent<LineRenderer>();
      cameraLine.startWidth = .01f;
      cameraLine.SetPosition(0, transform.position);
      cameraLine.SetPosition(1, anchor.transform.position);

    }
}
