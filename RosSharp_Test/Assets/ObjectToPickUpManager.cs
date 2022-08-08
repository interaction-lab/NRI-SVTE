using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class ObjectToPickUpManager : Singleton<ObjectToPickUpManager> {
        #region members
        List<ObjectToPickUp> _unUsedObjects;
        List<ObjectToPickUp> UnUsedObjects {
            get {
                if (_unUsedObjects == null) {
                    _unUsedObjects = new List<ObjectToPickUp>();
                }
                return _unUsedObjects;
            }
        }
        HashSet<ObjectToPickUp> _usedObjects;
        HashSet<ObjectToPickUp> UsedObjects {
            get {
                if (_usedObjects == null) {
                    _usedObjects = new HashSet<ObjectToPickUp>();
                }
                return _usedObjects;
            }
        }
        KuriTransformManager _kuriTransformManager;
        KuriTransformManager KuriT {
            get {
                if (_kuriTransformManager == null) {
                    _kuriTransformManager = KuriManager.instance.GetComponent<KuriTransformManager>();
                }
                return _kuriTransformManager;
            }
        }
        public ObjectToPickUp CurrentlyPickedUpObject = null;

        DialogueManager _dialogueManager;
        DialogueManager dialogueManager {
            get {
                if (_dialogueManager == null) {
                    _dialogueManager = DialogueManager.instance;
                }
                return _dialogueManager;
            }
        }
        #endregion
        #region unity
        void Start() {
            ResetObjectSets();
            DisableAllObjects();
        }
        #endregion
        #region public
        public void DisableAndSelectNewObject() {
            if (UnUsedObjects.Empty()) {
                ResetObjectSets();
            }
            // get last object in list
            ObjectToPickUp objectToPickUp = UnUsedObjects[UnUsedObjects.Count - 1];
            UnUsedObjects.RemoveAt(UnUsedObjects.Count - 1);
            UsedObjects.Add(objectToPickUp);

            DisableObject(CurrentlyPickedUpObject);
            CurrentlyPickedUpObject = objectToPickUp;
        }

        public void SpawnCurObject(Vector3 position) {
            EnableObject(CurrentlyPickedUpObject, position);
        }

        public void PickUpObject() {
            AttachToKuriHead(CurrentlyPickedUpObject);
        }
        #endregion

        #region private
        private void EnableObject(ObjectToPickUp objectToPickUp, Vector3 position) {
            objectToPickUp.gameObject.SetActive(true);
            objectToPickUp.transform.position = position;
        }
        private void DisableObject(ObjectToPickUp objectToPickUp) {
            if (objectToPickUp != null) {
                objectToPickUp.gameObject.SetActive(false);
                objectToPickUp.transform.parent = transform;
            }
        }
        private void AttachToKuriHead(ObjectToPickUp objectToPickUp) {
            objectToPickUp.transform.position = KuriT.HeadPosition + KuriT.AboveHeadOffset * Vector3.up;
            objectToPickUp.transform.rotation = KuriT.Rotation;
            objectToPickUp.transform.parent = KuriT.OriginT;
        }
        private void ResetObjectSets() {
            DisableObject(CurrentlyPickedUpObject); // need to reset it back to this transform
            UsedObjects.Clear();
            foreach (Transform t in transform) {
                ObjectToPickUp objectToPickUp = t.GetComponent<ObjectToPickUp>();
                if (objectToPickUp == null) {
                    objectToPickUp = t.gameObject.AddComponent<ObjectToPickUp>();
                }
                UnUsedObjects.Add(objectToPickUp);
            }
            UnUsedObjects.Shuffle();
        }
        private void DisableAllObjects() {
            if (UnUsedObjects.Empty()) {
                ResetObjectSets();
            }
            foreach (ObjectToPickUp obj in UnUsedObjects) {
                obj.gameObject.SetActive(false);
            }
        }
        #endregion
    }
}
