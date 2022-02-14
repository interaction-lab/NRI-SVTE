using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
  public float movementDelta = .00005f;
  private float pos = 0f;
  public AnimationCurve animCurve;
  public Vector3 startPose;
  public Vector3 endPose;
  public float maxRange;
  public float range;
  // public float alive;

  void Start()
  {

  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Alpha0))
    {
      GetComponent<TrailRenderer>().emitting = true;
      StartCoroutine(MoveBall());
    }
  }

  IEnumerator MoveBall() {
    // GetComponent<TrailRenderer>().emitting = true;
    float totalTime = 1;
    float liveTime = (range/maxRange)*totalTime;
    // float totalTime = (maxRange/movementDelta)*Time.deltaTime;
    // float liveTime = (range/movementDelta)*Time.deltaTime;
    // float returnTime = (maxRange/movementDelta)*Time.deltaTime;
    float returnTime = .5f;
    float curtime = 0;
    while(curtime < liveTime)
    {
      transform.position = Vector3.Lerp(startPose, endPose, animCurve.Evaluate(curtime/totalTime));
      curtime += Time.deltaTime;
      yield return new WaitForSeconds(Time.deltaTime);
    }
    GetComponent<TrailRenderer>().emitting = false;
    while(curtime < totalTime - .005)
    {
      transform.position = Vector3.Lerp(startPose, endPose, animCurve.Evaluate(curtime/totalTime));
      curtime += Time.deltaTime;
      yield return new WaitForSeconds(Time.deltaTime);
    }
    while(curtime < totalTime)
    {
      transform.position = Vector3.Lerp(startPose, endPose, animCurve.Evaluate(curtime/totalTime));
      curtime += Time.deltaTime;
      yield return new WaitForSeconds(Time.deltaTime);
    }
    curtime = 0;
    while(curtime < returnTime)
    {
      transform.position = Vector3.Lerp(endPose, startPose, animCurve.Evaluate(curtime/returnTime));
      curtime += Time.deltaTime;
      yield return new WaitForSeconds(Time.deltaTime);
    }
    transform.position = startPose;
    // yield return new WaitForSeconds(1f);
    GetComponent<TrailRenderer>().emitting = true;

    StartCoroutine(MoveBall());

  }

}
