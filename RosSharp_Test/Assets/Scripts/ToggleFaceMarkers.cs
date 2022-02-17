using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFaceMarkers : MonoBehaviour
{

  public bool EnableFaceSquares = false;
  public bool EnableFaceMeshes = false;
  public GameObject[] FaceSquares;
  public GameObject[] FaceMeshes;

  // Start is called before the first frame update
  void Start()
  {

    FaceSquares = GameObject.FindGameObjectsWithTag("SquareFaceMarker");
    FaceMeshes = GameObject.FindGameObjectsWithTag("MeshFaceMarker");


  }

  // Update is called once per frame
  void Update()
  {
    foreach (GameObject square in FaceSquares)
    {
      square.SetActive(EnableFaceSquares);
    }

    foreach (GameObject mesh in FaceMeshes)
    {
      mesh.SetActive(EnableFaceMeshes);
    }


  }

}
