using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;

namespace RosSharp_Test
{
    public class GameManager : MonoBehaviour
    {
        // bumper
        // pickup
        // cap touch
        // lidar
        // face detection
     
        private void Start()
        {
            VisualizationManager.instance.toggleSmallController(true);
            VisualizationManager.instance.toggleUserFace(true);
            VisualizationManager.instance.togglePhotoButton(false);
            VisualizationManager.instance.toggleMediumController(false);
            VisualizationManager.instance.toggleCapTouch(false);
            VisualizationManager.instance.ToggleKuriColorVizMesh(false);
            VisualizationManager.instance.toggleKuriText(false);
            VisualizationManager.instance.toggleLidar(false);
            VisualizationManager.instance.toggleLargeController(false);
        }

        int numPhotosTaken = 0;
        public void TookPhoto()
        {
            ++numPhotosTaken;
            if (numPhotosTaken == 2)
            {
                VisualizationManager.instance.toggleLargeController(true);
            }
        }

        int numMovementsPressed = 0;
        public void MovementPressed()
        {
            ++numMovementsPressed;
            if (numMovementsPressed == 2)
            {
                VisualizationManager.instance.toggleMediumController(true);
                //VisualizationManager.instance.togglePhotoButton(true);
            }
        }


        int numColorChanges = 0;
        public void ColorChanged()
        {
            ++numColorChanges;
            if(numColorChanges == 1)
            {
                VisualizationManager.instance.ToggleKuriColorVizMesh(true);
            }
        }

        bool lastToggle = false;
        int numAnimations = 0;
        public void AnimationPlayed()
        {
            ++numAnimations;
            FinalToggleCheck();
        }

        int numHeadPan = 0;
        public void HeadMoved()
        {
            ++numHeadPan;
            FinalToggleCheck();
        }

        void FinalToggleCheck()
        {
            if (numAnimations >= 1 && !lastToggle)
            {
                VisualizationManager.instance.toggleUserFace(true);
                VisualizationManager.instance.toggleCapTouch(true);
                VisualizationManager.instance.toggleKuriText(true);
                lastToggle = true;
            }
        }

    }
}
