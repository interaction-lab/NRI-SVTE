using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryVirtualRays : MonoBehaviour
{
  public float range = 50f;
  //public TextMeshProUGUI distanceText;

  //private LineRenderer laserLine;
  private bool showLasers = false;

  private LineRenderer lr;

  void Start ()
  {
    //laserLine = GetComponent<LineRenderer>();

    lr = GetComponent<LineRenderer>();


       // Set some positions
       Vector3[] positions = new Vector3[3];
       positions[0] = new Vector3(-2.0f, -1.0f, 0.0f);
       positions[1] = new Vector3(0.0f, 2.0f, 0.0f);
       positions[2] = new Vector3(2.0f, -2.0f, 0.0f);
       lr.positionCount = positions.Length;
       lr.SetPositions(positions);

       // Connect the start and end positions of the line together to form a continuous loop.
       lr.loop = true;
  }


  void Update ()
  {
    //
    //
    // if (Input.GetKeyDown("l"))
    // {
    //   showLasers = !showLasers;
    // }
    //
    // if (showLasers)
    // {
    //
    //   laserLine.enabled = true;
    //
    //   Vector3 rayOrigin = transform.position;
    //
    //   RaycastHit hit;
    //
    //   laserLine.SetPosition (0, transform.position);
    //
    //   Vector3 newDir = Quaternion.Euler(0, 90.0f, 0) * transform.forward;
    //
    //   if (Physics.Raycast (rayOrigin, newDir, out hit, range))
    //   {
    //     laserLine.SetPosition (1, hit.point);
    //   }
    //   else
    //   {
    //     laserLine.SetPosition (1, rayOrigin + (newDir * range));
    //   }
    // }
    // else
    // {
    //   laserLine.enabled = false;
    // }
  }
}
