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

        public Vector3 FlatForward {
            get {
                return new Vector3(Forward.x, 0, Forward.z);
            }
        }

        public Vector3 FlatPosition {
            get {
                return new Vector3(Position.x, 0, Position.z);
            }
        }
        #endregion

        #region unity
        #endregion

        #region public
        #endregion

        #region private
        #endregion
    }
}
