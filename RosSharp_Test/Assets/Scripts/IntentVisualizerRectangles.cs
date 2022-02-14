using NLUDataTypes;
using UnityEngine;
using TMPro;

namespace RosSharp.RosBridgeClient
{
    public class IntentVisualizerRectangles : IntentVisualizer
    {
        private GameObject[] rectangles;
        private float combinedLength = 2.0f;
        private Vector3 firstRectanglePosition = new(-2.0f,0.516f, 0f);
        private float fixedHeight = 0.20f;
        private float fixedDepth = 0.00f;
        private float scale = 0.5f;
        public int FontSize = 128;
        private float textScale = 0.005f;
        private float textHeight = 0.15f;
        private float enlargeFactor = 1.25f;
        private float lineWidth = 0.01f;
        private float linePadding = 0.02f;
        private float textHeightDifference = 0.07f;

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
                    Vector3 position = GetNextRectanglePosition(rectangles[i-1].transform.position, 
                        scale*intentList[i-1].Confidence* combinedLength, scale*intentList[i].Confidence * combinedLength);
                    rectangles[i] = CreateRectangle(position, scale * intentList[i].Confidence, intentColors[i]);
                    print(intentColors[i]);
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
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.SetPosition(1, GetRectangleStartingPosition(rectangle));
            lineRenderer.SetPosition(0, GetTextStartingPosition(text));
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.material = new Material((Material)Resources.Load(ResourcePathManager.dottedLineMatPath));
            lineRenderer.GetComponent<Renderer>().sortingLayerName = "Default";
            lineRenderer.GetComponent<Renderer>().sortingOrder = 1;
        }

        private Vector3 GetTextStartingPosition(GameObject text)
        {
            return new Vector3(text.transform.position.x - 
                Mathf.Sign(text.transform.position.x) * text.transform.localScale.x/2
                - linePadding * Mathf.Sign(text.transform.position.x),
                text.transform.position.y - Mathf.Sign(text.transform.position.y) * text.transform.localScale.y / 2,
                text.transform.position.z);
        }
        private Vector3 GetRectangleStartingPosition(GameObject rectangle)
        {
            return new Vector3(
                rectangle.transform.position.x -
                Mathf.Sign(rectangle.transform.position.x) * rectangle.transform.localScale.x/2
                - linePadding * Mathf.Sign(rectangle.transform.position.x),
                rectangle.transform.position.y +
                Mathf.Sign(rectangle.transform.position.y) * rectangle.transform.localScale.y / 2,
                rectangle.transform.position.z);
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
            rectangle.transform.position = new Vector3(position.x,
                        position.y, position.z);
            
            rectangle.transform.localScale = new Vector3(localScale, fixedHeight, fixedDepth);
            
            rectangle.GetComponent<Renderer>().material.color = new Color(color.r / 255, color.g / 255, color.b /255);

           
            return rectangle;
        }

        private GameObject SetIntentNameText(GameObject rectangle, string intentName, int index)
        {
            GameObject intentText = new GameObject(intentName);
            TextMesh text = intentText.AddComponent<TextMesh>();
            text.text = "<b>"+intentName+"</b>";
            
            text.transform.localScale = new Vector3(textScale, textScale, textScale);
            text.transform.position = new Vector3(rectangle.transform.position.x
                - rectangle.transform.localScale.x / 2 * Mathf.Sign(rectangle.transform.position.x)
                , rectangle.transform.position.y 
                + fixedHeight/2 + textHeight + textHeightDifference*(index%3),
                rectangle.transform.position.z);
            text.fontSize = FontSize;
            text.color = new Color(0,0,0);
            text.transform.eulerAngles = new Vector3(0,180,0); 
            Font futura = Resources.Load<Font>(ResourcePathManager.futuraPath);
            text.font = futura;
            text.GetComponent<Renderer>().material = new Material(futura.material);
            text.GetComponent<Renderer>().sortingLayerName= "Default";
            text.GetComponent<Renderer>().sortingOrder = 2;
            return intentText;
        }

        private void SetIntentConfidenceText(GameObject rectangle, float confidence, string intentName)
        {
            GameObject intentText = new GameObject(intentName + "_confidence");
            TextMesh text = intentText.AddComponent<TextMesh>();
            text.text = "<b>" + confidence*100 + "%" + "</b>";
            text.transform.localScale = new Vector3(textScale, textScale, textScale);
            text.transform.position = new Vector3(rectangle.transform.position.x, rectangle.transform.position.y,
                rectangle.transform.position.z);
            text.fontSize = FontSize;
            text.anchor = new TextAnchor();
            text.anchor = TextAnchor.MiddleCenter;
            text.alignment = TextAlignment.Center;
            text.color = new Color(0, 0, 0);
            text.transform.eulerAngles = new Vector3(0, 180, 0);
            Font futura = Resources.Load<Font>(ResourcePathManager.futuraPath);
            text.font = futura;
            text.GetComponent<Renderer>().material = new Material(futura.material);
            text.GetComponent<Renderer>().sortingLayerName = "Default";
            text.GetComponent<Renderer>().sortingOrder = 2;
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
