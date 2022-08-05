using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WebSocketSharp;

namespace NRISVTE {
    public class ConnectionManager : MonoBehaviour {
        #region members
        public string host = "localhost";
        public int port = 2346;
        public string endPointPath = "/Echo";
        WebSocket ws;
        DebugTextManager DebugTextM;
        int numErrors = 0;
        public static string msgSendColName = "msgSend", msgRecvColName = "msgRecv";
        LoggingManager _loggingManager;
        LoggingManager loggingManager {
            get {
                if (_loggingManager == null) {
                    _loggingManager = LoggingManager.instance;
                }
                return _loggingManager;
            }
        }

        string closeCode = "";

        #endregion

        #region unity
        void Start() {
            loggingManager.AddLogColumn(msgSendColName, "");
            loggingManager.AddLogColumn(msgRecvColName, "");
            DebugTextM = DebugTextManager.instance;
            ws = new WebSocket("ws://" + host + ":" + port + endPointPath);
            ws.OnOpen += (sender, e) => {
                Debug.Log("Connected");
            };
            ws.OnMessage += (sender, e) => {
                try {
                    UnityMainThread.wkr.AddJob(() => {
                        loggingManager.UpdateLogColumn(msgRecvColName, e.Data.ToString());
                        DebugTextM.SetDebugText("Received: " + e.Data);
                    });
                }
                catch (System.Exception ex) {
                    Debug.Log(ex.Message);
                }
            };
            ws.OnError += (sender, e) => {
                numErrors++;
            };
            ws.OnClose += (sender, e) => {
                Debug.Log("Closed with code: " + e.Code);
                closeCode = e.Code.ToString();
            };
            ws.Connect();
        }
        #endregion

        #region public
        public void SendToServer(string message) {
            loggingManager.UpdateLogColumn(msgSendColName, message);
            if (ws.ReadyState == WebSocketState.Open) {
                ws.Send(message);
            }
            else {
                Debug.Log("Not connected to server, attempted to send: " + message);
                Debug.Log("Close code: " + closeCode);
            }
        }

        #endregion

        #region private
        #endregion
    }
}
