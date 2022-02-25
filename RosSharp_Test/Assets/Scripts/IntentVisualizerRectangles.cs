using NLUDataTypes;
using UnityEngine;
using TMPro;

namespace RosSharp.RosBridgeClient
{
    public class IntentVisualizerRectangles : IntentVisualizer
    {
        private GameObject[] rectangles;
        private float combinedLength = 2.0f;
        private Vector3 firstRectanglePosition = new(3.57f,0,-1f);
        private float fixedHeight = 1f;
        private float fixedDepth = 1f;
        private float scale = 1f;
        private int FontSize = 512;
        private float textScale = 0.005f;
        private float textHeight = 0.4f;
        private float enlargeFactor = 1.25f;
        private float lineWidth = 0.01f;
        private float linePadding = 0.06f;
        private float textHeightDifference = 0.18f;
        public bool IsThreeDimensional = true;
        private GameObject speechBubblePrefab;
        private float textPadding = 0.02f;

        void Start() {
            speechBubblePrefab = Resources.Load<GameObject>(ResourcePathManager.bubblePrefabPath);
        }

        public override void Disable()
        {
            if(rectangles == null)
                return;
            for(int i = 0; i < rectangles.Length; i++)
            {
                rectangles[i].SetActive(false);
            }
        }

        private void FreeRectangles()
        {
            for(int i = 0; i<rectangles.Length; i++)
            {
                rectangles[i].SetActive(false);
                Destroy(rectangles[i]);
            }
        }
        public override void Visualize(NLUIntent[] intentList)
        {
            rectangles = new GameObject[intentList.Length];
            GenerateIntentColors(intentList.Length);
            for(int i = 0; i < rectangles.Length; i++)
            {
                if (i == 0)
                {
                    rectangles[i] = CreateRectangle(firstRectanglePosition, scale * intentList[i].Confidence * combinedLength,
                        intentColors[i]);
                }
                else 
                {
                    Vector3 position = GetNextRectanglePosition(rectangles[i-1].transform.localPosition, 
                        scale*intentList[i-1].Confidence* combinedLength, scale*intentList[i].Confidence * combinedLength);
                    rectangles[i] = CreateRectangle(position, scale * intentList[i].Confidence, intentColors[i]);
                   
                }   
                GameObject text = SetIntentNameText(rectangles[i],intentList[i].Name,i);
                SetIntentConfidenceText(rectangles[i],intentList[i].Confidence, intentList[i].Name);
                SetLine(rectangles[i],text);
            }
            EnlargeIntentWithMostConfidence(0);
        }

        private void SetLine(GameObject rectangle, GameObject text)
        {
            GameObject line = new GameObject
            {
                name = text.name + "Line"
            };
            line.transform.parent = GameObject.Find("Background").transform;
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;
            SetLinePosition(rectangle,text,lineRenderer);
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.material = new Material((Material)Resources.Load(ResourcePathManager.dottedLineMatPath));
            lineRenderer.GetComponent<Renderer>().sortingLayerName = "Default";
            lineRenderer.GetComponent<Renderer>().sortingOrder = 1;
        }

        private void SetLinePosition(GameObject rectangle, GameObject text, LineRenderer lineRenderer) {
            float textX = text.transform.position.x -
                Mathf.Sign(text.transform.position.x) * text.transform.localScale.x / 2;
            float textY = text.transform.position.y - Mathf.Sign(text.transform.position.y) * 
                text.transform.localScale.y / 2 - Mathf.Sign(text.transform.position.y)* linePadding;
            Vector3 endPosition = new (textX, rectangle.transform.position.y, text.transform.position.z);
            Vector3 startPosition = new(textX, textY, text.transform.position.z);
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);
        }
       
        private void EnlargeIntentWithMostConfidence(int maxConfidenceIndex)
        {
            rectangles[maxConfidenceIndex].transform.localScale = new Vector3(
                rectangles[maxConfidenceIndex].transform.localScale.x,
                rectangles[maxConfidenceIndex].transform.localScale.y * enlargeFactor,
                rectangles[maxConfidenceIndex].transform.localScale.z * enlargeFactor
                );
        }

        

        private GameObject CreateRectangle(Vector3 position, float localScale, Color color)
        {
            GameObject rectangle; 
            rectangle = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rectangle.transform.parent = GameObject.Find("Background").transform;
            rectangle.transform.localPosition = new Vector3(position.x,
                        position.y, position.z);
            rectangle.transform.rotation = new Quaternion(0,0,0,0);
            int threeDimensionalFlag = 0;
            if (IsThreeDimensional)
                threeDimensionalFlag = 1;
            rectangle.transform.localScale = new Vector3(localScale, fixedHeight, fixedDepth*threeDimensionalFlag);
            
            rectangle.GetComponent<Renderer>().material.color = new Color(color.r / 255, color.g / 255, color.b /255);

           
            return rectangle;
        }

