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
        #endregion
        #region unity
        void Start() {
            DebugTextM = DebugTextManager.instance;
            ws = new WebSocket("ws://" + host + ":" + port + endPointPath);
            ws.OnOpen += (sender, e) => {
                Debug.Log("Connected");
            };
            ws.OnMessage += (sender, e) => {
                try {
					UnityMainThread.wkr.AddJob(() => {
						DebugTextM.SetDebugText(e.Data);
					});
                }
                catch (System.Exception ex) {
                    Debug.Log(ex.Message);
                }
            };
            ws.OnError += (sender, e) => {
                Debug.Log("Error: " + e.Message);
            };
            ws.OnClose += (sender, e) => {
                Debug.Log("Closed with code: " + e.Code);
            };
            ws.Connect();
        }
        #endregion

        #region public
        public void SendToServer(string message) {
            ws.Send(message);
        }

        #endregion

        #region private
        #endregion
    }
}
