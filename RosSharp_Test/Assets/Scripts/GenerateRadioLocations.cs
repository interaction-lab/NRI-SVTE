using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRadioLocations : MonoBehaviour 
{
    public GameObject locationPrefab;
    public GameObject radio;
    private readonly int locationNumber = 4;
    private GameObject[] radioLocations;
    private float circleRadiusMax = 2;
    private float circleRadiusMin= 1;
    private bool IsCreated = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!IsCreated)
            Create();
    }

    void Create()
    {
        radioLocations = new GameObject[locationNumber];
        for (int i = 0; i < locationNumber; i++)
        {
            float circleRadius = Random.Range(circleRadiusMin, circleRadiusMax);
            //Calculating location position on the circle around Kuri and its rotation on the y axis
            float locationPosition = (float)i / (float)locationNumber;
            float x = Mathf.Sin(locationPosition * Mathf.PI * 2.0f + Mathf.PI / 4) * circleRadius;
            float z = Mathf.Cos(locationPosition * Mathf.PI * 2.0f + Mathf.PI / 4) * circleRadius;
            radioLocations[i] = Instantiate(locationPrefab, new Vector3(x, -0.34f, z), Quaternion.Euler(0,0,0)) as GameObject;
            radioLocations[i].transform.parent = GameObject.Find("RadioLocations").transform;
            radioLocations[i].GetComponent<ChangeRadioLocation>().radio = radio;
        }
        IsCreated = true;
    }

   
}
