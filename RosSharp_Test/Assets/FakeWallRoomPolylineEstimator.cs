using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace NRISVTE {
    public class FakeWallRoomPolylineEstimator : Singleton<FakeWallRoomPolylineEstimator> {
        #region members
        KuriTransformManager kuriTransformManager;
        KuriTransformManager KuriT {
            get {
                if (kuriTransformManager == null) {
                    kuriTransformManager = KuriManager.instance.GetComponent<KuriTransformManager>();
                }
                return kuriTransformManager;
            }
        }
        ARPlaneManager _planeManager;
        ARPlaneManager ARPlaneManager_ {
            get {
                if (_planeManager == null) {
                    _planeManager = FindObjectOfType<ARPlaneManager>();
                }
                return _planeManager;
            }
        }
        List<ARPlane> WallARPlanes {
            get {
                List<ARPlane> wallARPlanes = new List<ARPlane>();
#if !UNITY_EDITOR
                foreach (ARPlane plane in ARPlaneManager_.trackables) {
                    if (plane.alignment == PlaneAlignment.Vertical) {
                        wallARPlanes.Add(plane);
                    }
                }
#else
                wallARPlanes = BaseRoomTester.instance.FakePlanes;
#endif
                return wallARPlanes;
            }
        }
        #endregion
        #region unity
        #endregion
        #region public
        public List<List<float>> GetWallPolyLines() {
            List<List<float>> res = new List<List<float>>();
            // go through all WallARPlane get get the right and left bounds
#if !UNITY_EDITOR
            // this likely needs sorting but going to use this for now
            res = CalculateIntersectionPoints(res);
            // foreach (ARPlane plane in WallARPlanes) {
            //     // going to use plane intersection to get the right and left bounds of the wall

            //     // LeftAndRightBound leftAndRightBound = plane.GetComponent<LeftAndRightBound>();
            //     // if (leftAndRightBound != null) {
            //     //     res.Add(new List<float>() { leftAndRightBound.LeftBound.x, leftAndRightBound.LeftBound.y });
            //     //     res.Add(new List<float>() { leftAndRightBound.RightBound.x, leftAndRightBound.RightBound.y });
            //     // }
            // }
#else
            // fake
            foreach (ARPlane plane in WallARPlanes) {
                FakeLeftRightBound leftAndRightBound = plane.GetComponent<FakeLeftRightBound>();
                if (leftAndRightBound != null) {
                    res.Add(new List<float>() { leftAndRightBound.LeftBound.x, leftAndRightBound.LeftBound.y });
                    res.Add(new List<float>() { leftAndRightBound.RightBound.x, leftAndRightBound.RightBound.y });
                }
            }
#endif
            // convert to kuri space
            ConvertToKuriSpace(res);
            return res;
        }
        #endregion
        #region private
        private List<List<float>> CalculateIntersectionPoints(List<List<float>> res) {
            // make a List of all plane centers and normals
            List<Vector3> planeCenters = new List<Vector3>();
            List<Vector3> planeNormals = new List<Vector3>();
            foreach (ARPlane plane in WallARPlanes) {
                planeCenters.Add(plane.center);
                planeNormals.Add(plane.normal);
            }

            // calculate the intersection points of the planes
            List<List<Vector3>> planeIntersections = new List<List<Vector3>>();
            // go through all plane centers and normals and get the intersection points for each
            for (int i = 0; i < planeCenters.Count; i++) {
                Vector3 planeCenter = planeCenters[i];
                Vector3 planeNormal = planeNormals[i];
                planeIntersections.Add(new List<Vector3>());
                // go through all other plane centers and normals and get the intersection points
                for (int j = 0; j < planeCenters.Count; j++) {
                    if (i == j) {
                        continue;
                    }
                    Vector3 otherPlaneCenter = planeCenters[j];
                    Vector3 otherPlaneNormal = planeNormals[j];
                    Vector3 intersectionPointCenter = Vector3.zero;
                    Vector3 intersectionLine = Vector3.zero;
                    bool isValid = PlaneMathLibrary.PlanePlaneIntersection(
                        out intersectionPointCenter,
                        out intersectionLine,
                        planeCenter,
                        planeNormal,
                        otherPlaneCenter,
                        otherPlaneNormal);
                    // get points closet to the center for each poiint
                    if (isValid) {
                        planeIntersections[i].Add(intersectionPointCenter);
                    }
                }
            }

            // for each intersection point, find the closest 2 to that plane's center and add it to the result
            List<List<Vector2>> closestPointPairs = new List<List<Vector2>>();
            int index = -1;
            foreach (List<Vector3> intersectionPoints in planeIntersections) {
                ++index;
                if (intersectionPoints.Count == 0) {
                    continue;
                }
                Vector3 closestPoint = Vector3.zero;
                Vector3 secondClosestPoint = Vector3.zero;
                float closestDistance = float.MaxValue;
                float secondClosestDistance = float.MaxValue;
                foreach (Vector3 intersectionPoint in intersectionPoints) {
                    float distance = Vector3.Distance(intersectionPoint, planeCenters[index]);
                    if (distance < closestDistance) {
                        secondClosestDistance = closestDistance;
                        secondClosestPoint = closestPoint;
                        closestDistance = distance;
                        closestPoint = intersectionPoint;
                    }
                    else if (distance < secondClosestDistance) {
                        secondClosestDistance = distance;
                        secondClosestPoint = intersectionPoint;
                    }
                }
                closestPointPairs.Add(new List<Vector2>());
                closestPointPairs[index].Add(new Vector2(closestPoint.x, closestPoint.z));
                closestPointPairs[index].Add(new Vector2(secondClosestPoint.x, secondClosestPoint.z));
            }

            // match points to sort them in order

            Vector2 startingPoint = closestPointPairs[0][0];
            res.Add(new List<float>() { startingPoint.x, startingPoint.y });
            Vector2 curPoint = closestPointPairs[0][1];
            res.Add(new List<float>() { curPoint.x, curPoint.y });
            // remove the first point from the list of closest points
            closestPointPairs.RemoveAt(0);
            // find the corresponding closest point to curPoint in closestPointPairs and add it to the result
            int fIndex = -1;
            float threshold = 0.01f;
            bool foundPoint = true;
            while (closestPointPairs.Count > 0 && foundPoint) {
                foundPoint = false;
                foreach (List<Vector2> pointPair in closestPointPairs) {
                    ++fIndex;
                    if (PlaneMathLibrary.CloseEnough(curPoint, pointPair[0], threshold)) {
                        res.Add(new List<float>() { pointPair[1].x, pointPair[1].y });
                        curPoint = pointPair[1];
                        closestPointPairs.RemoveAt(fIndex);
                        foundPoint = true;
                        break;
                    }
                    else if (PlaneMathLibrary.CloseEnough(curPoint, pointPair[1], threshold)) {
                        res.Add(new List<float>() { pointPair[0].x, pointPair[0].y });
                        curPoint = pointPair[0];
                        closestPointPairs.RemoveAt(fIndex);
                        foundPoint = true;
                        break;
                    }
                }
            }

            return res;
        }
        private void ConvertToKuriSpace(List<List<float>> polylines) {
            for (int i = 0; i < polylines.Count; i++) {
                polylines[i][0] = (polylines[i][0] - KuriT.Position.x) * 100;
                polylines[i][1] = (polylines[i][1] - KuriT.Position.z) * 100;
            }
        }
    }
    #endregion
}

