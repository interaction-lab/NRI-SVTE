using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;

namespace RosSharp_Test
{
    public class GameManager : MonoBehaviour
    {
        private VisualizationManager visManager;
        private float timeCount = 0;
        private bool controllerSOff = true;
        private bool controllerMOff = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        public VisualizationManager VisualizationManager
        {
            get
            {
                if (!visManager)
                {
                    visManager = FindObjectOfType<VisualizationManager>();
                }
                return visManager;
            }
        }

        // Update is called once per frame
        void Update()
        {
            timeCount += Time.deltaTime;
            
            if (timeCount > 10 && controllerSOff)
            {
                VisualizationManager.toggleSmallController();
                controllerSOff = false;
            }

            if(timeCount > 20 && controllerMOff)
            {
                VisualizationManager.toggleMediumController();
                controllerMOff = false;
            }
        }
    }
}
