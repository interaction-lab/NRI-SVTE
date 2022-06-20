using System.Collections;
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
    private bool IsCreated = false;
    private float fixedHeight = 0.2f;
    private readonly float startingRadius = 0.8f;
    private float endingRadius = 2;
    private float timer = 0;
    public float moveSpeed = 0.1f;
    private Vector3 currentPosition;
    private int currentNode;
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
        if (AutomaticMovement == true)
            SetStartingRadioPosition();
        else {
            MoveRadioToLocation(0);
            currentLocation = 0;
            StartCoroutine(ChangeLocation());  
        }
        IsCreated = true;
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
        currentPosition = new Vector3(radioLocations[currentNode].transform.position.x,
            radioLocations[currentNode].transform.position.y + yOffset,
            radioLocations[currentNode].transform.position.z);
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
    }

    private void UpdateAutomaticPosition()
    {
        timer += Time.deltaTime * moveSpeed;
        if (Vector3.Distance(radio.transform.position,currentPosition) != 0)
        {
            Vector3 newPosition = Vector3.Lerp(radio.transform.position, currentPosition, timer);
            radio.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            radio.transform.LookAt(kuriPosition);
        }
        else
        {
            if (currentNode < radioLocations.Length - 1)
                currentNode += 1;
               
            else 
                currentNode = 0;
            UpdateCurrentPosition();
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
            UpdateAutomaticPosition();
        
    }

}
