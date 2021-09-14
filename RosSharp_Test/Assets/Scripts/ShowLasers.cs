using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLasers : MonoBehaviour
{
  public float weaponRange = 50f;
  //public TextMeshProUGUI distanceText;

  private LineRenderer laserLine;



  void Start ()
  {
    laserLine = GetComponent<LineRenderer>();
  }


  void Update ()
  {

    if (Input.GetKey("space"))
    {

      laserLine.enabled = true;

      Vector3 rayOrigin = transform.position;

      RaycastHit hit;

      laserLine.SetPosition (0, transform.position);

      int layermask = 1<<6;
      layermask = ~layermask;

      if (Physics.Raycast (rayOrigin, transform.forward, out hit, weaponRange, layermask))

      {
        laserLine.SetPosition (1, hit.point);
      }
      else
      {
        laserLine.SetPosition (1, rayOrigin + (transform.forward * weaponRange));
      }
    }
    else
    {
      laserLine.enabled = false;
    }
  }
}
