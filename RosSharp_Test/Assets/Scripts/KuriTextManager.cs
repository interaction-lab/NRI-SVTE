using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace NRISVTE
{
    public class KuriTextManager : Singleton<KuriTextManager> 
    {
        public GameObject kuriText;
        private int currentTaskLoc;
        public Transform spawnPoint;

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
            //transform.position = spawnPoint.position + Vector3.up;
        }

        private void OnEnable()
        {
            //transform.position = spawnPoint.position + Vector3.up;
        }

        public void CycleToNextPrompt()
        {
            if (currentTaskLoc + 1 < tasks.Length)
                currentTaskLoc++;
            else
                currentTaskLoc = 0;
            PrintTask(currentTaskLoc);
        }

        public void CycleToNextPrompt(int choice)
        {
            if (currentTaskLoc + 1 < tasks.Length)
                currentTaskLoc++;
            else
                currentTaskLoc = 0;
            PrintPrompt(choice);
        }

        public void PrintPrompt(int choice)
        {
            if (choice == 0)
                PrintTask(currentTaskLoc);
            else if (choice == 1)
                PrintCong(currentTaskLoc);
            else if (choice == 2)
                PrintEnc(currentTaskLoc);
        }

        private void PrintTask(int loc)
        {
            kuriText.GetComponent<TextMeshPro>().text = tasks[loc];
        }

        private void PrintEnc(int loc)
        {
            kuriText.GetComponent<TextMeshPro>().text = encouragement[loc];
        }

        private void PrintCong(int loc)
        {
            kuriText.GetComponent<TextMeshPro>().text = congratulation[loc];
        }


    }
}
