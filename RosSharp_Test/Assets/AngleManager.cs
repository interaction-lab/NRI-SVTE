using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class AngleManager : MonoBehaviour {
        #region members
        public float SinRobotToHumanAngle;
        public float CosRobotToHumanAngle;
        public float SinRobotToHumanOrientation;
        public float CosRobotToHumanOrientation;
        PlayerTransformManager _playerTransformManager;
        PlayerTransformManager playerTransformManager {
            get {
                if (_playerTransformManager == null) {
                    _playerTransformManager = GameObject.FindObjectOfType<PlayerTransformManager>();
                }
                return _playerTransformManager;
            }
        }
        KuriTransformManager _kuriTransformManager;
        KuriTransformManager kuriTransformManager {
            get {
                if (_kuriTransformManager == null) {
                    _kuriTransformManager = GameObject.FindObjectOfType<KuriTransformManager>();
                }
                return _kuriTransformManager;
            }
        }

        Vector3 flatPlayerPosition {
            get {
                return new Vector3(playerTransformManager.Position.x, 0, playerTransformManager.Position.z);
            }
        }
        Vector3 flatKuriPosition {
            get {
                return new Vector3(kuriTransformManager.Position.x, 0, kuriTransformManager.Position.z);
            }
        }
        Vector3 flatPlayerForward {
            get {
                return new Vector3(playerTransformManager.Forward.x, 0, playerTransformManager.Forward.z);
            }
        }
        Vector3 flatKuriForward {
            get {
                return new Vector3(kuriTransformManager.Forward.x, 0, kuriTransformManager.Forward.z);
            }
        }
        #endregion

        #region unity
        #endregion

        #region public
        void FixedUpdate() {
            UpdateAngles();
        }
        #endregion

        #region private
        void UpdateAngles() {
            SinRobotToHumanAngle = Mathf.Sin(Vector3.Angle(flatKuriPosition, flatPlayerPosition));
            CosRobotToHumanAngle = Mathf.Cos(Vector3.Angle(flatKuriPosition, flatPlayerPosition));
            SinRobotToHumanOrientation = Mathf.Sin(Vector3.Angle(flatKuriForward, flatPlayerForward));
            CosRobotToHumanOrientation = Mathf.Cos(Vector3.Angle(flatKuriForward, flatPlayerForward));
        }

        #endregion
    }
}
