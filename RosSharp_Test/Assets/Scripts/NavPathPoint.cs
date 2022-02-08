using UnityEngine;

namespace NRISVTE {
    /// <summary>
    /// Class that can hold a Vector3 point of a dynamic transform point
    /// Will prioritize the transform's point if set
    /// Else will return the static Vector3 point
    /// </summary>
    public class NavPathPoint {
        Transform pointsTransform = null;
        Vector3 pointsPosition = Vector3.zero;
        public Vector3 PointPosition {
            get {
                if (pointsTransform)
                {
                    return pointsTransform.position;
                }
                return pointsPosition;
            }
        }

        public NavPathPoint(Transform t) {
            Set(t);
        }

        public NavPathPoint(Vector3 v) {
            pointsPosition = v;
        }

        public void Set(Transform t) {
            pointsTransform = t;
        }

        public void Set(Vector3 v) {
            pointsPosition = v;
        }

        public void Reset() {
            pointsPosition = Vector3.zero;
            pointsTransform = null;
        }
    }
}