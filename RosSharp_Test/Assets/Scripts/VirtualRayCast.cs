using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VirtualRayCast : MonoBehaviour
{
  public float range = 50f;
  public TextMeshProUGUI distanceText;

  private LineRenderer laserLine;
  private bool showLasers = false;

  private float interval = 0.1f;
  private float nextTime = 0;

  void Start ()
  {
    laserLine = GetComponent<LineRenderer>();
  }


  void Update ()
  {

    // if ((Time.time >= nextTime) && Input.GetKey("l")) {
    //
    //   // //do something here every interval seconds
    //   // if (Input.GetKey("l")){
    //   //   showLasers = !showLasers;
    //   // }
    //   showLasers = !showLasers;
    //   nextTime += interval;
    //
    // }

    //showLasers = Input.GetKeyDown("l");

    if (Input.GetKeyDown("l"))
    //if (showLasers)
    {

      //laserLine.enabled = true;
      showLasers = !showLasers;
    }



    //if (Input.GetKey("l"))
    //if (Input.GetKeyDown("l"))
    if (showLasers)
    {

      laserLine.enabled = true;
      //laserLine.enabled = !laserLine.enabled;

      Vector3 rayOrigin = transform.position;

      RaycastHit hit;

      laserLine.SetPosition (0, transform.position);

      if (Physics.Raycast (rayOrigin, transform.forward, out hit, range))
      {
        laserLine.SetPosition (1, hit.point);
        distanceText.text = "Distance: " + hit.distance.ToString();
      }
      else
      {
        laserLine.SetPosition (1, rayOrigin + (transform.forward * range));
      }
    }
    else
    {
      laserLine.enabled = false;
    }
  }
}
