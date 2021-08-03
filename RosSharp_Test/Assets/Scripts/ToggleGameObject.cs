
using UnityEngine;

public class ToggleGameObject : MonoBehaviour {
    public void Toggle() {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
