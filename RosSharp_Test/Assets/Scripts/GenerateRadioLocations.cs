using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRadioLocations : MonoBehaviour 
{
    public GameObject locationPrefab;
    public GameObject radio;
    public bool AutomaticMovement = false;
    private Vector3 kuriPosition;
    //Location Generation
    public int locationNumber = 4;
    private GameObject[] radioLocations;
    private readonly float circleRadiusMax = 2;
    private readonly float circleRadiusMin= 1;
    private bool IsCreated = false;
    private bool LocationsActive = false;
    private float fixedHeight = 0.2f;
    //Path Generation
    private Vector3[] pathNodes;
    private readonly int nodeNumber = 64;
    private readonly float startingRadius = 0.8f;
    private float endingRadius = 2;
    private float timer = 0;
    public float moveSpeed = 10;
    private Vector3 currentPosition;
    private int currentNode;
    private bool direction = true;
    private int trailRendererTime = 18;
    //Change locations at runtime
    private float timeInEachLocation = 5f;
    public bool switchBetweenLocations = true;
    private int currentLocation;
    private float yOffset = 0.09f;


    // Start is called before the first frame update
    void Start()
    {
        if (!IsCreated)
        {
            radio = GameObject.FindGameObjectWithTag(ResourcePathManager.radioTag);
            kuriPosition = new Vector3(GameObject.Find("KURI").transform.position.x,
               GameObject.Find("KURI").transform.position.y, GameObject.Find("KURI").transform.position.z);
            radio.transform.LookAt(kuriPosition);
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
        else {
            SetRadioTrailRenderer(false);
            MoveRadioToLocation(0);
            currentLocation = 0;
            StartCoroutine(ChangeLocation());
            
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
        float radius = startingRadius;
        float radiusIncrement = (endingRadius - startingRadius) / locationNumber;
        for (int i = 0; i < locationNumber; i++)
        {
            
            //Calculating location position on the circle around Kuri and its rotation on the y axis
            float locationPosition = (float)i / (float)locationNumber;
            float x = Mathf.Sin(locationPosition * Mathf.PI * 2.0f + Mathf.PI / 4) * radius + kuriPosition.x;
            float z = Mathf.Cos(locationPosition * Mathf.PI * 2.0f + Mathf.PI / 4) * radius + kuriPosition.z;
            radioLocations[i] = Instantiate(locationPrefab, new Vector3(x, fixedHeight, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            radioLocations[i].transform.parent = GameObject.Find("RadioLocations").transform;
            radioLocations[i].GetComponent<ChangeRadioLocation>().SetRadio(radio);
            radioLocations[i].GetComponent<ChangeRadioLocation>().SetOrigin(kuriPosition);
            radius += radiusIncrement;
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
            radio.transform.LookAt(kuriPosition);
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

    IEnumerator ChangeLocation() {
        while (true) {
            if (currentLocation == radioLocations.Length - 1)
                currentLocation = 0;
            else
                currentLocation++;
            MoveRadioToLocation(currentLocation);
           
            yield return new WaitForSeconds(timeInEachLocation);
        }
    }

    private void MoveRadioToLocation(int index) {
        radio.transform.position = new Vector3(radioLocations[index].transform.position.x,
            radioLocations[index].transform.position.y + yOffset,
            radioLocations[index].transform.position.z);
        radio.transform.LookAt(kuriPosition);
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
