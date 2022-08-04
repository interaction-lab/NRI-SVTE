using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class ServerJSONManager : MonoBehaviour {
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
        RoomPolylineEstimator _roomPolylineEstimator;
        public RoomPolylineEstimator roomPolylineEstimator {
            get {
                if (_roomPolylineEstimator == null) {
                    _roomPolylineEstimator = GetComponent<RoomPolylineEstimator>();
                }
                return _roomPolylineEstimator;
            }
        }
        #endregion

        #region unity
        void Start() {
            polyLineJSONmsg = new PolyLineJSON();
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SendToServer(-1);
            }
        }
        #endregion

        #region public
        public void SendToServer(int score) {
            polyLineJSONmsg.identifier = string.Join("_", UserIDManager.PlayerId, UserIDManager.DeviceId, Time.time.ToString());
            polyLineJSONmsg.room = roomPolylineEstimator.PublicPolyLineList;
            polyLineJSONmsg.robot = new Dictionary<string, int>() {
                    {"id", 0}
                };
            polyLineJSONmsg.score = score;

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
            connectionManager.SendToServer(Newtonsoft.Json.JsonConvert.SerializeObject(polyLineJSONmsg));
        }
        #endregion
    }
}
