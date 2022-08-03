using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WebSocketSharp;

namespace NRISVTE {
    public class ConnectionManager : MonoBehaviour {
        #region members
        public string host = "localhost";
        public int port = 4649;
        public string endPointPath = "/Echo";
        WebSocket ws;
        DebugTextManager DebugTextM;
        int numErrors = 0;

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
            DebugTextM = DebugTextManager.instance;
            ws = new WebSocket("ws://" + host + ":" + port + endPointPath);
            ws.OnOpen += (sender, e) => {
                Debug.Log("Connected");
            };
            ws.OnMessage += (sender, e) => {
                try {
                    UnityMainThread.wkr.AddJob(() => {
                        DebugTextM.SetDebugText("Received: " + e.Data);
                    });
                }
                catch (System.Exception ex) {
                    Debug.Log(ex.Message);
                }
            };
            ws.OnError += (sender, e) => {
                //Debug.Log("Error: " + e.Message);
                numErrors++;
            };
            ws.OnClose += (sender, e) => {
                Debug.Log("Closed with code: " + e.Code);
            };
            ws.Connect();
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                polyLineJSONmsg.identifier = string.Join("_", UserIDManager.PlayerId, UserIDManager.DeviceId, Time.time.ToString());
                polyLineJSONmsg.room = roomPolylineEstimator.PublicPolyLineList;
                polyLineJSONmsg.robot = new Dictionary<string, int>() {
                    {"id", 0}
                };
                polyLineJSONmsg.score = -1;

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
                ws.Send(Newtonsoft.Json.JsonConvert.SerializeObject(polyLineJSONmsg));
            }
        }
        #endregion

        #region public
        public void SendToServer(string message) {
            //ws.Send(message);
        }

        #endregion

        #region private
        #endregion
    }
}