        private GameObject SetIntentNameText(GameObject rectangle, string intentName, int index)
        {
            GameObject intentText = new GameObject(intentName);
            intentText.transform.parent = GameObject.Find("Background").transform;
            TextMesh text = intentText.AddComponent<TextMesh>();
            text.text = "<b>"+intentName+"</b>";
            
            text.transform.localScale = new Vector3(textScale, textScale, textScale);
            text.transform.localPosition = new Vector3(rectangle.transform.localPosition.x
                - rectangle.transform.localScale.x / 2 * Mathf.Sign(rectangle.transform.localPosition.x)
                , rectangle.transform.localPosition.y 
                + fixedHeight/2 + textHeight + textHeightDifference*(index%3),
                rectangle.transform.localPosition.z);
            text.fontSize = FontSize;
            text.color = new Color(0,0,0);
            text.transform.eulerAngles = new Vector3(0,180,0); 
            Font futura = Resources.Load<Font>(ResourcePathManager.futuraPath);
            text.font = futura;
            text.anchor = new TextAnchor();
            text.anchor = TextAnchor.MiddleCenter;
            text.alignment = TextAlignment.Center;
            text.color = new Color(intentColors[index].r / 255, intentColors[index].g / 255
                , intentColors[index].b / 255);
            text.GetComponent<Renderer>().material = new Material(futura.material);
            text.GetComponent<Renderer>().sortingLayerName= "Default";
            text.GetComponent<Renderer>().sortingOrder = 2;
            SetTextBubble(intentText);
            return intentText;
        }

        private void SetTextBubble(GameObject text) {
            GameObject bubble = Instantiate(speechBubblePrefab,
                new Vector3(text.transform.position.x,
                    text.transform.position.y,
                    text.transform.position.z),
                Quaternion.Euler(0,0,0));
            bubble.transform.parent = GameObject.Find("Background").transform;
            bubble.transform.localPosition = new Vector3(text.transform.localPosition.x,
                    text.transform.localPosition.y,
                    text.transform.localPosition.z);
            MeshRenderer mesh = text.GetComponent<MeshRenderer>();
            bubble.transform.localScale = new Vector3(mesh.bounds.size.x + 5*textPadding,
                mesh.bounds.size.y + textPadding,
                mesh.bounds.size.z);
            //SpriteRenderer sr = bubble.GetComponentInChildren<SpriteRenderer>();
            //sr.size.Set(mesh.bounds.size.x + textPadding,mesh.bounds.size.y + textPadding);
           
        }
        private void SetIntentConfidenceText(GameObject rectangle, float confidence, string intentName)
        {
            GameObject intentText = new GameObject(intentName + "_confidence");
            TextMesh text = intentText.AddComponent<TextMesh>();
            text.text = "<b>" + confidence*100 + "%" + "</b>";
            text.transform.parent = GameObject.Find("Background").transform;
            text.transform.localScale = new Vector3(textScale, textScale, textScale);
            text.transform.localPosition = new Vector3(rectangle.transform.localPosition.x, rectangle.transform.localPosition.y - 
                fixedHeight * rectangle.transform.localScale.y,
                rectangle.transform.localPosition.z);
           
            text.fontSize = FontSize;
            text.anchor = new TextAnchor();
            text.anchor = TextAnchor.MiddleCenter;
            text.alignment = TextAlignment.Center;
            text.color = new Color(1, 1, 1);
            text.transform.eulerAngles = new Vector3(0, 180, 0);
            Font futura = Resources.Load<Font>(ResourcePathManager.futuraPath);
            text.font = futura;
            text.GetComponent<Renderer>().material = new Material(futura.material);
            text.GetComponent<Renderer>().sortingLayerName = "Default";
            text.GetComponent<Renderer>().sortingOrder = 2;
            SetTextBubble(intentText);
        }

        private Vector3 GetNextRectanglePosition(Vector3 lastRectanglePosition, 
            float lastRectangleScaleOnX, float nextRectangleScaleOnX)
        {
            float sign = Mathf.Sign(firstRectanglePosition.x);
            float nextX = (float)(lastRectanglePosition.x + (0.5 * lastRectangleScaleOnX + 0.5 * nextRectangleScaleOnX) * sign);
            return new Vector3(nextX, lastRectanglePosition.y, lastRectanglePosition.z);
        }
        

        
    }

   
}
