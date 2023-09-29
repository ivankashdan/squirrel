using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{

    public List<string> toDo = new List<string>(); //dialogue list

    Coroutine clearTextBackground;

    public void ClearText()
    {
        

        toDo.Clear();


        if (clearTextBackground != null)
        {
            StopCoroutine(clearTextBackground);
        }
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
        toDo.Add(dialogue);
        StartCoroutine(Speak());
        //FindObjectOfType<objectives>().addQuest(dialogue);
    }


    private IEnumerator Speak()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject voiceText = GameObject.FindWithTag("voiceText");
        voiceText.GetComponent<TMP_Text>().text = toDo[0];

        Mouse mouse = FindObjectOfType<Mouse>();
        mouse.clickContinueOn();

    }

  

    public void clickThroughDialogue()
    {
        toDo.RemoveAt(0);

        if (toDo.Count > 0)
        {
            StartCoroutine(Speak());
        }
        else
        {
            GameObject voiceText = GameObject.FindWithTag("voiceText");
            voiceText.GetComponent<TMP_Text>().text = null;

            StartCoroutine(CheckTextFinished());
        }

    }

    private IEnumerator CheckTextFinished()
    {

        yield return new WaitForSeconds(0.1f);

        GameObject voiceText = GameObject.FindWithTag("voiceText");
        if (voiceText.GetComponent<TMP_Text>().text == null) {
            Mouse mouse = FindObjectOfType<Mouse>();
            mouse.clickContinueOff();
        }
        


    }



}
