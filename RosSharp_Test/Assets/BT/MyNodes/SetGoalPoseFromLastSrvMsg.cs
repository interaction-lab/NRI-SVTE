using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace NRISVTE {
    public class SetGoalPoseFromLastSrvMsg : ActionNode {
        ConnectionManager connectionManager;
        ConnectionManager ConnectionManager_ {
            get {
                if (connectionManager == null) {
                    connectionManager = ConnectionManager.instance;
                }
                return connectionManager;
            }
        }
        protected override void OnStart() {
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            Vector3 newGoal;
            List<float> kuriCordList = new List<float>();
            string lastMsg = ConnectionManager_.LatestMsg;
            if (lastMsg != null) {
                kuriCordList = JsonUtility.FromJson<ServerPointResponseJSON>(lastMsg).point;
                newGoal = new Vector3(kuriCordList[0], kuriCordList[1], kuriCordList[2]);
                blackboard.goalPosition = newGoal;
                return State.Success;
            }
            return State.Failure;
        }
    }
}
