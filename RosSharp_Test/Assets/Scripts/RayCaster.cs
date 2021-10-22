using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
  // Instantiates prefabs in a circle formation
  public ShowLasers SL;
  public float[] ranges;
  // public GameObject prefab;
  // public float radius = 5f;
  public float MaxRotation = Mathf.PI/2;
  public float weaponRange = .00025f;

    void Start()
  {
      SL = FindObjectOfType<ShowLasers>();
      ranges = new float[180];
      weaponRange = 2.5f;
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
              ranges[i] = hit.distance;
            }else
            {
              ranges[i] = weaponRange;
            }
        }

        ShowLasers.Message updateMessage = new ShowLasers.Message();
        updateMessage.ranges = ranges;
        SL.UpdateRanges(updateMessage);

    }
}
