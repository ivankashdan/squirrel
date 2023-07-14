using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{

    //Meters
     int dialogueStored;
     float realTime;


    //delay for voice and dotdot
     float timeUntilClick = 0.1f;   //time until I can click
    float tTimer;


    //dotdot
     float dotGap = 0.5f;
    float dotTimer;
    string dotString;
    int addDot = 0;

    //has cSpoken?
    public bool cSpoken;

    //selected voice
    public GameObject voice;
   // GameObject cPointer;

    //dialogue list
    ArrayList toDo = new ArrayList();

    Pointer p;
    actionText actText;
    Controls control;

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

        p = FindObjectOfType<Pointer>();
        actText = FindObjectOfType<actionText>();
        control = FindObjectOfType<Controls>();
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
        

        dialogueStored = toDo.Count;
        realTime = Time.fixedTime;

        if (cSpoken == true)
        {

            if (Time.fixedTime > tTimer + timeUntilClick)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    clickThroughDialogue();

                }


            }

            //dotDot();  //turn back on for dot dot dot animation

        }
    }


    public void dotDot()
    {

        switch (addDot)
        {
            case 0:

                if (Time.fixedTime > dotTimer + 1 * dotGap)
                {

                    voice.GetComponent<TMP_Text>().text = dotString + "\n.";
                    addDot = 1;
                }
                return;
            case 1:

                if (Time.fixedTime > dotTimer + 2 * dotGap)
                {
                    voice.GetComponent<TMP_Text>().text = dotString + "\n..";
                    addDot = 2;

                }
                return;
            case 2:
                if (Time.fixedTime > dotTimer + 3 * dotGap)
                {
                    voice.GetComponent<TMP_Text>().text = dotString + "\n...";
                    addDot = 0;

                    dotTimer = Time.fixedTime;
                }
                return;
            

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


    



    public void Say(string dialogue)
    {


        

        if (p.isBlocking==false)
        {
            p.toggleCursor();
        }

        toDo.Add(dialogue);
        Debug.Log(dialogue + "added");


        Speak();

        // Sing();


        

    }

    void Sing()
    {

        int ran = Random.Range(0, vocals.Length);

        for (int i = 0; i < vocals.Length; i++)
        {

            if (vocals[ran])
            {

                AudioSource aS = GetComponent<AudioSource>();

               //float ranP = Random.Range(0.75f, 1.25f);
               // aS.pitch = ranP;

                aS.clip = vocals[ran];
                aS.Play();

                

                return;

            }

        }




    }

    public void Speak()
    {
       


        voice.GetComponent<TMP_Text>().text = toDo[0].ToString();

        dotString = voice.GetComponent<TMP_Text>().text;

        cSpoken = true;
        
        tTimer = Time.fixedTime;
        dotTimer = Time.fixedTime;

        


    }


    public void SayBackground(string dialogue)
    {
        //toDo.Add(dialogue);
        //StartCoroutine(Speak(toDo));
        SpeakBackground(dialogue);

    }


    public void SpeakBackground(string dialogue)
    {
        Debug.Log(dialogue);
        voice.GetComponent<TMP_Text>().text = dialogue;

        backSwitch = true;
        backTimer = 0;


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


    public IEnumerator Speak(ArrayList dialogue)
    {

      

        for (int i = 0; i < dialogue.Count; i++)
        {


            Debug.Log(dialogue[i]);

            voice.GetComponent<TMP_Text>().text = dialogue[i].ToString();


            yield return new WaitForSeconds(3);


            // dialogue.RemoveAt(i);
            // i -= 1;

            voice.GetComponent<TMP_Text>().text = null;

            
        }


        dialogue.Clear();

        






    }


    /*
        public void toggleCollider()
        {

            if (p.isBlocking)
            {



                object[] c = GetComponents(typeof(Collider2D));
                foreach (Collider2D collider in c)
                {

                    if (collider.enabled)
                    {
                        collider.enabled = false;
                    }

                    else
                    {
                        collider.enabled = true;
                    }


                }

            }
            else
            {

            }

        }
      */





    /*

    public void Say(string dialogue)
    {

        toDo.Add(dialogue);
        StartCoroutine(Speak(toDo));


    }



    public void Interrupt()
    {
     
        toDo.Clear();

    }



    public IEnumerator Speak(ArrayList dialogue) 
    {


        for (int i = 0; i < dialogue.Count; i++)
        {
            Debug.Log(dialogue[i]);

            wText wt = FindObjectOfType<wText>();
            wt.GetComponent<TMP_Text>().text = dialogue[i].ToString();


                yield return new WaitForSeconds(2);


           // dialogue.RemoveAt(i);
           // i -= 1;

            wt.GetComponent<TMP_Text>().text = null;


        }


       dialogue.Clear();

        






    }

    





    */


}
