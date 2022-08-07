using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        Dictionary<char, Dictionary<bool, List<Vector3>>> pointHits;
        int maxLeafNodes = 4; // higher number = faster building, but slower querying
        int numNearestToLookAt = 7; // higher number = slower querying, but more accurate/uses all hits
        KuriTransformManager _kuriT;
        private List<Vector3> PolyLineList; // [[x, y, angle],...]
        public List<List<float>> PublicPolyLineList; // [[x, y],...]
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
        }
        #endregion
        #region public
        void Update() {
            UpdateRayEstimators();
            // DebugDrawRayHits();
            BuildPolyLine();
            // DebugDrawPolyLineFromKuri();
        }
        #endregion
        #region private
        void UpdateRayEstimators() {
            InitPointHits();
            UpdateWestRays();
            UpdateEastRays();
            UpdateNorthRays();
            UpdateSouthRays();
        }

        void InitPointHits() {
            pointHits = new Dictionary<char, Dictionary<bool, List<Vector3>>>()
            {
                {'S', new Dictionary<bool, List<Vector3>>()
                {
                    { true, new List<Vector3>() },
                    { false, new List<Vector3>() }
                }},
                {'N', new Dictionary<bool, List<Vector3>>()
                {
                    { true, new List<Vector3>() },
                    { false, new List<Vector3>() }
                }},
                {'W', new Dictionary<bool, List<Vector3>>()
                {
                    { true, new List<Vector3>() },
                    { false, new List<Vector3>() }
                }},
                {'E', new Dictionary<bool, List<Vector3>>()
                {
                    { true, new List<Vector3>() },
                    { false, new List<Vector3>() }
                }}
            };
        }

        // [start, end]
        void UpdateRayCast(RaycastHit[,] raycastHitsArr, Vector3 dir0, Vector3 dir1, int start, int end, char dirKey) {
            int numMissess = 0;
            int cur = start;
            bool isVertical = raycastHitsArr == verticalRaycastHits;
            while (true) {
                Vector3 location = CalcLocForRayOrigin(cur, isVertical);
                Physics.Raycast(location, dir0, out raycastHitsArr[cur, 0], maxRayDist, layerMask);
                Physics.Raycast(location, dir1, out raycastHitsArr[cur, 1], maxRayDist, layerMask);
                if (!raycastHitsArr[cur, 0].collider && !raycastHitsArr[cur, 1].collider) {
                    numMissess++;
                }
                else {
                    numMissess = 0;
                    pointHits[dirKey][true].Add(raycastHitsArr[cur, 0].point);
                    pointHits[dirKey][false].Add(raycastHitsArr[cur, 1].point);
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
        }
        void UpdateWestRays() {
            UpdateRayCast(verticalRaycastHits, KuriT.Forward, -KuriT.Forward, centerCords.x, 0, 'W');
        }
        void UpdateEastRays() {
            UpdateRayCast(verticalRaycastHits, KuriT.Forward, -KuriT.Forward, centerCords.x, width - 1, 'E');
        }
        void UpdateNorthRays() {
            UpdateRayCast(horizontalRaycastHits, KuriT.Left, -KuriT.Left, centerCords.y, 0, 'N');
        }
        void UpdateSouthRays() {
            UpdateRayCast(horizontalRaycastHits, KuriT.Left, -KuriT.Left, centerCords.y, height - 1, 'S');
        }
        Vector3 CalcLocForRayOrigin(int i, bool isVertical) {
            if (isVertical) {
                return KuriT.Position + (KuriT.Left * ((i - centerCords.x) * cellSize));
            }
            else {
                return KuriT.Position + (KuriT.Forward * ((i - centerCords.x) * cellSize));
            }
        }

        void BuildPolyLine() {
            // ST, NT, WT, ET, SF, NF, WF, EF
            PolyLineList = new List<Vector3>();
            PolyLineList.AddRange(pointHits['S'][true]);
            PolyLineList.AddRange(pointHits['N'][true]);
            PolyLineList.AddRange(pointHits['W'][true]);
            PolyLineList.AddRange(pointHits['E'][true]);
            PolyLineList.AddRange(pointHits['S'][false]);
            PolyLineList.AddRange(pointHits['N'][false]);
            PolyLineList.AddRange(pointHits['W'][false]);
            PolyLineList.AddRange(pointHits['E'][false]);

            PublicPolyLineList = new List<List<float>>();

            // DebugDrawPolyLine(); // draw prior to transforming into kuri coords

            // convert to relative to Kuri
            Vector3 forwardNormed = KuriT.Forward.normalized;
            Vector2 kuriForward = new Vector2(forwardNormed.x, forwardNormed.z);
            for (int i = 0; i < PolyLineList.Count; i++) {
                Vector2 twodpos = new Vector2(
                    PolyLineList[i].x - KuriT.Position.x,   // x in kuri cords
                    PolyLineList[i].z - KuriT.Position.z);  // y in kuri coords
                float angle = Vector2.SignedAngle(kuriForward, twodpos.normalized);
                PolyLineList[i] = new Vector3(twodpos.x, twodpos.y, angle);
                PublicPolyLineList.Add(new List<float>() { twodpos.x * 100, twodpos.y * 100 });
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

        void DebugDrawPolyLine() {
            for (int i = 0; i < PolyLineList.Count; i++) {
                // draw a line from the current point to the next point
                int nextPointIndex = (i + 1) % PolyLineList.Count;
                Debug.DrawLine(PolyLineList[i], PolyLineList[nextPointIndex], Color.white);
            }
        }

        void DebugDrawPolyLineFromKuri() {
            foreach (Vector3 v in PolyLineList) {
                // convert to world coords from x,y,angle
                Vector3 kuriCords = new Vector3(v.x, 0, v.y);
                Vector3 worldCords = KuriT.Position + kuriCords;
                Debug.DrawLine(KuriT.Position, worldCords, Color.cyan);
            }
        }

        #endregion

    }
}