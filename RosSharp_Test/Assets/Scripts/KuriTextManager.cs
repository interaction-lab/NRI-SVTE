using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class KuriTextManager : MonoBehaviour
{
    public Text changingText;
    public GameObject changingTextTwo;

    void Start()
    {
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ChangeText();
        }
    }

    public void ChangeText()
    {
        changingText.text = "changed!";
        changingTextTwo.GetComponent<TextMeshPro>().text = "diff change";
    }
}
