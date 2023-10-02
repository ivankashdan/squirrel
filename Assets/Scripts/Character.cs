using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    public bool clickContinue = false;
    public bool clickable = false;

    public List<string> toDo = new List<string>(); //dialogue list

    Coroutine clearTextBackground;

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
            ToggleInvVisible(false);
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
            ToggleInvVisible(true);
        }
        

    }


    public void ToggleInvVisible(bool visible)
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        foreach (var slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                GameObject item = slot.transform.GetChild(0).gameObject;
                SpriteRenderer sprite = item.GetComponent<SpriteRenderer>();

                if (visible)
                {
                    sprite.sortingLayerName = "UI";
                }
                else
                {
                    sprite.sortingLayerName = "Hidden";
                }
            }
        }
    }

  

}
