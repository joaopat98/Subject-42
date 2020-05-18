using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue2 : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    private int index;
    public float typingSpeed;
    public List<string> sentences;
    private GameObject DialoguePanel;
    public AbilityType AbilityToSend;
    private bool ToSend;
    private bool JustChanged;
    Player player;

    IEnumerator Type(){
        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
    }

    private void Advance(){

        textDisplay.text = "";

        DialoguePanel.gameObject.SetActive(false);

        switch(index <= sentences.Count-1)
        {
            case true:
                if(!JustChanged)
                {
                    DialoguePanel.gameObject.SetActive(true);
                    StartCoroutine(Type());
                    index++;
                }
                else
                    JustChanged = false;   
                break;
            
            default:
                DialoguePanel.gameObject.SetActive(false);
                index = 0;
                sentences.Clear();
                if(ToSend)
                {
                    AddAbility();
                    ToSend = false;
                }
                JustChanged = true;
                break;
        }
    }

    public void AddSentence(string sentence){
        
        if(!sentences.Contains(sentence))
        {
            sentences.Add(sentence);
        }
    }

    public void AddAbility(){
        player.AddAbility(AbilityToSend);
    }

    public void ReceiveAbility(AbilityType abilityType){
       AbilityToSend = abilityType;
       ToSend = true;
    }
    
    void Start()
    {
        index = 0;
        DialoguePanel = GameObject.Find("DialogueCanvas");
        DialoguePanel.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ToSend = false;

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
