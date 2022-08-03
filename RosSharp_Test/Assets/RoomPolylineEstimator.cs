using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructures.ViliWonka.KDTree;
using System.Linq;

namespace NRISVTE {
    public class RoomPolylineEstimator : MonoBehaviour {
        #region members
        // up to down shot ray casts, these spawn east and west of kuri
        public RaycastHit[,] verticalRaycastHits; // west to east
        public RaycastHit[,] horizontalRaycastHits; // north to south
        static float cellSize = 0.1f; // in meters
        static int width = 100; // in cells
        static int height = 100; // in cells
        static float maxRayDist = 7f; // in meters
        static int numMissessAllowed = 5; // this is how many times rays can miss before we stop, we are likely out of the mesh at this point
        int layerMask;
        Vector2Int centerCords = new Vector2Int(width / 2, height / 2);

        KDTree kdTree;
        KDQuery query = new KDQuery();
        List<Vector3> pointHits = new List<Vector3>();
        int maxLeafNodes = 4; // higher number = faster building, but slower querying
        int numNearestToLookAt = 7; // higher number = slower querying, but more accurate/uses all hits
        KuriTransformManager _kuriT;
        public KuriTransformManager KuriT {
            get {
                if (_kuriT == null) {
                    _kuriT = KuriManager.instance.GetComponent<KuriTransformManager>();
                }
                return _kuriT;
            }
        }

        #endregion
        #region unity
        void Start() {
            verticalRaycastHits = new RaycastHit[width, 2]; // up, down
            horizontalRaycastHits = new RaycastHit[height, 2]; // left, right
            layerMask = 1 << LayerMask.NameToLayer("Spatial Awareness");
            kdTree = new KDTree(maxLeafNodes);
        }
        #endregion
        #region public
        void Update() {
            UpdateRayEstimators();
            BuildPolyLine();
            DebugDrawRayHits();
        }
        #endregion
        #region private
        void UpdateRayEstimators() {
            pointHits.Clear();
            pointHits.AddRange(UpdateWestRays());
            pointHits.AddRange(UpdateEastRays());
            pointHits.AddRange(UpdateNorthRays());
            pointHits.AddRange(UpdateSouthRays());
        }

        // [start, end]
        List<Vector3> UpdateRayCast(RaycastHit[,] raycastHitsArr, Vector3 dir0, Vector3 dir1, int start, int end) {
            int numMissess = 0;
            int cur = start;
            bool isVertical = raycastHitsArr == verticalRaycastHits;
            List<Vector3> pointHits = new List<Vector3>();
            while (true) {
                Vector3 location = CalcLocForRayOrigin(cur, isVertical);
                Physics.Raycast(location, dir0, out raycastHitsArr[cur, 0], maxRayDist, layerMask);
                Physics.Raycast(location, dir1, out raycastHitsArr[cur, 1], maxRayDist, layerMask);
                if (!raycastHitsArr[cur, 0].collider && !raycastHitsArr[cur, 1].collider) {
                    numMissess++;
                }
                else {
                    numMissess = 0;
                    pointHits.Add(raycastHitsArr[cur, 0].point);
                    pointHits.Add(raycastHitsArr[cur, 1].point);
                }
                if (numMissess > numMissessAllowed) {
                    break;
                }
                if (start < end) {
                    ++cur;
                    if (cur > end) {
                        break;
                    }
                }
                else {
                    --cur;
                    if (cur < end) {
                        break;
                    }
                }

            }
            return pointHits;
        }
        List<Vector3> UpdateWestRays() {
            return UpdateRayCast(verticalRaycastHits, KuriT.Forward, -KuriT.Forward, centerCords.x, 0);
        }
        List<Vector3> UpdateEastRays() {
            return UpdateRayCast(verticalRaycastHits, KuriT.Forward, -KuriT.Forward, centerCords.x, width - 1);
        }
        List<Vector3> UpdateNorthRays() {
            return UpdateRayCast(horizontalRaycastHits, KuriT.Left, -KuriT.Left, centerCords.y, 0);
        }
        List<Vector3> UpdateSouthRays() {
            return UpdateRayCast(horizontalRaycastHits, KuriT.Left, -KuriT.Left, centerCords.y, height - 1);
        }
        Vector3 CalcLocForRayOrigin(int i, bool isVertical) {
            if (isVertical) {
                return KuriT.Position + (KuriT.Left * ((i - centerCords.x) * cellSize));
            }
            else {
                return KuriT.Position + (KuriT.Forward * ((i - centerCords.x) * cellSize));
            }
        }


