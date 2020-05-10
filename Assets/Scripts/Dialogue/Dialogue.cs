using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    private int index;
    public float typingSpeed;
    public List<string> sentences;
    private GameObject DialoguePanel;


    IEnumerator Type(){
        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void Advance(){

        textDisplay.text = "";

        switch(index < sentences.Count)
        {
            case true:
                DialoguePanel.gameObject.SetActive(true);
                StartCoroutine(Type());
                index++;
                break;
            
            default:
                DialoguePanel.gameObject.SetActive(false);
                index = 0;
                sentences.Clear();
                break;
        }
    }

    public void AddSentence(string sentence){
        sentences.Add(sentence);
    }
    
    void Start()
    {
        index = 0;
        DialoguePanel = GameObject.Find("DialogueCanvas");
        DialoguePanel.gameObject.SetActive(false);
    }

    void Update(){

        if(sentences.Count > 0){
            
            if(index == 0) Advance();

            else
            {
                if(textDisplay.text == sentences[index-1] && Input.GetButtonDown("Interact"))
                {
                    Advance();
                }
            }
        }
    }

}
