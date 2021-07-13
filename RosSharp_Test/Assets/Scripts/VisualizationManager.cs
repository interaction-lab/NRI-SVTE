using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp_Test
{
    public class VisualizationManager : Singleton<VisualizationManager>
    {
        public MeshRenderer meshFrontLeft;
        public MeshRenderer meshLeft;
        public MeshRenderer meshRearLeft;
        public MeshRenderer meshCenter;
        public MeshRenderer meshFront;
        public MeshRenderer meshRearRight;
        public MeshRenderer meshRight;
        public MeshRenderer meshFrontRight;
        public MeshRenderer kuriTextBackground;
        public MeshRenderer kuriText;
        public MeshRenderer KuriCameraManager;
        public MeshRenderer userFace;

        //public Transform ct;
        //public MeshRenderer[] CapTouch = GetComponentsInChildren<MeshRenderer>(ct);

        void Start()
        {
            //CapTouch.enabled = false;
        }

        void Update()
        {

        }

        public void toggleCapTouch()
        {
            /*
            foreach (Transform child in GetComponentsInChildren<Transform>(ct))
            {
                child.gameObject.SetActive(false);
            }*/
            meshFrontLeft.enabled = !meshFrontLeft.enabled;
            meshLeft.enabled = !meshLeft.enabled;
            meshRearLeft.enabled = !meshRearLeft.enabled;
            meshCenter.enabled = !meshCenter.enabled;
            meshFront.enabled = !meshFront.enabled;
            meshRearRight.enabled = !meshRearRight.enabled;
            meshRight.enabled = !meshRight.enabled;
            meshFrontRight.enabled = !meshFrontRight.enabled;
        }

        public void toggleKuriText()
        {
            kuriText.enabled = !kuriText.enabled;
            kuriTextBackground.enabled = !kuriTextBackground.enabled;
        }

        public void toggleKuriCameraManager()
        {
            KuriCameraManager.enabled = !KuriCameraManager.enabled;
            userFace.enabled = !userFace.enabled;
        }
    }
}
