using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLasers : MonoBehaviour
{
  public float weaponRange = 50f;
  public GameObject prefab;
  //public TextMeshProUGUI distanceText;

  private LineRenderer laserLine;



  void Start ()
  {
    laserLine = GetComponent<LineRenderer>();

    float angle = i * Mathf.PI / 180;
    float angleDegrees = 180 + angle*Mathf.Rad2Deg;
    Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
    GameObject pf = Instantiate(prefab, transform.position, rot, transform) as GameObject;
    prefabs[i]=pf;
  }


  void Update ()
  {

    if (Input.GetKey("l"))
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
