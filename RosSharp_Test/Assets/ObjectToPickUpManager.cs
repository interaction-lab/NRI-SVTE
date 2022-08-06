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

        #endregion
        #region unity
        void Start() {
            ResetObjectSets();
            DisableAllObjects();
        }
        #endregion
        #region public
        public void EnableObject(ObjectToPickUp objectToPickUp) {
            objectToPickUp.gameObject.SetActive(true);
            AttachToKuriHead(objectToPickUp);
        }
        public void DisableObject(ObjectToPickUp objectToPickUp) {
            objectToPickUp?.gameObject.SetActive(false);
            objectToPickUp?.transform.parent = transform;
        }

        public void GetNewRandomObject() {
            if (UnUsedObjects.Empty()) {
                ResetObjectSets();
            }
            // get last object in list
            ObjectToPickUp objectToPickUp = UnUsedObjects[UnUsedObjects.Count - 1];
            UnUsedObjects.RemoveAt(UnUsedObjects.Count - 1);
            UsedObjects.Add(objectToPickUp);

            DisableObject(CurrentlyPickedUpObject);
            CurrentlyPickedUpObject = objectToPickUp;
            EnableObject(CurrentlyPickedUpObject);
        }

        private void AttachToKuriHead(ObjectToPickUp objectToPickUp) {
            objectToPickUp.transform.position = KuriT.HeadPosition + KuriT.AboveHeadOffset * Vector3.up;
            objectToPickUp.transform.rotation = KuriT.Rotation;
            objectToPickUp.transform.parent = KuriT.OriginT;
        }
        #endregion

        #region private
        private void ResetObjectSets() {
            UsedObjects.Clear();
            UnUsedObjects.AddRange(GetComponentsInChildren<ObjectToPickUp>(true));
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
