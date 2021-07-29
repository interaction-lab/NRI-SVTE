using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;

namespace RosSharp_Test
{
    public class GameManager : MonoBehaviour
    {
        public AudioSource audioSource;


        private void Start()
        {
            VisualizationManager.instance.toggleSmallController(true);
            VisualizationManager.instance.togglePhotoButton(false);
            VisualizationManager.instance.toggleMediumController(false);
            VisualizationManager.instance.toggleCapTouch(false);
            VisualizationManager.instance.ToggleKuriColorVizMesh(false);
            VisualizationManager.instance.toggleUserFace(false);
            VisualizationManager.instance.toggleKuriText(true);
            VisualizationManager.instance.toggleFreePlayText(false);
            VisualizationManager.instance.toggleControllerText(true);
            VisualizationManager.instance.toggleCameraText(false);
            VisualizationManager.instance.toggleExtendText(false);
            //audioSource = GetComponent<AudioSource>();
        }

        int numPhotosTaken = 0;
        public void TookPhoto()
        {
            ++numPhotosTaken;
            if (numPhotosTaken == 3)
            {
                VisualizationManager.instance.toggleMediumController(true);
                VisualizationManager.instance.toggleCameraText(false);
                VisualizationManager.instance.toggleExtendText(true);
                audioSource.Play();
            }
        }

        int numMovementsPressed = 0;
        public void MovementPressed()
        {
            ++numMovementsPressed;
            if (numMovementsPressed == 2)
            {
                VisualizationManager.instance.togglePhotoButton(true);
                VisualizationManager.instance.toggleControllerText(false);
                VisualizationManager.instance.toggleCameraText(true);
                audioSource.Play();
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
            if (numAnimations >= 1 && numHeadPan >= 1 && !lastToggle)
            {
                VisualizationManager.instance.toggleUserFace(true);
                VisualizationManager.instance.toggleCapTouch(true);
                VisualizationManager.instance.toggleKuriText(true);
                lastToggle = true;
                audioSource.Play();
            }
        }

        int numHeadPan = 0;
        public void HeadMoved()
        {
            ++numHeadPan;
            if (numAnimations >= 1 && numHeadPan >= 1 && !lastToggle)
            {
                VisualizationManager.instance.toggleUserFace(true);
                VisualizationManager.instance.toggleCapTouch(true);
                VisualizationManager.instance.toggleKuriText(true);
                lastToggle = true;
                audioSource.Play();
            }
        }

        bool extended = false;
        public void ControllerExtended()
        {
            if(!extended)
            {
                extended = true;
                VisualizationManager.instance.toggleExtendText(false);
                VisualizationManager.instance.toggleFreePlayText(true);
                audioSource.Play();
            }
        }

    }
}
