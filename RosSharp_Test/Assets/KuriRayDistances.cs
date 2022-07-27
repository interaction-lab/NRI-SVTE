using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class KuriRayDistances : MonoBehaviour {
        #region members
        // create List of raycasts
        List<RaycastHit> raycastHits = new List<RaycastHit>();
        Transform _originT;
        Transform OriginT {
            get {
                if (_originT == null) {
                    _originT = transform; // using my transform for now
                }
                return _originT;
            }
        }
        // create child game object that holds a set of line render children
        GameObject lineRenderContainer;
        List<LineRenderer> lineRenderers = new List<LineRenderer>();

        public List<float> RayDistances {
            get {
                List<float> distances = new List<float>();
                foreach (RaycastHit hit in raycastHits) {
                    distances.Add(hit.distance);
                }
                return distances;
            }
        }

        Transform _personT;
        Transform PersonT {
            get {
                if (_personT == null) {
                    _personT = Camera.main.transform;
                }
                return _personT;
            }
        }

        public float DistanceToPerson{
            get {
                return Vector3.Distance(OriginT.position, PersonT.position);
            }
        }
        #endregion

        #region unity
        // initialize raycasts to size 8
        void Start() {
            InitRayCastHits();
            InitLineRenders();
        }

        void FixedUpdate() {
            UpdateRayCasts();
            DebugDrawRaycasts();
        }

        #endregion

        #region public
        #endregion

        #region private
        void UpdateRayCasts() {
            for (int i = raycastHits.Capacity - 1; i >= 0; i--) {
                UpdateRaycast(i);
            }
        }

        void UpdateRaycast(int i) {
            float angle = 360.0f / raycastHits.Capacity * i;
            Vector3 origin = OriginT.position;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            RaycastHit hit;
            Physics.Raycast(origin, direction, out hit, Mathf.Infinity);
            raycastHits[i] = hit;
        }

        void DebugDrawRaycasts() {
            for (int i = raycastHits.Capacity - 1; i >= 0; i--) {
                Debug.DrawLine(OriginT.position, raycastHits[i].point, Color.red);
                // update line renderer
                lineRenderers[i].enabled = true;
                lineRenderers[i].SetPosition(0, OriginT.position);
                lineRenderers[i].SetPosition(1, raycastHits[i].point);
            }
        }

        private void InitRayCastHits() {
            for (int i = 8 - 1; i >= 0; i--) {
                raycastHits.Add(new RaycastHit());
            }
        }
        private void InitLineRenders() {
            InitLRContainer();

            // create line renderers
            // for each raycast hit, create a line renderer game object and add it to the line renderer container
            for (int i = 0; i < raycastHits.Count; i++) {
                Debug.Log(i);
                GameObject lineRenderGO = new GameObject("LineRender" + i);
                lineRenderGO.transform.parent = lineRenderContainer.transform;
                lineRenderGO.transform.localPosition = Vector3.zero;
                lineRenderGO.transform.localRotation = Quaternion.identity;
                lineRenderGO.transform.localScale = Vector3.one;
                LineRenderer lineRender = lineRenderGO.AddComponent<LineRenderer>();
                // use universal render pipeline lit shader for liner renderer
                Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                mat.color = Color.red;
                lineRender.material = mat;
                lineRender.startWidth = 0.01f;
                lineRender.endWidth = 0.01f;
                lineRender.startColor = Color.red;
                lineRender.endColor = Color.red;
                lineRender.positionCount = 2;
                lineRender.enabled = false;
                lineRenderers.Add(lineRender);
            }
        }

        private void InitLRContainer() {
            // create a container for the line renderers
            lineRenderContainer = new GameObject("LineRenderContainer");
            lineRenderContainer.transform.parent = transform;
            lineRenderContainer.transform.localPosition = Vector3.zero;
            lineRenderContainer.transform.localRotation = Quaternion.identity;
            lineRenderContainer.transform.localScale = Vector3.one;
        }
        #endregion
    }
}

