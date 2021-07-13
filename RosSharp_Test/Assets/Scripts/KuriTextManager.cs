using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class KuriTextManager : MonoBehaviour
{
    public GameObject kuriText;
    private int currentTaskLoc;

    private string[] congratulation = {
                    "Well done!",
                    "Good job!",
                    "Great!",
                    "Excellent!",
                    "Very good!"
                };
    private string[] encouragement = {
                    "You can do this!",
                    "Go ahead!",
                    "Don't give up!",
                    "Keey trying!"
                };

    private string[] tasks = {
                    "Use the controller on the top left to move me around!",
                    "Try turning on my camera display!",
                    "Look at my face detection display, this is how I see you!",
                    "Give me a pet on the head!",
                    "Try pushing in my bumper",
                    "Pick me up!",
                };


    void Start()
    {
        currentTaskLoc = 0;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (currentTaskLoc + 1 < tasks.Length)
                currentTaskLoc++;
            else
                currentTaskLoc = 0;
            PrintTask(currentTaskLoc);
        }
    }

    public void PrintTask(int loc)
    {
        kuriText.GetComponent<TextMeshPro>().text = tasks[loc];
    }

    public void PrintEnc(int loc)
    {
        kuriText.GetComponent<TextMeshPro>().text = encouragement[loc];
    }

    public void PrintCong(int loc)
    {
        kuriText.GetComponent<TextMeshPro>().text = congratulation[loc];
    }


}
