using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE
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
        public ColorVisualizer colorVis;
        public GameObject userFace;
        public Transform lidar;
        public Transform FreePlayText;
        public Transform ControllerText;
        public Transform CameraText;
        public Transform ExtendText;


        public void ToggleKuriColorVizMesh(bool on)
        {
            colorVis.enabled = on;
        }

        public void toggleCapTouch(bool? on = null)
        {
            Toggle(CapTouch, on);
        }

        public void toggleKuriText(bool? on = null)
        {
            Toggle(KuriText, on);
        }

        public void toggleKuriCameraManager(bool? on = null)
        {
            Toggle(KuriCamera, on);
        }

        public void toggleNext(bool? on = null)
        {
            Toggle(NextStepButton, on);
        }

        public void toggleSmallController(bool? on = null)
        {
            Toggle(smallController, on);
        }

        public void toggleMediumController(bool? on = null)
        {
            Toggle(mediumController, on);
        }

        public void toggleLargeController(bool? on = null)
        {
            Toggle(largeController, on);
        }


        public void togglePhotoButton(bool? on = null)
        {
            Toggle(photoButton, on);
        }

        public void toggleUserFace(bool? on = null)
        {
            Toggle(userFace, on);
        }

        public void toggleLidar(bool? on = null)
        {
            Toggle(lidar, on);
        }

        public void TOGGLEBUTTONLIDAR()
        {
            Toggle(lidar, null);
        }

        public void toggleFreePlayText(bool? on = null)
        {
            Toggle(FreePlayText, on);
        }

        public void toggleControllerText(bool? on = null)
        {
            Toggle(ControllerText, on);
        }

        public void toggleCameraText(bool? on = null)
        {
            Toggle(CameraText, on);
        }

        public void toggleExtendText(bool? on = null)
        {
            Toggle(ExtendText, on);
        }

        void Toggle(Transform t, bool? on)
        {
            Toggle(t.gameObject, on);

        }

        void Toggle(GameObject go, bool? on)
        {
            if (on == null)
            {
                go.gameObject.SetActive(!go.activeSelf);
            }
            else
            {
                go.gameObject.SetActive((bool)on);
            }
        }
    }
}
