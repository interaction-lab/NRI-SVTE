using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class ModelManager : MonoBehaviour {
        #region members
        public float[] modelInputs = new float[14];
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

        #endregion

        #region unity
        void FixedUpdate() {
            UpdateModelInputs();
            SendModelInputs();
        }
        #endregion

        #region public
        #endregion

        #region private
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
            ConnectionM.SendToServer(string.Join(',', modelInputs));
        }
        #endregion
    }
}
