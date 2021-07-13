using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp_Test
{
    public class VisualizationManager : Singleton<VisualizationManager>
    {
       
        public Transform NextStepButton;
        public Transform CapTouch;
        public Transform KuriCamera;
        public Transform KuriText;

        void Start()
        {
            //CapTouch.enabled = false;
        }

        public void toggleCapTouch()
        {
            CapTouch.gameObject.SetActive(!CapTouch.gameObject.activeSelf);
        }

        public void toggleKuriText()
        {
            KuriText.gameObject.SetActive(!KuriText.gameObject.activeSelf);
        }

        public void toggleKuriCameraManager()
        {

            KuriCamera.gameObject.SetActive(!KuriCamera.gameObject.activeSelf);
        }

        public void toggleNext()
        {
            NextStepButton.gameObject.SetActive(!NextStepButton.gameObject.activeSelf);
        }
    }
}
