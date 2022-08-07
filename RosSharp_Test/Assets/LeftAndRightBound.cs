using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace NRISVTE {
    public class LeftAndRightBound : MonoBehaviour {
        #region members
        public Vector2 LeftBound = Vector2.zero, RightBound = Vector2.zero;
        ARPlane _arplane;
        ARPlane aRPlane {
            get {
                if (_arplane == null) {
                    _arplane = GetComponent<ARPlane>();
                }
                return _arplane;
            }
        }
        #endregion
        #region unity
        #endregion
        #region public
        #endregion
        #region private
        private void ArPlane_BoundaryChanged(ARPlaneBoundaryChangedEventArgs obj) {
            if (obj.plane == aRPlane) {
                // m_Boundary[0] = new Vector2(-extents.x, -extents.y);
                // m_Boundary[1] = new Vector2(-extents.x, extents.y);
                // m_Boundary[2] = new Vector2(extents.x, extents.y);
                // m_Boundary[3] = new Vector2(extents.x, -extents.y);
                // I just want the x value of the boundary
                float negX = aRPlane.boundary[0].x;
                float posX = aRPlane.boundary[2].x;
                Vector3 center = aRPlane.center;
                Vector3 normal = aRPlane.normal;

                Vector2 leftVec = new Vector2(negX, 0);
                Vector2 rightVec = new Vector2(posX, 0);
                Vector2 normalFlattened = new Vector2(normal.x, normal.z);
                Vector2 centerFlattened = new Vector2(center.x, center.z);

                Vector2 perpendicularRightOfNormal = Vector2.Perpendicular(normalFlattened).normalized;
                Vector2 perpendicularLeftOfNormal = -perpendicularRightOfNormal;
                LeftBound = centerFlattened + perpendicularLeftOfNormal * negX;
                RightBound = centerFlattened + perpendicularRightOfNormal * posX;
            }
        }
        #endregion
    }
}
