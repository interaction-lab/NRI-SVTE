using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
  // Instantiates prefabs in a circle formation

  public GameObject prefab;
  public int numberOfObjects = 181;
  public float radius = 5f;
  public float MaxRotation = Mathf.PI/2;


  void Start()
  {
      for (int i = -90; i < 90; i++)
      {
          float angle = i * Mathf.PI / 180;
          //float x = Mathf.Cos(angle) * radius;
          //float z = Mathf.Sin(angle) * radius;
          //Vector3 pos = transform.position + new Vector3(x, 0, z);
          float angleDegrees = 180 - angle*Mathf.Rad2Deg;
          Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
          Instantiate(prefab, transform.position, rot, transform);
          // Instantiate(prefab, transform, rot);
      }
  }
}
