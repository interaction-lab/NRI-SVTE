using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class ServerJSONManager : Singleton<ServerJSONManager> {
        #region members
        ConnectionManager _connectionManager;
        ConnectionManager connectionManager {
            get {
                if (_connectionManager == null) {
                    _connectionManager = gameObject.GetComponent<ConnectionManager>();
                }
                return _connectionManager;
            }
        }
        PlayerTransformManager _playerT;
        PlayerTransformManager PlayerT {
            get {
                if (_playerT == null) {
                    _playerT = Camera.main.GetComponent<PlayerTransformManager>();
                }
                return _playerT;
            }
        }

        KuriTransformManager _kuriT;
        KuriTransformManager KuriT {
            get {
                if (_kuriT == null) {
                    _kuriT = KuriManager.instance.GetComponent<KuriTransformManager>();
                }
                return _kuriT;
            }
        }
        PolyLineJSON polyLineJSONmsg;
        FakeWallRoomPolylineEstimator fakeWallRoomPolylineEstimator;

        FakeWallRoomPolylineEstimator FakeWallRoomPolylineEstimator_ {
            get {
                if (fakeWallRoomPolylineEstimator == null) {
                    fakeWallRoomPolylineEstimator = FindObjectOfType<FakeWallRoomPolylineEstimator>();
                }
                return fakeWallRoomPolylineEstimator;
            }
        }

        GroundObstacleManager _groundObstacleManager;
        GroundObstacleManager groundObstacleManager {
            get {
                if (_groundObstacleManager == null) {
                    _groundObstacleManager = GroundObstacleManager.instance;
                }
                return _groundObstacleManager;
            }
        }
        InteractionManager _interactionManager;
        InteractionManager InteractionManager_ {
            get {
                if (_interactionManager == null) {
                    _interactionManager = InteractionManager.instance;
                }
                return _interactionManager;
            }
        }
        #endregion

        #region unity
        void Start() {
            polyLineJSONmsg = new PolyLineJSON();
        }
        #endregion

        #region public
        public void SendLabeledPoint(int score) {
            polyLineJSONmsg.requestType = "label";
            GeneratePolyline(score);

        }

        public void RequestNextSamplePoint() {
            polyLineJSONmsg.requestType = "sample";
            GeneratePolyline(0);
        }
        #endregion

        #region private
        void GeneratePolyline(int score) {
            polyLineJSONmsg.sampleType = InteractionManager_.CurrentSampleType.ToString();
            polyLineJSONmsg.identifier = string.Join("_", UserIDManager.PlayerId, UserIDManager.DeviceId, Time.time.ToString());
            polyLineJSONmsg.room = FakeWallRoomPolylineEstimator_.GetWallPolyLines();
            polyLineJSONmsg.robot = new Dictionary<string, int>() {
                    {"id", 0}
                };
            polyLineJSONmsg.score = score;
            polyLineJSONmsg.objects = groundObstacleManager.GetObstaclePolyLines();

            Vector2 userPosRelKuri = new Vector2(PlayerT.Position.x - KuriT.Position.x, PlayerT.Position.z - KuriT.Position.z);
            Vector2 kuriForward2D = new Vector2(KuriT.Forward.x, KuriT.Forward.z);
            float angle = Vector2.SignedAngle(userPosRelKuri, kuriForward2D);
            polyLineJSONmsg.humans = new List<Dictionary<string, float>>() {
                    new Dictionary<string, float>() {
                        {"id", 1},
                        {"xPos", userPosRelKuri.x * 100},
                        {"yPos", userPosRelKuri.y * 100},
                        {"orientation", angle}
                    }
                };
            Debug.Log("SendPolyline: " + polyLineJSONmsg.ToString());
            connectionManager.SendToServer(Newtonsoft.Json.JsonConvert.SerializeObject(polyLineJSONmsg));
        }
        #endregion
    }
}
