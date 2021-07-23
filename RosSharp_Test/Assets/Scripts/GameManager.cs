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
            VisualizationManager.instance.togglePhotoButton(false);
            VisualizationManager.instance.toggleMediumController(false);
            VisualizationManager.instance.toggleCapTouch(false);
            VisualizationManager.instance.ToggleKuriColorVizMesh(false);
        }

        int numPhotosTaken = 0;
        public void TookPhoto()
        {
            ++numPhotosTaken;
            if (numPhotosTaken == 3)
            {
                VisualizationManager.instance.toggleMediumController(true);
            }
        }

        int numMovementsPressed = 0;
        public void MovementPressed()
        {
            ++numMovementsPressed;
            if (numMovementsPressed == 2)
            {
                VisualizationManager.instance.togglePhotoButton(true);
            }
        }

        int numHeadMovements = 0;
        public void HeadMove()
        {
            ++numHeadMovements;
        }

        int numColorChanges = 0;
        public void ColorChanged()
        {
            ++numColorChanges;
        }


    }
}
