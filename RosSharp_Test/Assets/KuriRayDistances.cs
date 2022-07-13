using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class KuriRayDistances : MonoBehaviour {
        #region members
        // create List of raycasts
        List<RaycastHit> raycastHits = new List<RaycastHit>();
        Transform _originT;
        Transform OriginT {
            get {
                if (_originT == null) {
                    _originT = transform; // using my transform for now
                }
                return _originT;
            }
        }
        #endregion

        #region unity
        // initialize raycasts to size 8
        void Start() {
            InitRayCastHits();
        }

        void FixedUpdate() {
            UpdateRayCasts();
            DebugDrawRaycasts();
        }

        #endregion

        #region public
        #endregion

        #region private
        void UpdateRayCasts() {
            for (int i = raycastHits.Capacity - 1; i >= 0; i--) {
                UpdateRaycast(i);
            }
        }

        void UpdateRaycast(int i) {
            float angle = 360.0f / raycastHits.Capacity * i;
            Vector3 origin = OriginT.position;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            RaycastHit hit;
            Physics.Raycast(origin, direction, out hit, Mathf.Infinity);
            raycastHits[i] = hit;
        }

        void DebugDrawRaycasts() {
            for (int i = raycastHits.Capacity - 1; i >= 0; i--) {
                Debug.DrawLine(OriginT.position, raycastHits[i].point, Color.red);
            }
        }

        private void InitRayCastHits() {
            for (int i = 8 - 1; i >= 0; i--) {
                raycastHits.Add(new RaycastHit());
            }
        }
        #endregion
    }
}

