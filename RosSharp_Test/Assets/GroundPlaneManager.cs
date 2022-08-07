using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class GroundPlaneManager : Singleton<GroundPlaneManager> {
        #region members
        KuriTransformManager _kuriTransformManager;
        KuriTransformManager kuriTransformManager {
            get {
                if (_kuriTransformManager == null) {
                    _kuriTransformManager = KuriManager.instance.GetComponent<KuriTransformManager>();
                }
                return _kuriTransformManager;
            }
        }

        private float offset = 0.01f;
        #endregion
        #region unity
        void Start() {
            // if not unity editor, turn off the mesh renderer of the ground plane
#if !UNITY_EDITOR
            GetComponent<MeshRenderer>().enabled = false;
#endif
        }
        void Update() {
            MoveTowardGround();
        }
        #endregion
        #region public
        #endregion
        #region private
        void MoveTowardGround() {
            // move my transform y toward kuriTransformManager.GroundYCord
            transform.position = new Vector3(transform.position.x, kuriTransformManager.GroundYCord - offset, transform.position.z);
        }
        #endregion
    }
}