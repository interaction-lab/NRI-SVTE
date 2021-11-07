using RosSharp.RosBridgeClient.MessageTypes.Nav;
using System;
using UnityEngine;

namespace RosSharp.RosBridgeClient {
    public class MapSubscriber : UnitySubscriber<OccupancyGrid> {
        public MeshFilter objMesh;
        Mesh mesh;
        MapMetaData mapMetaData;
        sbyte[] data;
        Vector3[] newVertices;
        int[] newTriangles;
        Vector2[] newUV;
        Color[] newColors;

        bool mapReceived = false;
        
        void Start()
        {
            base.Start();
            mesh = new Mesh();
            mesh.name = "OccupancyGridMesh";
            objMesh.GetComponent<MeshFilter>().mesh = mesh;
        }

        private void Update() {
            if (mapReceived) // must be from main thread
            {
                mapReceived = false;
                DrawMap();
            }
        }

        protected override void ReceiveMessage(OccupancyGrid message) {

            mapMetaData = message.info;
            data = message.data;
            mapReceived = true;
        }

        private void DrawMap() {
            int xSize = (int)mapMetaData.width;
            int ySize = (int)mapMetaData.height;
           
            newTriangles = new int[xSize * ySize * 6];
            newVertices = new Vector3[(xSize + 1) * (ySize + 1)];
            newUV = new Vector2[newVertices.Length];
            newColors = new Color[newVertices.Length];
   
      
            for (int i = 0, y = 0; y <= ySize; y++)
            {
                for (int x = 0; x <= xSize; x++, i++)
                {
                    newVertices[i] = new Vector3(x, y);
                   
                    int index = (int)(x + mapMetaData.width * y);
                    if(index < data.Length) { 
                        if(data[index] >= 0) 
                        {
                            float ch = (1 - data[index] / 100.0f);
                            newColors[i] = new Color(ch,ch,ch,1);
                        
                        }
                        else
                        {
                            newColors[i] = new Color(0, 0, 1, 1);
                        }
                    }
                }
            }

            for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
            {
                for (int x = 0; x < xSize; x++, ti += 6, vi++)
                {
                    newTriangles[ti] = vi;
                    newTriangles[ti + 3] = newTriangles[ti + 2] = vi + 1;
                    newTriangles[ti + 4] = newTriangles[ti + 1] = vi + xSize + 1;
                    newTriangles[ti + 5] = vi + xSize + 2;
                }
            }

            mesh.vertices = newVertices;
           // mesh.uv = newUV;
            mesh.triangles = newTriangles;
            mesh.colors = newColors;
        }
    }
}
