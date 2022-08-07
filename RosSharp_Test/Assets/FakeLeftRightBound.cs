using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class FakeLeftRightBound : MonoBehaviour {
        #region members
        public Vector2 LeftBound;
        public Vector2 RightBound;
        #endregion
        #region unity
        List<Color> CornerColors = new List<Color>() { Color.red, Color.blue, Color.yellow, Color.green }; //Different colors for different corners
        List<Vector3> VerticeList;
        List<Vector3> VerticeListToShow;
        public float sphereSize = 0.005f;
        // void OnDrawGizmos() {
        //     int b = 0;
        //     if (VerticeList.Count > 0)
        //         for (int a = 0; a < VerticeListToShow.Count; a++) {
        //             Gizmos.color = CornerColors[b++];
        //             Gizmos.DrawSphere(VerticeListToShow[a], sphereSize); //show coordinate as a sphere on editor
        //         }
        // }
        public static bool CloseEnough(Vector2 _v1, Vector2 _v2, Vector2 _e) {
            return System.Math.Abs(_v1.x - _v2.x) <= _e.x &&
                   System.Math.Abs(_v1.y - _v2.y) <= _e.y;
        }
        void Start() {
            VerticeList = new List<Vector3>(GetComponent<MeshFilter>().sharedMesh.vertices); //get vertice points from the mesh of the object
            VerticeListToShow = new List<Vector3>();
            // 0, 10, 15
            VerticeListToShow.Add(transform.TransformPoint(VerticeList[0])); //corner points are added to show  on the editor
            VerticeListToShow.Add(transform.TransformPoint(VerticeList[10]));
            VerticeListToShow.Add(transform.TransformPoint(VerticeList[15]));
            VerticeListToShow.Add(transform.TransformPoint(VerticeList[18]));

            // start with left arbitrarily chosen
            LeftBound = new Vector2(VerticeListToShow[0].x, VerticeListToShow[0].z);
            RightBound = new Vector2(VerticeListToShow[1].x, VerticeListToShow[1].z);
            if (CloseEnough(LeftBound, RightBound, new Vector2(0.1f, 0.1f))) {
                RightBound = new Vector2(VerticeListToShow[2].x, VerticeListToShow[2].z);
            }
            if (CloseEnough(LeftBound, RightBound, new Vector2(0.1f, 0.1f))) {
                RightBound = new Vector2(VerticeListToShow[3].x, VerticeListToShow[3].z);
            }
            // check which is left of plane normal
            Vector3 normal = transform.forward;
            Vector2 normalFlattened = new Vector2(normal.x, normal.z);
            // check if Leftbound is left of normal, swap if so
            if (Vector2.Dot(normalFlattened, LeftBound) < 0) {
                Vector2 temp = LeftBound;
                LeftBound = RightBound;
                RightBound = temp;
            }
        }
        #endregion
        #region public
        #endregion
        #region private
        #endregion
    }
}
