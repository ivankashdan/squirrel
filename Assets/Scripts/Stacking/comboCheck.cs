using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class comboCheck : MonoBehaviour
{
    private string comboName;
    public float timer;
    public bool timeOn;
    private Controls control;
    public bool testNoTerminal;

    GameObject whirl;
    Pointer p;
    Inventory inv;
    objectTalk objTalk;

    float timeLength = 0;
    public float timeLengthD = 1.25f;
    //int timeLength = 70;

    List<string> combos = new List<string>(); // is it expensive loading these images?

    bool cursorSwitch = true;


    private void Start()
    {
        control = FindObjectOfType<Controls>().GetComponent<Controls>();
        whirl = FindObjectOfType<Character>().gameObject;
        p = FindObjectOfType<Pointer>();
        inv = FindObjectOfType<Inventory>();
        objTalk = FindObjectOfType<objectTalk>();

        Sprite[] images = Resources.LoadAll("Combos", typeof(Sprite)).Cast<Sprite>().ToArray();

        foreach (Sprite s in images)
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

        if (timeOn && !whirl.GetComponent<Character>().cSpoken)
        {
            if (cursorSwitch)
            {
                //p.toggleCursor();
                cursorSwitch = false;
            }
         

        }
        else
        {
            if (cursorSwitch == false)
            {
                //p.toggleCursor();
                cursorSwitch = true;
            }
        }

        if (!(gameObject.GetComponent<SpriteRenderer>().sprite != null))
        {
            return;
        }

        comboName = gameObject.GetComponent<SpriteRenderer>().sprite.name;
        string str = "";
        string[] strArray = comboName.Split(char.Parse("_"));
        if (strArray.Length == 2)
        {
            str = strArray[1] + "_" + strArray[0];
        }

        if (control.getSpecial(comboName) != "")
        {
            newItem(control.getSpecial(comboName), comboName);
        }
        else
        {
            if (!(control.getSpecial(str) != ""))
                return;
            newItem(control.getSpecial(str), str);
        }
    }


    void checkTime(string i)
    {

        switch (i)
        {
            case "fire":
            case "tent":
            case "lightning":
            case "rocket":
            case "squirrel":
            case "tree":
            
                timeLength = 0;
                break;
            default:
                timeLength = timeLengthD;
                break;
        }

    }

    //void checkSpool(string i)
    //{
    //    if (i == "bird_fire")
    //    {
    //        unSpool(i);
    //    }

    //}

    //void unSpool(string i)
    //{

    //}


    private void newItem(string special, string recipe)
    {
        if (control.rumble)
        {
            Gamepad.current.SetMotorSpeeds(0.25f, 0.50f);
        }


        timeOn = true;
        checkTime(special);

        //this.gameObject.GetComponent<SpriteRenderer>().sprite = null; //fix to enable refresh of items on special change

        if (whirl.GetComponent<Character>().cSpoken == false)
        {

            if (!p.isBlocking)  //remove cursor while waiting for transformation of already discovered combo
            {
                p.toggleCursor();
            }


            if (timer <= timeLength)
            {


                


                return;
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Combos/" + special, typeof(Sprite)) as Sprite;
            if (gameObject.GetComponent<PolygonCollider2D>()) //replace collider for more accurate hotspot
            {

                //Destroy(gameObject.GetComponent<PolygonCollider2D>());
                //gameObject.AddComponent<BoxCollider2D>();

                Destroy(gameObject.GetComponent<PolygonCollider2D>());
                gameObject.AddComponent<PolygonCollider2D>();
               // gameObject.AddComponent<BoxCollider2D>();
                //gameObject.GetComponent<BoxCollider2D>().size = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x / 5,
                    //gameObject.GetComponent<BoxCollider2D>().size.y / 5);

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
            bool flag2 = false;


            if (testNoTerminal == false)  //if terminal combos are allowed
            {
                foreach (string s in combos)
                {
                    Debug.Log(s);

                    if (s.Contains(special + "_") || s.Contains("_" + special))
                    {
                        flag2 = true;
                        break;
                    }

                }

            }
            else
            {
                flag2 = true;
            }

            if (flag2)
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

            if (control.rumble)
            {
                Gamepad.current.SetMotorSpeeds(0, 0);
            }
            objTalk.elderComment();
        }
        

    }
}
