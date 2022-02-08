using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRadioLocations : MonoBehaviour 
{
    public GameObject locationPrefab;
    public GameObject radio;
    public bool AutomaticMovement = true;
    //Location Generation
    private readonly int locationNumber = 16;
    private GameObject[] radioLocations;
    private readonly float circleRadiusMax = 2;
    private readonly float circleRadiusMin= 1;
    private bool IsCreated = false;
    private bool LocationsActive = false;
    //Path Generation
    private Vector3[] pathNodes;
    private readonly int nodeNumber = 64;
    private readonly float startingRadius = 1;
    private float endingRadius = 3;
    private float timer = 0;
    public float moveSpeed = 10;
    private Vector3 currentPosition;
    private int currentNode;
    private bool direction = true;
    private int trailRendererTime = 18;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsCreated)
        {
            radio = GameObject.FindGameObjectWithTag(ResourcePathManager.radioTag);
            Create();
        }
    }

    void Create()
    {
        GenerateLocations();
        GeneratePath();
        if (AutomaticMovement == true)
        {
            SetStartingRadioPosition();
            SetRadioLocationsActive(false);
        }
        IsCreated = true;
    }

    private void SetRadioLocationsActive(bool IsActive)
    {
        for(int i = 0; i < radioLocations.Length; i++)
        {
            radioLocations[i].SetActive(IsActive);
            radioLocations[i].GetComponent<Renderer>().enabled = IsActive;
        }
        LocationsActive = IsActive;
    }
    
    private void SetRadioTrailRenderer(bool IsActive)
    {
        TrailRenderer tr = radio.GetComponent<TrailRenderer>();
        if (!IsActive)
        {
            tr.Clear();
            tr.time = -1;
        }
        else
        {
            tr.time = trailRendererTime;
        }
    }

    private void SetStartingRadioPosition()
    {
        currentNode = 0;
        UpdateCurrentPosition();
        radio.transform.position = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z);

    }

    private void UpdateCurrentPosition()
    {
        timer = 0;
        currentPosition = pathNodes[currentNode];
    }
    private void GenerateLocations()
    {
        locationPrefab = Resources.Load<GameObject>(ResourcePathManager.radioLocationPath);
        radioLocations = new GameObject[locationNumber];
        for (int i = 0; i < locationNumber; i++)
        {
            float circleRadius = Random.Range(circleRadiusMin, circleRadiusMax);
            //Calculating location position on the circle around Kuri and its rotation on the y axis
            float locationPosition = (float)i / (float)locationNumber;
            float x = Mathf.Sin(locationPosition * Mathf.PI * 2.0f + Mathf.PI / 4) * circleRadius;
            float z = Mathf.Cos(locationPosition * Mathf.PI * 2.0f + Mathf.PI / 4) * circleRadius;
            radioLocations[i] = Instantiate(locationPrefab, new Vector3(x, -0.34f, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            radioLocations[i].transform.parent = GameObject.Find("RadioLocations").transform;
            radioLocations[i].GetComponent<ChangeRadioLocation>().SetRadio(radio);
        }
        LocationsActive = true;
    }

    private void GeneratePath()
    {
        float radius = startingRadius;
        float radiusIncrement = (endingRadius - startingRadius) / nodeNumber;
        Vector3[] path = new Vector3[nodeNumber];
        for (int i = 0; i < nodeNumber; i++)
        {
            float nodePosition = (float)i / (float)nodeNumber;
            float x = Mathf.Sin(nodePosition * Mathf.PI * 2.0f + Mathf.PI / 4) * radius;
            float z = Mathf.Cos(nodePosition * Mathf.PI * 2.0f + Mathf.PI / 4) * radius;
            radius += radiusIncrement;
            path[i] = new Vector3(x, radio.transform.position.y, z);
        }
        pathNodes = path;
    }

    private void UpdateAutomaticPosition()
    {
        timer += Time.deltaTime * moveSpeed;
        if (radio.transform.position != currentPosition)
        {
            Vector3 newPosition = Vector3.Lerp(radio.transform.position, currentPosition, timer);
            radio.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            radio.transform.LookAt(Vector3.zero);
        }
        else
        {
            if (direction)
            {
                if (currentNode < pathNodes.Length - 1)
                {
                    currentNode += 1;
                    UpdateCurrentPosition();
                }
                else
                    direction = false;
            }
            else
            {
                if (currentNode > 0)
                {
                    currentNode -= 1;
                    UpdateCurrentPosition();
                }
                else
                    direction = true;
            }
        }
    }

    private void Update()
    {
        if (AutomaticMovement)
        {
            if (LocationsActive)
            {
                SetStartingRadioPosition();
                SetRadioLocationsActive(false);
                SetRadioTrailRenderer(true);
            }
            UpdateAutomaticPosition();
        }
        else
        {
            if (!LocationsActive)
            {
                SetRadioTrailRenderer(false);
                SetRadioLocationsActive(true);
            }
        }
    }


}
