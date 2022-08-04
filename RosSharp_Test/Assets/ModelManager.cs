using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class ModelManager : MonoBehaviour {
        #region members
        public float[] modelInputs = new float[14];
        string[] logNames = new string[] {
            "h_w", "h_e", "h_n", "h_s",
            "r_w", "r_e", "r_n", "r_s",
            "area",
            "hr_dist",
            "h_angle_sin", "h_angle_cos",
            "h_orientation_sin", "h_orientation_cos"
        };
        private float distScaleF = 1000f;
        PlayerTransformManager _playerTransformManager;
        PlayerTransformManager playerTransformManager {
            get {
                if (_playerTransformManager == null) {
                    _playerTransformManager = Camera.main.transform.GetComponent<PlayerTransformManager>();
                }
                return _playerTransformManager;
            }
        }
        TransformManager _kuriTransformManager;
        TransformManager kuriTransformManager {
            get {
                if (_kuriTransformManager == null) {
                    _kuriTransformManager = FindObjectOfType<KuriTransformManager>();
                }
                return _kuriTransformManager;
            }
        }
        // get human FourWayRay
        FourWayRay playerFourWayRay;
        FourWayRay PlayerFourWayRay {
            get {
                if (playerFourWayRay == null) {
                    playerFourWayRay = playerTransformManager.GetComponent<FourWayRay>();
                }
                return playerFourWayRay;
            }
        }
        FourWayRay kuriFourWayRay;
        FourWayRay KuriFourWayRay {
            get {
                if (kuriFourWayRay == null) {
                    kuriFourWayRay = kuriTransformManager.GetComponent<FourWayRay>();
                }
                return kuriFourWayRay;
            }
        }

        DistToPlayer _distToPlayer;
        DistToPlayer DistToPlayer_ {
            get {
                if (_distToPlayer == null) {
                    _distToPlayer = kuriTransformManager.GetComponent<DistToPlayer>();
                }
                return _distToPlayer;
            }
        }

        AngleManager _kuriAngleManager;
        AngleManager KuriAngleManager {
            get {
                if (_kuriAngleManager == null) {
                    _kuriAngleManager = kuriTransformManager.GetComponent<AngleManager>();
                }
                return _kuriAngleManager;
            }
        }

        ConnectionManager _connectionManager;
        ConnectionManager ConnectionM {
            get {
                if (_connectionManager == null) {
                    _connectionManager = FindObjectOfType<ConnectionManager>();
                }
                return _connectionManager;
            }
        }

        RoomAreaEstimationManager _roomAreaEstimationManager;
        RoomAreaEstimationManager RoomAreaEstimationM {
            get {
                if (_roomAreaEstimationManager == null) {
                    _roomAreaEstimationManager = GetComponent<RoomAreaEstimationManager>();
                }
                return _roomAreaEstimationManager;
            }
        }

        LoggingManager _loggingManager;
        LoggingManager LoggingM {
            get {
                if (_loggingManager == null) {
                    _loggingManager = LoggingManager.instance;
                }
                return _loggingManager;
            }
        }

        #endregion

        #region unity
        void Start() {
            Init();    
        }
        void FixedUpdate() {
            UpdateModelInputs();
            SendModelInputs();
            LogModelInputs();
        }
        #endregion

        #region public
        #endregion

        #region private
        void Init() {
            for (int i = 0; i < modelInputs.Length; i++) {
                modelInputs[i] = -1f;
                LoggingM.AddLogColumn(logNames[i], "-1");
            }
        }
        // Note all model inputs should be in mm so we scale them to mm from m
        void UpdateModelInputs() {
            // h_w, h_e, h_n, h_s
            for (int i = 0; i < 4; i++) {
                modelInputs[i] = PlayerFourWayRay.raycastDistances[i] * distScaleF;
            }
            // r_w, r_e, r_n, r_s
            for (int i = 4; i < 8; i++) {
                modelInputs[i] = KuriFourWayRay.raycastDistances[i - 4] * distScaleF;
            }
            // area
            modelInputs[8] = RoomAreaEstimationM.TotalArea;
            // hr_dist
            modelInputs[9] = DistToPlayer_.GetDistanceToPlayer() * distScaleF;
            // h_angle_sin, h_angle_cos
            modelInputs[10] = KuriAngleManager.SinRobotToHumanAngle;
            modelInputs[11] = KuriAngleManager.CosRobotToHumanAngle;
            // h_orientation_sin, h_orientation_cos
            modelInputs[12] = KuriAngleManager.SinRobotToHumanOrientation;
            modelInputs[13] = KuriAngleManager.CosRobotToHumanOrientation;
        }

        private void SendModelInputs() {
            // ConnectionM.SendToServer(string.Join(',', modelInputs));
        }


        private void LogModelInputs() {
            for (int i = 0; i < modelInputs.Length; i++) {
                LoggingM.UpdateLogColumn(logNames[i], modelInputs[i].ToString());
            }
        }

        #endregion
    }
}
