using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class PlaneMathLibrary {
        #region members
        #endregion
        #region unity
        #endregion
        #region public
        public static bool CloseEnough(Vector2 a, Vector2 b, float threshold) {
            return Vector2.Distance(a, b) < threshold;
        }
        //Find the line of intersection between two planes. The planes are defined by a normal and a point on that plane.
        //The outputs are a point on the line and a vector which indicates it's direction. If the planes are not parallel, 
        //the function outputs true, otherwise false.
        public static bool PlanePlaneIntersection(out Vector3 linePoint, out Vector3 lineVec, Vector3 plane1Normal, Vector3 plane1Position, Vector3 plane2Normal, Vector3 plane2Position) {
            linePoint = Vector3.zero;
            lineVec = Vector3.zero;
            lineVec = Vector3.Cross(plane1Normal, plane2Normal);
            Vector3 ldir = Vector3.Cross(plane2Normal, lineVec);
            float denominator = Vector3.Dot(plane1Normal, ldir);
            if (Mathf.Abs(denominator) > 0.006f) {
                Vector3 plane1ToPlane2 = plane1Position - plane2Position;
                float t = Vector3.Dot(plane1Normal, plane1ToPlane2) / denominator;
                linePoint = plane2Position + t * ldir;
                return true;
            }
            else {
                return false;
            }
        }
        #endregion
        #region private
        #endregion
    }
}
