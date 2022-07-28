using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class RoomAreaEstimationManager : MonoBehaviour {
        #region members
        public bool[,] occupancyGrid;
        static float cellSize = 0.1f; // in meters
        static int gridWidth = 100; // in cells
        static int gridHeight = 100; // in cells

		static float cellArea = cellSize * cellSize;
		[SerializeField]
		public float TotalArea { get; private set; }

        // robot position in the grid
        int robotRow = gridWidth / 2;
        int robotCol = gridHeight / 2;

        KuriTransformManager _kuriT;
        public KuriTransformManager KuriT {
            get {
                if (_kuriT == null) {
                    _kuriT = KuriManager.instance.GetComponent<KuriTransformManager>();
                }
                return _kuriT;
            }
        }

        Vector3 RobotPos {
            get {
                return KuriT.Position;
            }
        }

        #endregion

        #region unity
        void Start() {
            occupancyGrid = new bool[gridWidth, gridHeight];
        }
        #endregion

        #region public
        void Update() {
            UpdateOccupancyGrid();
            PrintOccupancyGrid();
        }

        public void UpdateOccupancyGrid() {
			TotalArea = 0;
            for (int i = 0; i < gridWidth; i++) {
                for (int j = 0; j < gridHeight; j++) {
                    Vector3 location = CalcLocation(i, j);
                    occupancyGrid[i, j] = IsFloor(location);
					if (occupancyGrid[i, j]) {
						TotalArea += cellArea;
					}
                }
            }
        }
        // print the occupancy grid
        public void PrintOccupancyGrid() {
            for (int i = 0; i < gridWidth; i++) {
                for (int j = 0; j < gridHeight; j++) {
                    Vector3 location = CalcLocation(i, j);
					if(occupancyGrid[i, j]) {
						Debug.DrawRay(location, Vector3.down * 0.2f, Color.green);
					} else {
						Debug.DrawRay(location, Vector3.down * 0.2f, Color.red);
					}
                }
            }
        }

        #endregion

        #region private
        // calculate the location of a cell in the grid in real space relative to the robot
        Vector3 CalcLocation(int row, int col) {
            float x = RobotPos.x + (row - robotRow) * cellSize;
            float y = RobotPos.y;
            float z = RobotPos.z + (col - robotCol) * cellSize;
            return new Vector3(x, y, z);
        }


        private bool IsFloor(Vector3 location) {
            // shoot raycast down from the location
            // if it hits something, then it is floor
            RaycastHit hit;
            // make raycast only hit Spatial Awareness Layers
            int layerMask = 1 << LayerMask.NameToLayer("Spatial Awareness");
            if (!Physics.Raycast(location + (Vector3.up * 0.1f), Vector3.down, out hit, 0.2f, layerMask)) {
                return false;
            }
            return true;
        }
        #endregion
    }
}
