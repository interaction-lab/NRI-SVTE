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
        public Transform smallController;
        public Transform mediumController;
        public Transform largeController;
        public GameObject photoButton;

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

        public void toggleSmallController(bool? on = null)
        {
            Toggle(smallController, on);
        }

        public void toggleMediumController()
        {
            mediumController.gameObject.SetActive(!mediumController.gameObject.activeSelf);
        }

        public void toggleLargeController()
        {
            largeController.gameObject.SetActive(!largeController.gameObject.activeSelf);
        }

        void Toggle(Transform t, bool? on)
        {
            Toggle(t.gameObject, on);
        }

        void Toggle(GameObject go, bool? on)
        {
            if (on == null)
            {
                go.gameObject.SetActive(!smallController.gameObject.activeSelf);
            }
            else
            {
                go.gameObject.SetActive((bool)on);
            }
        }

        public void togglePhotoButton(bool? on = null)
        {
            Toggle(photoButton, on);
        }
    }
}