        void DebugDrawRayHits() {
            // draw vertical rays
            for (int i = 0; i < width; i++) {
                Vector3 origin = CalcLocForRayOrigin(i, true);
                if (verticalRaycastHits[i, 0].collider) {
                    Debug.DrawLine(origin, verticalRaycastHits[i, 0].point, Color.black);
                }
                else {
                    Debug.DrawRay(origin, KuriT.Forward * maxRayDist, Color.red);
                }
                if (verticalRaycastHits[i, 1].collider) {
                    Debug.DrawLine(origin, verticalRaycastHits[i, 1].point, Color.green);
                }
                else {
                    Debug.DrawRay(origin, -KuriT.Forward * maxRayDist, Color.red);
                }
            }
            // draw horizontal rays
            for (int i = 0; i < height; i++) {
                Vector3 origin = CalcLocForRayOrigin(i, false);
                if (horizontalRaycastHits[i, 0].collider) {
                    Debug.DrawLine(origin, horizontalRaycastHits[i, 0].point, Color.blue);
                }
                else {
                    Debug.DrawRay(origin, KuriT.Left * maxRayDist, Color.red);
                }
                if (horizontalRaycastHits[i, 1].collider) {
                    Debug.DrawLine(origin, horizontalRaycastHits[i, 1].point, Color.yellow);
                }
                else {
                    Debug.DrawRay(origin, -KuriT.Left * maxRayDist, Color.red);
                }
            }
        }

        void BuildPolyLine() {
            int totalPointHits = pointHits.Count;
            kdTree.Build(pointHits, maxLeafNodes);
            if (kdTree.Points.Length == 0) {
                Debug.Log("No points in kdTree");
                return;
            }

            // take an initial point from the beginning of the kd tree points
            Vector3 curPoint = kdTree.Points[0];
            HashSet<Vector3> visitedPoints = new HashSet<Vector3>();
            List<Vector3> polyLine = new List<Vector3>();
            polyLine.Add(curPoint);
            visitedPoints.Add(curPoint);


            bool runTwice = false;
            while (polyLine.Count < totalPointHits) {
                List<int> resultIndices = new List<int>();
                List<float> resultDistances = new List<float>();
                query.KNearest(kdTree, curPoint, numNearestToLookAt, resultIndices, resultDistances);

                // sort the indices by distance
                // resultIndices = resultIndices.OrderBy(i => resultDistances[i]).ToList(); // this breaks for some reason
                resultIndices = KeyValSort(resultDistances, resultIndices);

                bool newNearestFound = false;
                foreach (int index in resultIndices) {
                    if (!visitedPoints.Contains(kdTree.Points[index])) {
                        curPoint = kdTree.Points[index];
                        polyLine.Add(curPoint);
                        visitedPoints.Add(curPoint);
                        newNearestFound = true;
                        runTwice = false;
                        break;
                    }
                }

                if (!newNearestFound && polyLine.Count < totalPointHits) {
                    if (runTwice) {
                        ++numNearestToLookAt; // keep adding to this until we find a new nearest point
                    }
                    else {
                        // remove all points visisted from pointHits and rebuild the kdTree
                        List<Vector3> newPointHits = new List<Vector3>();
                        foreach (Vector3 point in pointHits) {
                            if (!visitedPoints.Contains(point)) {
                                newPointHits.Add(point);
                            }
                        }
                        pointHits = newPointHits;
                        kdTree.Build(pointHits, maxLeafNodes);
                        curPoint = kdTree.Points[0];
                        runTwice = true;
                    }
                }
            }
            Debug.Log("Polyline length: " + polyLine.Count);

        }

        List<int> KeyValSort(List<float> keys, List<int> values) {
            List<int> result = new List<int>();
            for (int i = 0; i < keys.Count; i++) {
                int index = keys.IndexOf(keys.Min());
                result.Add(values[index]);
                keys.RemoveAt(index);
                values.RemoveAt(index);
            }
            return result;
        }

        void PrintList<T>(List<T> list) {
            string str = "";
            foreach (T v in list) {
                str += v.ToString() + " ";
            }
            Debug.Log(str);
        }
        #endregion

    }
}
