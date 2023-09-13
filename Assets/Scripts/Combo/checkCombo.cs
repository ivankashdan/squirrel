using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
//using UnityEditor.Experimental.GraphView;

public class checkCombo : MonoBehaviour
{

    public float timer;
    public bool timeOn;
    public bool testNoTerminal;

    GameObject whirl;
    cDialogue wO;
    pVisible p;
    Inventory inv;
    gamePad gPad;
    Slot slot;

    string comboName;
    string storedCombo = "";

    public float timeLength = 0;
    public float timeLengthD = 1.25f;
    //int timeLength = 70;

    public bool transformTrigger = false;


    List<string> combos = new List<string>(); // is it expensive loading these images?    //necessary now we have an enum?????

    private void Start()
    {
        gPad = FindObjectOfType<gamePad>();
        whirl = FindObjectOfType<Character>().gameObject;
        wO = FindObjectOfType<cDialogue>();
        p = FindObjectOfType<pVisible>();
        inv = FindObjectOfType<Inventory>();
        slot = FindObjectOfType<Slot>();



        Sprite[] images = Resources.LoadAll("Combos", typeof(Sprite)).Cast<Sprite>().ToArray();

        foreach (Sprite s in images)            //NECESSARY????
        {
            combos.Add(s.name);
        }

    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (timeOn)
            {
                timer = timeLength;  //skip transformation wait if mouse button pressed
            }
        }

    }

    private void FixedUpdate()
    {
    

        if (timeOn)
        {

            timer += Time.deltaTime;
            //Debug.Log(timer);

           
        }


        if (gameObject.GetComponent<SpriteRenderer>().sprite == null)
        {
            storedCombo = ""; //for drawHostpot()
            return;
        }

        //comboName = gameObject.GetComponent<SpriteRenderer>().sprite.name;
        //storedCombo = comboName;


        if (gameObject.GetComponent<SpriteRenderer>().sprite.name != storedCombo)
        {
            comboName = gameObject.GetComponent<SpriteRenderer>().sprite.name;


            if (inv.getSpecial(comboName) != "") //this is checking repeatedly! Find a way to just do it once   //getSpecial
            {
                transformTrigger = true;
            }
            storedCombo = comboName;
            drawHotspot(gameObject); 
        }
       
        if (transformTrigger)
        {
            newItem(inv.getSpecial(comboName), comboName);  //getSpecial
        }


    }

    private void newItem(string special, string recipe)
    {
        //if (gPad.rumble)
        //{
        //    Gamepad.current.SetMotorSpeeds(0.25f, 0.50f);
        //}

        timeOn = true;
        inv.checkTime(special);

        //this.gameObject.GetComponent<SpriteRenderer>().sprite = null; //fix to enable refresh of items on special change

        if (whirl.GetComponent<Character>().cSpoken == false)
        {

            if (!p.isBlocking)  //remove cursor while waiting for transformation of already discovered combo
            {
                p.toggleCursor();
            }


            if (timer <= timeLength)    //cuts the function short if the timer isn't ready yet
            {

                return;
            }

            

            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Combos/" + special, typeof(Sprite)) as Sprite;
            if (gameObject.GetComponent<PolygonCollider2D>()) //replace collider for more accurate hotspot
            {

                Destroy(gameObject.GetComponent<PolygonCollider2D>());
                gameObject.AddComponent<PolygonCollider2D>();

                

            }

            if (p.isBlocking)  //restore cursor after transformation is complete of already discovered combo
            {
                p.toggleCursor();
            }



            if (Resources.Load("SFX/" + special))
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("SFX/" + special, typeof(AudioClip)) as AudioClip);
            }
            timeOn = false;
            timer = 0;
            bool flag1 = false;
            bool notTerminal = false;


            if (testNoTerminal == false)  //if terminal combos are allowed, normal behaviour
            {
                foreach (string s in combos)
                {
                    if (s.Contains(special + "_") || s.Contains("_" + special))  //if more combos exist that are possible, return to hand
                    {
                        notTerminal = true;
                        break;
                    }

                }

            }
            else
            {
                notTerminal = true;
            }

            if (notTerminal) //return special to hand on pick-up
            {
                for (int i = 0; i < inv.transform.childCount; ++i)
                {
                    string str1 = recipe;
                    char[] chArray = new char[1] { char.Parse("_") };  //split up recipe into parts 

                    foreach (string str2 in str1.Split(chArray))  //for each parts
                    {
                        if (inv.transform.GetChild(i).GetComponent<Slot>().taken != null
                            && inv.transform.GetChild(i).GetComponent<Slot>().taken.name == str2)   //if a slot's taken is empty and its name is the part
                        {
                            if (!flag1)
                            {
                                inv.transform.GetChild(i).GetComponent<Slot>().taken = Resources.Load("Combos/" + special, typeof(Sprite)) as Sprite;
                                //replace taken with the special

                                //checkSpool(special)
                                flag1 = true;    //ticks the item off as added
                            }
                            else
                                inv.transform.GetChild(i).GetComponent<Slot>().taken = null; //otherwise, make the other taken null
                        }
                    }
                }
            }

                                    ///WIP STILL DOESN'T WORK ATM!!!!
            //else //if terminal combo has been reached, iterate through inventory and unspool until there is nothing left to unspool
            //{

            //    Debug.Log("terminal combo reached");
            //    bool unspooled = true;
                
            //    while (unspooled)
            //    {
            //        unspooled = false;
            //        for (int i = 0; i < inv.transform.childCount; ++i)
            //        {
            //            if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite)
            //            {
            //                if (inv.getRecipe(inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.name) != "")
            //                {
            //                    inv.unSpool(inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.name);
            //                    unspooled = true;
            //                }
            //            }

                        
            //            if (inv.getRecipe(inv.transform.GetChild(i).GetComponent<Slot>().taken.name) != "")
            //            {
            //                inv.unSpool(inv.transform.GetChild(i).GetComponent<Slot>().taken.name);
            //                unspooled = true;
            //            }
                        

            //        }
            //    }
                
            
            //}



            //if (gPad.rumble)
            //{
            //    Gamepad.current.SetMotorSpeeds(0, 0);
            //}
            wO.elderComment();
            transformTrigger = false;

        }

        
    }

    public void drawHotspot(GameObject obj) //WIP /////////Regnerate hotspot using saved file if exists  //there are 2 of these... need just 1
    {

        bool hsFound = false;
        if (Resources.Load("Combos/Hotspots/" + obj.GetComponent<SpriteRenderer>().sprite.name))
        {
            obj.GetComponent<SpriteRenderer>().sprite = Resources.Load("Hotspots/" + obj.GetComponent<SpriteRenderer>().sprite.name, typeof(Sprite)) as Sprite;
            hsFound = true;
        }
        if (obj.GetComponent<PolygonCollider2D>())
        {
            Destroy(obj.GetComponent<PolygonCollider2D>());
            obj.AddComponent<PolygonCollider2D>();
        }

        if (hsFound)
        {
            obj.GetComponent<SpriteRenderer>().sprite = Resources.Load("Combos/" + obj.GetComponent<SpriteRenderer>().sprite.name, typeof(Sprite)) as Sprite;
        }
    }
}
