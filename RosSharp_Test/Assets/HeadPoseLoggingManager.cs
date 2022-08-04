using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class HeadPoseLoggingManager : Singleton<HeadPoseLoggingManager> {
        static string headPoseColName = "HeadPose", headRotColName = "HeadRot";

        PlayerTransformManager _playerT;
        PlayerTransformManager PlayerT {
            get {
                if (_playerT == null) {
                    _playerT = Camera.main.GetComponent<PlayerTransformManager>();
                }
                return _playerT;
            }
        }
        LoggingManager _loggingManager;
        LoggingManager loggingManager {
            get {
                if (_loggingManager == null) {
                    _loggingManager = LoggingManager.instance;
                }
                return _loggingManager;
            }
        }

        private void Start() {
            loggingManager.AddLogColumn(headPoseColName, Vector3.zero.ToString());
            loggingManager.AddLogColumn(headRotColName, Quaternion.identity.ToString());
        }

        private void Update() {
            loggingManager.UpdateLogColumn(headPoseColName, PlayerT.Position.ToString());
            loggingManager.UpdateLogColumn(headRotColName, PlayerT.Rotation.ToString());
        }
    }
}
