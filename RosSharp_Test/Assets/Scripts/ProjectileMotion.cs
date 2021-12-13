using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
  public float movementDelta = .10f;
  private float pos = 0f;
  public AnimationCurve animCurve;
  public Vector3 startPose;
  public Vector3 endPose;
  
  void Start()
  {

  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Alpha0))
    {
      StartCoroutine(MoveBall());
    }
  }

  IEnumerator MoveBall() {
    GetComponent<TrailRenderer>().emitting = true;
    float totalTime = 1;
    float returnTime = .25f;
    float curtime = 0;
    while(curtime < totalTime - .005)
    {
      transform.position = Vector3.Lerp(startPose, endPose, animCurve.Evaluate(curtime/totalTime));
      curtime += Time.deltaTime;
      yield return new WaitForSeconds(Time.deltaTime);
    }
    GetComponent<TrailRenderer>().emitting = false;
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
    StartCoroutine(MoveBall());

  }

}
