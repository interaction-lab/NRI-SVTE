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
    public float weaponRange = 2.5f;


    void Start()
  {
      RaycastHit hit;

      for (int i = -90; i < 90; i++)
      {
          float angle = i * Mathf.PI / 180;
          //float x = Mathf.Cos(angle) * radius;
          //float z = Mathf.Sin(angle) * radius;
          float angleDegrees = 90 + angle*Mathf.Rad2Deg;
          Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
          Instantiate(prefab, transform.position, rot, transform);

          int layermask = 1 << 6;
          layermask = ~layermask;

            Physics.Raycast(transform.position, rot * transform.up, out hit, weaponRange, layermask);
            print("hit distance =" + hit.distance);
          // Instantiate(prefab, transform, rot);
      }


    }

    void Update()
    {
        RaycastHit hit;

        for (int i = 0; i < 180; i++)
        {
            float angle = i * Mathf.PI / 180;
            //float x = Mathf.Cos(angle) * radius;
            //float z = Mathf.Sin(angle) * radius;
            float angleDegrees = 180 + angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);

            int layermask = 1 << 6;
            layermask = ~layermask;

            if(Physics.Raycast(transform.position, rot * transform.forward, out hit, weaponRange, layermask))
            {
              Debug.DrawRay(transform.position, rot * transform.forward, Color.green);
              Physics.Raycast(transform.position, rot * transform.forward, out hit, weaponRange, layermask);

              if (hit.distance <= 1.0){
                print("hit distance =" + hit.distance);
              }

              Physics.Raycast(transform.position, rot * transform.forward, out hit, weaponRange, layermask);
              // Debug.DrawRay(transform.position, rot * transform.forward, Color.green);
            }

            // print(hit.distance);
            // Instantiate(prefab, transform, rot);
        }


    }
}
