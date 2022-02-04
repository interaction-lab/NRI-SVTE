using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RosSharp.RosBridgeClient {
    public class AudioVisualizationUIController : MonoBehaviour
    {
        private GameObject radioLocations;
        private Toggle toggleSpheres;
        private Toggle toggleCones;
        AudioVisualizerCones conesVisualizer;
        AudioVisualizerSpheres spheresVisualizer;
        private bool IsConesOn = true;
        void Start()
        {
            GameObject microphones = GameObject.FindWithTag(ResourcePathManager.microphoneTag);
            conesVisualizer = microphones.GetComponent<AudioVisualizerCones>();
            spheresVisualizer = microphones.GetComponent<AudioVisualizerSpheres>();
            radioLocations = GameObject.FindWithTag(ResourcePathManager.radioLocationsTag);
            toggleSpheres = GameObject.Find("ToggleSpheres").GetComponent<Toggle>();
            toggleCones = GameObject.Find("ToggleCones").GetComponent<Toggle>();
            SetStartingVisualizer();
        }

        //Depending on the value of IsConesOn sets the correct value for the toggles and enables the correct visualizer
        void SetStartingVisualizer()
        {
            conesVisualizer.enabled = IsConesOn;
            spheresVisualizer.enabled = !IsConesOn;
            toggleCones.isOn = IsConesOn;
            toggleSpheres.isOn = !IsConesOn;
           
        }

        public void ToggleConeVisualizer()
        {
            toggleSpheres.isOn = !toggleSpheres.isOn;
            conesVisualizer.enabled = !toggleSpheres.isOn;
            spheresVisualizer.enabled = toggleSpheres.isOn;
        }

        public void ToggleSphereVisualizer()
        {
            toggleCones.isOn = !toggleCones.isOn;
            conesVisualizer.enabled = toggleCones.isOn;
            spheresVisualizer.enabled = !toggleCones.isOn;
        }
        
        public void ToggleAutomatic()
        {
            radioLocations.GetComponent<GenerateRadioLocations>().AutomaticMovement = !radioLocations.GetComponent<GenerateRadioLocations>().AutomaticMovement;
        }

       
    }
}