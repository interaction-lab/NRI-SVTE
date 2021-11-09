using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLasers : MonoBehaviour
{
  public GameObject prefab;
  public GameObject[] prefabs;
  public Color lidarColor = Color.magenta;
  public float lidarWidth = .05f;

  void Start ()
  {
    prefabs = new GameObject[180];

    for (int i = 0; i < 180; i++)
    {
        float angle = i * Mathf.PI / 180;
        float angleDegrees = 90 + angle*Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
        GameObject pf = Instantiate(prefab, transform.position, rot, transform) as GameObject;
        prefabs[i]=pf;
    }
  }

  public class Message {
    public float[] ranges;
  }

  public void UpdateRanges(Message message)
  {
    float[] ranges = message.ranges;
    for (int i = 0; i < 180; i++)
    {
        GameObject pf = prefabs[i];
        LineRenderer laserLine = pf.GetComponent<LineRenderer>();
        laserLine.SetPosition (0, transform.position);
        laserLine.SetPosition(1,transform.position + (pf.transform.forward * ranges[i]));
        laserLine.material.color = lidarColor;
        laserLine.SetWidth(0,lidarWidth);
    }
  }

  void Update ()
  {
  }
}
