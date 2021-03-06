using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public abstract class TransformManager : MonoBehaviour {
        #region members
        Transform _originT;
        public Transform OriginT {
            get {
                if (_originT == null) {
                    _originT = transform; // using my transform for now
                }
                return _originT;
            }
        }


        public abstract Vector3 Forward { get; }
        public abstract Vector3 Position {
            get; set;
        }
        public abstract Quaternion Rotation { get; set; }

        public virtual Vector3 FlatForward {
            get {
                return new Vector3(Forward.x, GroundYCord, Forward.z);
            }
        }

        public virtual Vector3 GroundPosition {
            get {
                return new Vector3(Position.x, GroundYCord, Position.z);
            }
        }

        public virtual Vector2 TwoDPosition {
            get {
                return new Vector2(Position.x, Position.z);
            }
        }
        public virtual Vector2 TwoDForward {
            get {
                return new Vector2(Forward.x, Forward.z);
            }
        }

        float _groundYCord = 0f;
        public virtual float GroundYCord {
            get {
                return _groundYCord + 0.01f; // just off the ground to deal with issues with floating point
            }
            set {
                _groundYCord = value;
            }
        }

        #endregion

        #region unity
        private void FixedUpdate() {
            // raycast down to find ground
            RaycastHit hit;
            // make raycast only hit Spatial Awareness Layers
            int layerMask = 1 << LayerMask.NameToLayer("Spatial Awareness");

            if (Physics.Raycast(OriginT.position + (Vector3.up * 0.1f), Vector3.down, out hit, 10, layerMask)) {
                GroundYCord = hit.point.y;
            }
            else {
                // raycast up to find ground
                if (Physics.Raycast(OriginT.position - (Vector3.up * 0.1f), Vector3.up, out hit, 10, layerMask)) {
                    GroundYCord = hit.point.y;
                }
            }
        }
        #endregion

        #region public
        #endregion

        #region private
        #endregion
    }
}
