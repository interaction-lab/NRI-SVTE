using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class KuriHeadPositionManager : Singleton<KuriHeadPositionManager> {
        #region members
        public Vector3 HeadPosition {
            get {
                return transform.position;
            }
        }
        public Transform HeadPan, HeadTilt, EyeLids;

        public float AboveHeadOffset = 0.5f;
        #endregion
        #region unity
        #endregion
        #region public
        #endregion
        #region private
        #endregion
    }
}
