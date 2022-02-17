using UnityEngine;


namespace RosSharp.RosBridgeClient { 
    public class MicrophonesManager : AudioProvider
    {
        private GameObject audioSource;
        private GameObject microphonePrefab;
        private GameObject[] microphones = new GameObject[4];
        //Y of the microphones transform position
        private readonly float height = 0.38f;
        //Local scale of the microphones
        private readonly float localScale = 0.035f;
        private bool IsCreated = false;
        //Value by which x and z transform position of the microphones are scaled
        private readonly float positionScale = 0.10f;
        //Value in meters from which the microphones pick up sound as if they were in the source
        private readonly float hearingThreshold = 1.0f;
        private readonly float hearingAngle = 70;
        //TODO: Figure out why it does not update position in correct way
        private Vector3 kuriPosition = new(2,0.1f,0);


        public void Create()
        {
            Debug.Log("kuri position is " + kuriPosition);
            for (int i = 0; i < microphones.Length; i++)
            {
                float microphonePosition = (float)i / (float)microphones.Length;
                
                float x = Mathf.Sin(microphonePosition * Mathf.PI * 2.0f + Mathf.PI / 4);
                float z = Mathf.Cos(microphonePosition * Mathf.PI * 2.0f + Mathf.PI / 4);
                Debug.Log(x);
                Debug.Log(z);
                microphones[i] = Instantiate(microphonePrefab,
                    new Vector3(positionScale * x + kuriPosition.x, height, positionScale *z + kuriPosition.z),
                    Quaternion.Euler(0, 0, 0));
                microphones[i].transform.localPosition = new Vector3(positionScale * x + kuriPosition.x,
                    height, positionScale * z + kuriPosition.z);
                microphones[i].transform.parent = GameObject.Find("Microphones").transform;
                microphones[i].transform.localScale = new Vector3(localScale, localScale, localScale);
                GameObject angleCalculator = new GameObject("AngleCalculator_" + i);
                angleCalculator.transform.parent = microphones[i].transform;
                angleCalculator.transform.localPosition = Vector3.zero;
                angleCalculator.transform.eulerAngles = new Vector3(0,45 + 90 * i);

            }
            IsCreated = true;
        }
        // Start is called before the first frame update
        void Start()
        {
           
            if (IsCreated == false)
            {
                kuriPosition = new Vector3(GameObject.Find("KURI").transform.position.x,
              GameObject.Find("KURI").transform.position.y, GameObject.Find("KURI").transform.position.z);
                microphonePrefab = Resources.Load<GameObject>(ResourcePathManager.microphonePath);
                audioSource = GameObject.FindGameObjectWithTag(ResourcePathManager.radioTag);
                Create();
                IsCreated = true;
            }
            audioRecording.data = new double[4] { loudnessMin, loudnessMin, loudnessMin, loudnessMin };
            audioRecording.layout.dim = new RosSharp.RosBridgeClient.MessageTypes.Std.MultiArrayDimension[1];
            audioRecording.layout.dim[0] = new RosSharp.RosBridgeClient.MessageTypes.Std.MultiArrayDimension
            {
                size = 4
            };
            audioVisualizers = GetComponents<AudioVisualizer>();
        }

        // Update is called once per frame
        void Update()
        {
            SetMessage();
            for (int i = 0; i < audioVisualizers.Length; i++)
            {
                audioVisualizers[i].Visualize(audioRecording);
            }

        }

        //Gets the angle between the microphone with index and the and the audio source
        private float GetAngle(int index) {
            GameObject angleCalculator = GameObject.Find("AngleCalculator_"+index);
            float startingRotation = angleCalculator.transform.eulerAngles.y;
            angleCalculator.transform.LookAt(audioSource.transform);
            float angle = angleCalculator.transform.eulerAngles.y - startingRotation;
            angleCalculator.transform.eulerAngles = new Vector3(0,45 + 90*index, 0);
            return angle;
        }

        protected override void SetMessage()
        {
            float radius;
            float angle;
            float soundIntensityAtSource;
            for (int i = 0; i < microphones.Length; i++)
            {
                //Distance between a microphone and the audioSource positions'
                angle = GetAngle(i);
                
                radius = Vector3.Distance(microphones[i].transform.position, audioSource.transform.position);
                //Sound Intensity at the source
                soundIntensityAtSource = audioSource.GetComponent<AudioSource>().volume * (loudnessMax - loudnessMin);
                if (Mathf.Abs(angle) <= hearingAngle)
                {
                    if (radius <= hearingThreshold)
                    {
                        audioRecording.data[i] = soundIntensityAtSource;

                    }
                    else
                    {
                        audioRecording.data[i] = soundIntensityAtSource * Mathf.Pow(hearingThreshold, 2) /
                            Mathf.Pow(radius, 2);
                    }
                }
                else
                {
                    audioRecording.data[i] = 0;
                }
            }
        }
    }
}
