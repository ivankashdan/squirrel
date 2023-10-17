using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speech : MonoBehaviour
{
    public bool clickContinue = false;
    public bool clickable = false;

    public List<string> toDo = new List<string>(); //dialogue list

    Coroutine clearTextBackground;

    private void Start()
    {
        GameObject voiceText = GameObject.FindWithTag("voiceText"); //clear opening text
        TMP_Text textComp = voiceText.GetComponent<TMP_Text>();
        textComp.text = "";
    }

    public void SayBackground(string dialogue)
    {
        GameObject voiceText = GameObject.FindWithTag("voiceText");
        TMP_Text textComp = voiceText.GetComponent<TMP_Text>();
        textComp.text = dialogue;

        clearTextBackground = StartCoroutine(ClearTextBackground(dialogue, textComp));
    }
    private IEnumerator ClearTextBackground(string dialogue, TMP_Text textComp)
    {
        yield return new WaitForSeconds(3);
        textComp.text = null;
    }

    public void Say(string dialogue)
    {
        if (!clickContinue)
        {
            if (clearTextBackground != null)
            {
                StopCoroutine(clearTextBackground);
            }

            Inventory inv = FindObjectOfType<Inventory>();
            inv.ToggleInvVisible(false);

            clickContinue = true;
        }
        toDo.Add(dialogue);
        StartCoroutine(Speak());
        //FindObjectOfType<objectives>().addQuest(dialogue);
    }

    private IEnumerator Speak()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject voiceText = GameObject.FindWithTag("voiceText");

        if (toDo.Count > 0)
            voiceText.GetComponent<TMP_Text>().text = toDo[0];

        clickable = true;
    }

    public void clickThroughDialogue()
    {
        if (clickable)
        {
            if (toDo.Count > 0)
            {
                toDo.RemoveAt(0);
            }
            if (toDo.Count > 0)
            {
                StartCoroutine(Speak());
            }
            else
            {
                GameObject voiceText = GameObject.FindWithTag("voiceText");
                clickable = false;
                clickContinue = false;
                StartCoroutine(CheckTextFinished());
            }
        }
    
    }

    private IEnumerator CheckTextFinished()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject voiceText = GameObject.FindWithTag("voiceText");
        if (toDo.Count == 0) {
            voiceText.GetComponent<TMP_Text>().text = null;

            Inventory inv = FindObjectOfType<Inventory>();
            inv.ToggleInvVisible(true);
        }
        

    }


  
  

}
