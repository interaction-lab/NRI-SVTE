using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class KuriLoggingManager : MonoBehaviour {
        #region members
        public static string robotPoseColName = "robotPose", robotRotColName = "robotRot";
        LoggingManager _loggingManager;
        LoggingManager loggingManager {
            get {
                if (_loggingManager == null) {
                    _loggingManager = LoggingManager.instance;
                }
                return _loggingManager;
            }
        }
        KuriTransformManager _kuriT;
        KuriTransformManager kuriT {
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
            loggingManager.AddLogColumn(robotPoseColName, "");
            loggingManager.AddLogColumn(robotRotColName, "");
        }
        void Update() {
            loggingManager.UpdateLogColumn(robotPoseColName, kuriT.Position.ToString());
            loggingManager.UpdateLogColumn(robotRotColName, kuriT.Rotation.ToString());
        }
        #endregion

    }
}
