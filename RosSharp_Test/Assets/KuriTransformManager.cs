using UnityEngine;

namespace NRISVTE {
    public class KuriTransformManager : TransformManager {
        #region members
        public override Vector3 Forward {
            get {
                return OriginT.forward;
            }
        }

        public override Vector3 Position {
            get {
                return OriginT.position;
            }
            set {
                OriginT.position = value;
            }
        }
        public override Quaternion Rotation {
            get {
                return OriginT.rotation;
            }
            set {
                OriginT.rotation = value;
            }
        }
        KuriHeadPositionManager _kuriHeadPositionManager;
        KuriHeadPositionManager KuriHeadManager {
            get {
                if (_kuriHeadPositionManager == null) {
                    _kuriHeadPositionManager = KuriHeadPositionManager.instance;
                }
                return _kuriHeadPositionManager;
            }
        }
        public Vector3 HeadPosition {
            get {
                return KuriHeadManager.HeadPosition;
            }
        }

        public float AboveHeadOffset {
            get {
                return KuriHeadManager.AboveHeadOffset;
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
