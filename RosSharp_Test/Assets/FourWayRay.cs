using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class FourWayRay : MonoBehaviour {
        #region members
        // west, east, north, south raycasts
        public RaycastHit westRaycastHit;
        public RaycastHit eastRaycastHit;
        public RaycastHit northRaycastHit;
        public RaycastHit southRaycastHit;
        public RaycastHit[] raycastHits = new RaycastHit[4];
        public float[] raycastDistances = new float[4];
        public float[] raycastAngles = new float[4] { 270, 90, 0, 180 };
        TransformManager _transformManager;
		public TransformManager TransformT {
			get {
				if (_transformManager == null) {
					_transformManager = GetComponent<TransformManager>();
				}
				return _transformManager;
			}
		}
        #endregion

        #region unity
        void Start() {
            InitRayCastHits();
        }
        void FixedUpdate() {
            UpdateRayCasts();
#if UNITY_EDITOR
            DebugDrawRaycasts();
#endif
        }
        #endregion

        #region public
        #endregion

        #region private
        void InitRayCastHits() {
            raycastHits[0] = westRaycastHit;
            raycastHits[1] = eastRaycastHit;
            raycastHits[2] = northRaycastHit;
            raycastHits[3] = southRaycastHit;
        }
        void UpdateRayCasts() {
            for (int i = 0; i < raycastHits.Length; i++) {
                // shoot raycast from origin to direction starting west, east, north, south
                float angle = raycastAngles[i];
                Vector3 origin = TransformT.Position;
                Vector3 direction = Quaternion.Euler(0, angle, 0) * TransformT.Forward;
                Physics.Raycast(origin, direction, out raycastHits[i], Mathf.Infinity, 1 << LayerMask.NameToLayer("Spatial Awareness"));
                raycastDistances[i] = raycastHits[i].distance;
            }
        }

        Color[] colors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow };
        void DebugDrawRaycasts() {
            for (int i = 0; i < raycastHits.Length; i++) {
                Debug.DrawRay(TransformT.Position, raycastHits[i].point - TransformT.Position, colors[i]);
            }
        }
        #endregion
    }
}
