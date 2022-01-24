using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pyramid : MonoBehaviour {
    public float height = 1;
    public float width = 1;
    public float length = 1;
    public RawImage camViewerFrame;
    public Camera cam;


    void Start() {
        var meshFilter = GetComponent<MeshFilter>();
        var mesh = new Mesh();

        Vector3 camPosition = cam.GetComponent<Transform>().position;
        RectTransform camViewerFrameRT = camViewerFrame.GetComponent<RectTransform>();
        Vector3[] camViewerFrameCorners = new Vector3[4];
        camViewerFrameRT.GetWorldCorners(camViewerFrameCorners);

        var points = new Vector3[] {
            camViewerFrameCorners[0],
            camViewerFrameCorners[1],
            camViewerFrameCorners[2],
            camViewerFrameCorners[3],
            camPosition
        };

        mesh.vertices = new Vector3[] {
            points[0], points[1], points[2],
            points[0], points[2], points[3],
            points[0], points[1], points[4],
            points[1], points[2], points[4],
            points[2], points[3], points[4],
            points[3], points[0], points[4]
        };

        mesh.triangles = new int[] {
            0, 1, 2,
            3, 4, 5,
            8, 7, 6,
            11, 10, 9,
            14, 13, 12,
            17, 16, 15
        };


        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();

        meshFilter.mesh = mesh;
    }

    void Update() {

      var meshFilter = GetComponent<MeshFilter>();
      var mesh = new Mesh();

      Vector3 camPosition = cam.GetComponent<Transform>().position;
      RectTransform camViewerFrameRT = camViewerFrame.GetComponent<RectTransform>();
      Vector3[] camViewerFrameCorners = new Vector3[4];
      camViewerFrameRT.GetWorldCorners(camViewerFrameCorners);

      var points = new Vector3[] {
          camViewerFrameCorners[0],
          camViewerFrameCorners[1],
          camViewerFrameCorners[2],
          camViewerFrameCorners[3],
          camPosition
      };

      mesh.vertices = new Vector3[] {
          points[0], points[1], points[2],
          points[0], points[2], points[3],
          points[0], points[1], points[4],
          points[1], points[2], points[4],
          points[2], points[3], points[4],
          points[3], points[0], points[4]
      };

      mesh.triangles = new int[] {
          0, 1, 2,
          3, 4, 5,
          8, 7, 6,
          11, 10, 9,
          14, 13, 12,
          17, 16, 15
      };


      mesh.RecalculateNormals();
      mesh.RecalculateBounds();
      mesh.Optimize();

      meshFilter.mesh = mesh;



    }
}
