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
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
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
    // Vector3 startPose = transform.position;
    // Vector3 startPose = transform.parent.position;
    // Vector3 endPose = transform.position + Vector3.up * 3; // 0, 1, 0
    // Vector3 endPose = startPose * 2;
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
    // GetComponent<TrailRenderer>().emitting = false;
    // yield return new WaitForSeconds(Time.deltaTime);
    transform.position = startPose;
    StartCoroutine(MoveBall());

  }

}


// public class LerpSphere : MonoBehaviour
// {
//     // Start is called before the first frame update
//     public AnimationCurve animCurve;
//
//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Alpha0))
//         {
//             StartCoroutine(MoveBall());
//         }
//     }
//
//     IEnumerator MoveBall() {
//         GetComponent<TrailRenderer>().enabled = true;
//         float totalTime = 1;
//         float curtime = 0;
//         Vector3 startPose = transform.position;
//         Vector3 endPose = transform.position + Vector3.up * 3; // 0, 1, 0
//         while(curtime < totalTime)
//         {
//             transform.position = Vector3.Lerp(startPose, endPose, animCurve.Evaluate(curtime/totalTime));
//             curtime += Time.deltaTime;
//             yield return new WaitForSeconds(Time.deltaTime);
//         }
//         GetComponent<TrailRenderer>().enabled = false;
//         transform.position = startPose;
//         StartCoroutine(MoveBall());
//
//     }
// }
