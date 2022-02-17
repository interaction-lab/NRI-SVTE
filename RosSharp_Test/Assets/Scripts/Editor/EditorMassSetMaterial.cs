using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NRISVTE {
    public class EditorMassSetMaterial : MonoBehaviour {
        [MenuItem("Scripts/Mass Set Materials")]
        static void MassSetMaterials() {
            Material mat = Resources.Load<Material>("Materials/PretendKuriMat");
            ChangeArrayGameObjects(Selection.gameObjects, mat);

        }

        static void ChangeArrayGameObjects(GameObject[] goArr, Material mat) {
            foreach (GameObject go in goArr) {
                ChangeMaterial(go, mat);
            }
        }

        static void ChangeMaterial(GameObject go, Material mat) {
            if (go.transform.childCount > 0) {
                List<GameObject> goArr = new List<GameObject>();
                foreach (Transform t in go.transform) {
                    goArr.Add(t.gameObject);
                }
                ChangeArrayGameObjects(goArr.ToArray(), mat);
            }
            MeshRenderer rend = go.GetComponent<MeshRenderer>();
            if (rend) {
                rend.material = mat;
            }
        }
    }
}
