using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLasers : MonoBehaviour
{
  public GameObject prefab;
  public GameObject[] prefabs;
  public Color lidarColor = Color.white;
  public float lidarWidth = .00f;
  public Slider widthSlider;
  public Slider colorSlider;
  public FlexibleColorPicker cp;
  private AudioSource source;

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
        LineRenderer laserLine = pf.GetComponent<LineRenderer>();
        TrailRenderer lidarTrail = pf.GetComponent<TrailRenderer>();
        lidarWidth = widthSlider.value;


        lidarTrail.startWidth = lidarWidth;
    }
  }

  public class Message {
    public float[] ranges;
    public Vector3[] hit_points;
  }

  public void UpdateRanges(Message message)
  {
    float[] ranges = message.ranges;
    for (int i = 0; i < 180; i++)
    {
        GameObject pf = prefabs[i];
        LineRenderer laserLine = pf.GetComponent<LineRenderer>();
        laserLine.enabled = false;
        var sphere = pf.transform.Find("Sphere");
        TrailRenderer lidarTrail = sphere.GetComponent<TrailRenderer>();
        var PM = sphere.GetComponent<ProjectileMotion>();
        PM.startPose = pf.transform.position;
        PM.endPose = pf.transform.position + (pf.transform.forward * ranges[i]);
        // laserLine.SetPosition (0, transform.position);
        // laserLine.SetPosition(1,transform.position + (pf.transform.forward * ranges[i]));
        // laserLine.material.color = lidarColor;
        lidarWidth = widthSlider.value;
        // lidarColor = Color.HSVToRGB(colorSlider.value, 1, 1);
        lidarColor = cp.color;

        lidarTrail.startWidth = lidarWidth;
        lidarTrail.material.color = lidarColor;
    }
  }

  void Update ()
  {

  }
}
