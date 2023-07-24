using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{

    //delay for voice and dotdot
    float timeUntilClick = 0.1f;   //time until I can click
    float tTimer;

    public bool cSpoken; //has cSpoken?

    public GameObject voice; //selected voice

    ArrayList toDo = new ArrayList(); //dialogue list

    pVisible p;

    public AudioClip[] vocals;


    //DIY co-routine
    float backTimer = 0;
    bool backSwitch = false;
    float backLength = 5;

    //
    string heldDialogue = "";
    float waitTimer = 0;
    float waitLength = 5;


    private void Start()
    {
        voice.GetComponent<TMP_Text>().text = null;

        p = FindObjectOfType<pVisible>();
        // cPointer = FindObjectOfType(typeof(Pointer)) as GameObject;
    }

    private void Update()
    {

        if (heldDialogue!="")
        {
            if (cSpoken == false)
            {
                waitTimer += Time.deltaTime;

                if (waitTimer > waitLength)
                {
                    voice.GetComponent<TMP_Text>().text = heldDialogue;
                    heldDialogue = "";
                    backSwitch = true;

                }
            }
            else
            {
                heldDialogue = "";
            }
        }

        if (backSwitch)   //SayBackground Timer
        {
            backTimer += Time.deltaTime;

            if ((backTimer > backLength) && (cSpoken == false))
            {
                voice.GetComponent<TMP_Text>().text = null;
                backSwitch = false;
            }

        }
        

        if (cSpoken == true)
        {

            if (Time.fixedTime > tTimer + timeUntilClick)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    clickThroughDialogue();

                }


            }


        }
    }


    public void clickThroughDialogue()
    {
        //cSpoken = false;  is it important that it is here instead of below?


        Debug.Log(toDo[0] + " removed");

        toDo.RemoveAt(0);

        //Debug.Log(toDo.Count);

        if (toDo.Count != 0)
        {
            //cSpoken = true;
            Speak();
        }
        else
        {
            voice.GetComponent<TMP_Text>().text = null;
            cSpoken = false;
            p.toggleCursor();
            //Debug.Log("WHENEVER TRIGGERED");

        }

    }

    public void SayBackground(string dialogue)
    {

        Debug.Log(dialogue);
        voice.GetComponent<TMP_Text>().text = dialogue;

        backSwitch = true;
        backTimer = 0;


        //FindObjectOfType<objectives>().addQuest(dialogue);
    }

    public void Say(string dialogue)
    {
        if (p.isBlocking==false)
        {
            p.toggleCursor();
        }

        toDo.Add(dialogue);
        Debug.Log(dialogue + "added");


        Speak();

        //FindObjectOfType<objectives>().addQuest(dialogue);
        

    }


    public void Speak()
    {
       
        voice.GetComponent<TMP_Text>().text = toDo[0].ToString();

        cSpoken = true;
        
        tTimer = Time.fixedTime;


    }


    

    public void Wait(float i)
    {

        heldDialogue = voice.GetComponent<TMP_Text>().text;
        voice.GetComponent<TMP_Text>().text = "";
        waitTimer = 0;
        waitLength = i;
        //waitLength = i * 100;

        backSwitch = false;

    }




}
