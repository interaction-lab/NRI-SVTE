using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDisplay : MonoBehaviour
{
    Camera kuri_cam;
    RenderTexture render_tex;
    // Start is called before the first frame update
    void Start()
    {
      kuri_cam.targetTexture = render_tex;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
