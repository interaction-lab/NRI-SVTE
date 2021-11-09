using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
  public ShowLasers SL;
  public float[] ranges;
  public float maxDistance = .00025f;
  public int layermask;


    void Start()
  {
      SL = FindObjectOfType<ShowLasers>();
      ranges = new float[180];
      maxDistance = 2.5f;
      layermask = 1 << 6;
      layermask = ~layermask;
    }

    void Update()
    {
        RaycastHit hit;

        for (int i = 0; i < 180; i++)
        {
            float angle = i * Mathf.PI / 180;
            float angleDegrees = 180 + angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);

            if(Physics.Raycast(transform.position, rot * transform.forward, out hit, maxDistance, layermask))
            {
              ranges[i] = hit.distance;
            }else
            {
              ranges[i] = maxDistance;
            }
        }

        ShowLasers.Message updateMessage = new ShowLasers.Message();
        updateMessage.ranges = ranges;
        SL.UpdateRanges(updateMessage);

    }
}