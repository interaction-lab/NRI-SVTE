using System.Collections;
using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float textSpeed = 0.05f;
    private GameObject loadingIcon;
   
    private void Awake()
    {
        textMesh = GameObject.FindWithTag(ResourcePathManager.bubbleTextTag).GetComponent<TextMeshProUGUI>();
        loadingIcon = GameObject.FindWithTag(ResourcePathManager.loadingIconTag);

    }   

    private void Start()
    {
        ClearText();
    }

    public void Setup(string text, bool IsLoadingActive)
    {

       StartCoroutine(WriteSentence(text));
       loadingIcon.SetActive(IsLoadingActive);
 
    }

    IEnumerator WriteSentence(string text)
    {
        foreach(char c in text.ToCharArray())
        {
            textMesh.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void ClearText()
    {
        textMesh.text = "";
    }

    public string GetText()
    {
        return textMesh.text;
    }

    public void ChangeText(string newText)
    {
        textMesh.text = newText;
    }
}
