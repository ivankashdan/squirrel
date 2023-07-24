using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class objectives : MonoBehaviour
{
    Character whirl;
    wObjects log;
    //List <string> quests = new List <string>();
    Dictionary <string, string> quests = new Dictionary <string, string> ();
    string title = "<b>Ideas</b><br>";

    bool listChanged = false;

    void Start()
    {
        whirl = FindObjectOfType<Character> ();
        log = FindObjectOfType<wObjects>();
        gameObject.GetComponent<TMP_Text>().text = title;

        //gameObject.GetComponent<TMP_Text>().text = "";

    }

    public void addQuest(string newObj, string condition)
    {
        if (newObj != "") 
        {
            quests.Add(newObj, condition);
            listChanged = true;
            //updateQuests();
            return;
        }

    }

    public void checkQuests(string combo)
    {
        foreach (var q in quests)
        {
            if (q.Value == combo)
            {

                quests.Remove(q.Key);
                quests.Add("<s>" + q.Key + "</s>", q.Value);
                listChanged = true;
                //updateQuests();
                return;
            }
        }
    }

    void updateQuests()
    {
        gameObject.GetComponent<TMP_Text>().text = title;

        foreach (var q in quests)
        {
            gameObject.GetComponent<TMP_Text>().text += q.Key + "<br>";
            
        }

    }

    private void FixedUpdate()
    {
        if (!whirl.cSpoken && listChanged)
        {
            updateQuests();
            listChanged = false;
        }
    }







}
