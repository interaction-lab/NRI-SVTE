using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;

namespace RosSharp_Test
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            VisualizationManager.instance.toggleSmallController(true);
        }

        int numPhotosTaken = 0;
        public void TookPhoto()
        {
            ++numPhotosTaken;
        }

        int numMovementsPressed = 0;
        public void MovementPressed()
        {
            ++numMovementsPressed;
        }
    }
}
