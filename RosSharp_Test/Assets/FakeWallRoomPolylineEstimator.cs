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
            List<Vector3> planeCenters, planeNormals;
            CreatePlaneCentersAndNormals(out planeCenters, out planeNormals);
            List<List<Vector3>> planeIntersections = FindPlaneIntersections(planeCenters, planeNormals);
            List<List<Vector2>> closestPointPairs = FindWithinPointClosestPairs(planeCenters, planeIntersections);
            CreatePolylineFromClosestPointPairs(res, closestPointPairs);
            return res;
        }

        private void CreatePolylineFromClosestPointPairs(List<List<float>> res, List<List<Vector2>> closestPointPairs) {
            if (closestPointPairs.Empty()) {
                return;
            }
            Vector2 startingPoint = closestPointPairs[0][0];
            res.Add(new List<float>() { startingPoint.x, startingPoint.y });
            Vector2 curPoint = closestPointPairs[0][1];
            res.Add(new List<float>() { curPoint.x, curPoint.y });
            closestPointPairs.RemoveAt(0);

            while (!closestPointPairs.Empty()) {
                float minDist = float.MaxValue;
                int minIndex = -1;
                bool isFirstPoint = true;
                for (int i = 0; i < closestPointPairs.Count; ++i) {
                    Vector2 point1 = closestPointPairs[i][0];
                    Vector2 point2 = closestPointPairs[i][1];
                    float dist1 = Vector2.Distance(point1, curPoint);
                    float dist2 = Vector2.Distance(point2, curPoint);
                    if (dist1 < minDist) {
                        isFirstPoint = true;
                        minDist = dist1;
                        minIndex = i;
                    }
                    if (dist2 < minDist) {
                        isFirstPoint = false;
                        minDist = dist2;
                        minIndex = i;
                    }
                }
                // whatever is matched is the same point essentially as what was last
                if (isFirstPoint) {
                    curPoint = closestPointPairs[minIndex][1];
                }
                else {
                    curPoint = closestPointPairs[minIndex][0];
                }
                res.Add(new List<float>() { curPoint.x, curPoint.y });
                // remove found point pair from list
                closestPointPairs.RemoveAt(minIndex);
            }
        }

        private List<List<Vector2>> FindWithinPointClosestPairs(List<Vector3> planeCenters, List<List<Vector3>> planeIntersections) {
            List<List<Vector2>> closestPointPairs = new List<List<Vector2>>();

            int index = -1;
            foreach (List<Vector3> intersectionPoints in planeIntersections) {
                ++index;
                if (intersectionPoints.Count == 0) { // nothing intersected with this plane which is odd
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

            return closestPointPairs;
        }

        private List<List<Vector3>> FindPlaneIntersections(List<Vector3> planeCenters, List<Vector3> planeNormals) {
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

            return planeIntersections;
        }

        private void CreatePlaneCentersAndNormals(out List<Vector3> planeCenters, out List<Vector3> planeNormals) {
            planeCenters = new List<Vector3>();
            planeNormals = new List<Vector3>();
            foreach (ARPlane plane in WallARPlanes) {
                planeCenters.Add(plane.center);
                planeNormals.Add(plane.normal);
            }
        }

        private void ConvertToKuriSpace(List<List<float>> polylines) {
            for (int i = 0; i < polylines.Count; i++) {
                polylines[i][0] = (polylines[i][0] - KuriT.Position.x) * 100;
                polylines[i][1] = (polylines[i][1] - KuriT.Position.z) * 100;
            }
        }

        private void PrettyPrintClosestPointPairs(List<List<Vector2>> closestPointPairs) {
            Debug.Log("Closest Point Pairs:");
            string final = "";
            for (int i = 0; i < closestPointPairs.Count; ++i) {
                final += "Plane " + i + ": ";
                for (int j = 0; j < closestPointPairs[i].Count; ++j) {
                    final += "(" + closestPointPairs[i][j].x + ", " + closestPointPairs[i][j].y + ") ";
                }
                final += "\n";
            }
            Debug.Log(final);
        }
    }
    #endregion
}

