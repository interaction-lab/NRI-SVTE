using RosSharp.RosBridgeClient.MessageTypes.Std;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
 
    public class AudioVisualizerSpheres : AudioVisualizer
    {
        public GameObject spherePrefab;
        private readonly int sphereNumber = 32;
        private readonly int rowsOfSpheres = 3;
        private GameObject[,] audioSpheres;
        private readonly float distanceBetweenRows = 0.15f;
        private readonly float offsetY = 0.1f;
        private readonly float localScale = 0.05f;
        private bool IsCreated = false;
        private float positionScale = 0.2f;
        private readonly float scaleRow = 0.01f;
        private readonly float inflatingCoefficient = 2.0f;
        private Vector3[,] originalDimensions;
        
        private void Create()
        {
            audioSpheres = new GameObject[rowsOfSpheres,sphereNumber];
            originalDimensions = new Vector3[rowsOfSpheres, sphereNumber];
            float midrow = (rowsOfSpheres - 1) / 2;
            for (int row = 0; row < rowsOfSpheres; row++)
            {
                //Getting y of the transform position of the spheres on the row 
                float yPositionRow = row * distanceBetweenRows;
                //Getting the offset of localScale of the row with respect to the middle row. Spheres get gradually smaller
                //the farther from the middle row
                float rowScaleOffset =(float) Mathf.Abs(midrow-row)*scaleRow;
                positionScale -= rowScaleOffset;
                for (int i = 0; i < sphereNumber; i++) {
                    //Getting sphere x and y transform position on a circle around Kuri
                    float spherePosition = (float)i / (float)sphereNumber;
                    float x = Mathf.Sin(spherePosition * Mathf.PI * 2.0f + Mathf.PI / 4);
                    float z = Mathf.Cos(spherePosition * Mathf.PI * 2.0f + Mathf.PI / 4);
                    audioSpheres[row, i] = Instantiate(spherePrefab,
                        new Vector3(positionScale * x, positionScale * yPositionRow - offsetY, positionScale * z),
                        Quaternion.Euler(0, 0, 0));
                    audioSpheres[row, i].transform.parent = GameObject.Find("Microphones").transform;
                    audioSpheres[row,i].transform.localScale = new Vector3(localScale,localScale,localScale);
                    audioSpheres[row,i].transform.localScale -= new Vector3(rowScaleOffset, rowScaleOffset, rowScaleOffset);
                    originalDimensions[row, i] = audioSpheres[row, i].transform.localScale;
                }
                IsCreated = true;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            if(IsCreated == false) 
            {
                Create();
                IsCreated = true;
            
            }
            
        }

        public override void Visualize(Float64MultiArray audioRecording)
        {
            if (!IsCreated)
                Create();
            //Gets the loudness heard by each microphone in the audioRecording
            double[] loudness = GetLoudness(audioRecording);
            for(int row = 0; row < rowsOfSpheres; row++)
            {
                for (int i = 0; i < sphereNumber; i++)
                {
                    //Gets the loudness heard by sphere i
                    float sphereLoudness = GetObjectLoudness(loudness, i, sphereNumber);
                    Color sphereColor = GetInterpolatedColor(Color.red, Color.green, sphereLoudness);
                    print("Sphere color is: " + sphereColor);
                    audioSpheres[row, i].transform.localScale = GetInflation(sphereLoudness,row,i);
                    Renderer sphereRenderer = audioSpheres[row, i].GetComponent<Renderer>();
                    sphereRenderer.material.color = sphereColor;
                }
            }
            
        }

        private Vector3 GetInflation(float sphereLoudness,int row,int sphereIndex)
        {
            if(sphereLoudness == 0)
            {
                return originalDimensions[row,sphereIndex];
            }
            else
            {
                return (sphereLoudness+1) * inflatingCoefficient * originalDimensions[row, sphereIndex];
            }
        }
        

        protected override void DestroyObjects()
        {
            if(audioSpheres != null)
            {
                for(int row = 0; row< rowsOfSpheres; row++)
                {
                    for(int i = 0; i < sphereNumber; i++)
                    {
                        Destroy(audioSpheres[row,i]);
                    }
                }
            }
        }
    }
}
