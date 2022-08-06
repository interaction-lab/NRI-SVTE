using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace NRISVTE{
    public class TurnHeadToLookAtUser : ActionNode
    {
        public float speed = 5; // degrees per second
        PlayerTransformManager _playerTransformManager;
        PlayerTransformManager playerTransformManager {
            get {
                if (_playerTransformManager == null) {
                    _playerTransformManager = Camera.main.GetComponent<PlayerTransformManager>();
                }
                return _playerTransformManager;
            }
        }
        KuriHeadPositionManager _kuriHeadPositionManager;
        KuriHeadPositionManager kuriHeadPositionManager {
            get {
                if (_kuriHeadPositionManager == null) {
                    _kuriHeadPositionManager = KuriHeadPositionManager.instance;
                }
                return _kuriHeadPositionManager;
            }
        }
        protected override void OnStart() {
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            // get desired direction to look at user
            Vector3 desiredDirection = playerTransformManager.Position - kuriHeadPositionManager.HeadPosition;
            // get current direction to look at user
            Vector3 currentDirection = kuriHeadPositionManager.HeadPan.forward;
            // get angle between current and desired direction
            float angle = Vector3.SignedAngle(currentDirection, desiredDirection);
            // get angle between current and desired direction in degrees
            float angleInDegrees = angle * Mathf.Rad2Deg * speed * Time.deltaTime;


            kuriHeadPositionManager.HeadTilt.Rotate(0, 0, angleInDegrees);
            return State.Success;
        }
    }
}
