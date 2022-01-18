using UnityEngine;


namespace RosSharp.RosBridgeClient { 
    public class MicrophonesManager : AudioProvider
    {
        public GameObject audioSource;
        public GameObject spherePrefab;
        private GameObject[] microphones = new GameObject[4];
        //Y of the microphones transform position
        private readonly float height = -0.05f;
        //Local scale of the microphones
        private readonly float localScale = 0.035f;
        private bool IsCreated = false;
        //Value by which x and z transform position of the microphones are scaled
        private readonly float positionScale = 0.1f;
        //Value in meters from which the microphones pick up sound as if they were in the source
        private readonly float hearingThreshold = 1.0f;
        private readonly float hearingAngle = 60;


        public void Create()
        {
            for (int i = 0; i < microphones.Length; i++)
            {
                float microphonePosition = (float)i / (float)microphones.Length;
                float x = Mathf.Sin(microphonePosition * Mathf.PI * 2.0f + Mathf.PI / 4);
                float z = Mathf.Cos(microphonePosition * Mathf.PI * 2.0f + Mathf.PI / 4);
                microphones[i] = Instantiate(spherePrefab,
                    new Vector3(positionScale * x, height, positionScale * z),
                    Quaternion.Euler(0, 0, 0));
                microphones[i].transform.parent = GameObject.Find("Microphones").transform;
                microphones[i].transform.localScale = new Vector3(localScale, localScale, localScale);
            }
            IsCreated = true;
        }
        // Start is called before the first frame update
        void Start()
        {
            if(IsCreated == false)
            {
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

        protected override void SetMessage()
        {
            float radius;
            float angle;
            float soundIntensityAtSource;
            for (int i = 0; i < microphones.Length; i++)
            {
                //Distance between a microphone and the audioSource positions'
                angle = Vector3.SignedAngle(microphones[i].transform.position, audioSource.transform.position, microphones[i].transform.position);
                print("Microphone is number " + i + " angle is " + angle);
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
