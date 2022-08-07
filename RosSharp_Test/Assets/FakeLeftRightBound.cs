using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class FakeLeftRightBound : MonoBehaviour {
        #region members
        public Vector2 LeftBound, RightBound;
        #endregion
        #region unity
        void Start() {
            List<Vector3> VerticeList = new List<Vector3>(GetComponent<MeshFilter>().sharedMesh.vertices); //get vertice points from the mesh of the object
            List<Vector3> VerticeListToShow = new List<Vector3>();
            VerticeListToShow.Add(transform.TransformPoint(VerticeList[0])); //corner points are added to show  on the editor
            VerticeListToShow.Add(transform.TransformPoint(VerticeList[10]));
            VerticeListToShow.Add(transform.TransformPoint(VerticeList[110]));
            VerticeListToShow.Add(transform.TransformPoint(VerticeList[120]));

            // start with left arbitrarily chosen
            LeftBound = new Vector2(VerticeListToShow[0].x, VerticeListToShow[0].z);
            RightBound = new Vector2(VerticeListToShow[1].x, VerticeListToShow[1].z);
            if (RightBound == LeftBound) {
                RightBound = new Vector2(VerticeListToShow[2].x, VerticeListToShow[2].z);
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
