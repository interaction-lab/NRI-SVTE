using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:
// Make ShowLasers into a thing that instantiates prefabs
// Make RayCaster form a message that it sends to ShowLasers
// Message needs to contain a list of distances
// it'll be great



public class RayCaster : MonoBehaviour
{
  // Instantiates prefabs in a circle formation

  public GameObject prefab;
  public float radius = 5f;
  public float MaxRotation = Mathf.PI/2;
  public float weaponRange = 2.5f;
  public GameObject[] prefabs;


    void Start()
  {
      RaycastHit hit;

      // instantiate prefabs
      for (int i = 0; i < 180; i++)
      {
          float angle = i * Mathf.PI / 180;
          //float x = Mathf.Cos(angle) * radius;
          //float z = Mathf.Sin(angle) * radius;
          float angleDegrees = 180 + angle*Mathf.Rad2Deg;
          Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
          GameObject pf = Instantiate(prefab, transform.position, rot, transform) as GameObject;
          prefabs[i]=pf;

          int layermask = 1 << 6;
          layermask = ~layermask;
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
              Debug.DrawRay(transform.position, rot * transform.forward, Color.green);
            }

            // print(hit.distance);
            // Instantiate(prefab, transform, rot);
        }


    }
}
