using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;

namespace NRISVTE
{
    public class GameManager : MonoBehaviour
    {
        public Transform KuriText;
        public Transform Kuri;

        private void Start()
        {
            VisualizationManager.instance.toggleSmallController(true);
            VisualizationManager.instance.toggleUserFace(true);
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
                AudioManager.instance.PlaySoundAtObject(KuriText, AudioManager.correctAudioClip);
            }
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
                AudioManager.instance.PlaySoundAtObject(KuriText, AudioManager.correctAudioClip);
            }
        }


        int numColorChanges = 0;
        public void ColorChanged()
        {
            ++numColorChanges;
            if(numColorChanges == 1)
            {
                VisualizationManager.instance.ToggleKuriColorVizMesh(true);
                AudioManager.instance.PlaySoundAtObject(Kuri, AudioManager.popAudioClip);
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
                AudioManager.instance.PlaySoundAtObject(KuriText, AudioManager.correctAudioClip);
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
                AudioManager.instance.PlaySoundAtObject(KuriText, AudioManager.correctAudioClip);
            }
        }

    }
}
