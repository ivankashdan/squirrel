using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class Controls : MonoBehaviour
{

    public string selectedItem;
    public bool rumble;


    public int slotNumber;
    private bool leftRight;
    private GameObject combo;
    private GameObject whirl;
    private GameObject inv;

    actionText text;
    Pointer p;

    float stickTimer = 0;
    bool stickSwitch = true;

    public bool controller;


    private void Start()
    {
        combo = FindObjectOfType<createCombo>().gameObject;
        whirl = FindObjectOfType<Character>().gameObject;
        inv = FindObjectOfType<Inventory>().gameObject;
        text = FindObjectOfType<actionText>();
        p = FindObjectOfType<Pointer>();

        slotNumber = 0;
       
    }


    int rightItem()
    {
        for (int i = inv.transform.childCount-1; i >= 0; i--)   ///REASSESS ALL THESE NUMBERS FOR DIFFERENT QUANITITIES
        {
            if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite != null)
            {
                return i;
            }
        }
        return 0;
    }

    int leftItem()
    {
        for (int i = 0; i < inv.transform.childCount; i++)
        {
            if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite != null)
            {
                return i;
                
            }
        }
        return 0;
    }

    private void Left()
    {
        if (slotNumber == leftItem())
        {
            slotNumber = rightItem();
            
        }
        else
        {

            --slotNumber;
        }
        
    }

    private void Right()
    {


        if (slotNumber == rightItem())
        {
            slotNumber = leftItem();

        }
        else
        {
            ++slotNumber;
        }
    }

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape))  //Quit function
        {
            Application.Quit();
        }


        Gamepad current = Gamepad.current;
        if (current == null)
        {
            controller = false;
           // Cursor.visible = true;
            if (!whirl.GetComponent<Character>().cSpoken)
            {
                p.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            controller = true;
           // Cursor.visible = false;

            p.GetComponent<SpriteRenderer>().enabled = false;
            if (gameObject.transform.GetChild(slotNumber).GetComponent<SpriteRenderer>().sprite != null)
            {
                selectedItem = gameObject.transform.GetChild(slotNumber).GetComponent<SpriteRenderer>().sprite.name;
            }
            else
            {
                if (slotNumber == 0)
                    leftRight = true;
                if (slotNumber == inv.transform.childCount - 2)
                    leftRight = false;
                if (leftRight)
                    Right();
                else
                    Left();
            }
            if (whirl.GetComponent<Character>().cSpoken)
            {
                if (current.aButton.wasPressedThisFrame || current.yButton.wasPressedThisFrame || current.bButton.wasPressedThisFrame)
                    whirl.GetComponent<Character>().clickThroughDialogue();
                    combo.GetComponent<comboCheck>().timer = combo.GetComponent<comboCheck>().timeLengthD;

            }
            else if (combo.GetComponent<comboCheck>().timeOn)
            {
                if (current.aButton.wasPressedThisFrame || current.yButton.wasPressedThisFrame || current.bButton.wasPressedThisFrame)
                {
                    combo.GetComponent<comboCheck>().timer = combo.GetComponent<comboCheck>().timeLengthD;
                    //combo.GetComponent<comboCheck>().timer = combo.GetComponent<comboCheck>().timeLengthD;
                }
            }
            else
            {
                if (current.dpad.left.wasPressedThisFrame)
                {
                    Left();
                    leftRight = false;
                }
                if (current.dpad.right.wasPressedThisFrame)
                {
                    Right();
                    leftRight = true;
                }
                if (current.aButton.wasPressedThisFrame && gameObject.transform.GetChild(slotNumber).GetComponent<SpriteRenderer>().sprite != null)
                    gameObject.transform.GetChild(slotNumber).GetComponent<Slot>().OnMouseDown();
                    combo.GetComponent<comboCheck>().timer = 0;

                if (current.yButton.wasPressedThisFrame)
                {
                    //bool fullReset = false;
                    //if (combo.GetComponent<SpriteRenderer>().sprite != null)
                    //{
                    //    if (combo.GetComponent<SpriteRenderer>().sprite.name == "bird_fire")
                    //    {
                    //        fullReset = true;
                    //    }

                    //}

                    
                    reset();
                    //if (fullReset)
                    //{
                    //    checkSpool();

                    //}
                }

                if (current.bButton.wasPressedThisFrame)
                {
                    //if (selectedItem)
                    if (combo.GetComponent<SpriteRenderer>().sprite==null)  //quick fix - don't let unspool happen when stacking
                    {
                        if (FindObjectOfType<selector>().GetComponent<SpriteRenderer>().enabled)
                        {
                            unSpool(selectedItem);
                        }
                    }

                 
                }
            }



            stickTimer += Time.deltaTime;
            
            //stickTimer++;
            //Debug.Log(stickTimer);

            if (stickTimer > 0.2f)
            {
                stickSwitch = true;
            }
            if (stickSwitch)
            {
                if (current.leftStick.x.ReadValue() == -1)
                {
                    Left();
                    leftRight = false;
                    stickSwitch = false;
                    stickTimer = 0;
                }
                else if (current.leftStick.x.ReadValue() == 1)
                {
                    Right();
                    leftRight = true;
                    stickSwitch = false;
                    stickTimer = 0;
                }

            }
            //Debug.Log(current.leftStick.x.ReadValue());
        }
    }

    //void checkSpool()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite != null)
    //        {

    //            unSpool(inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.name);
    //        }

    //    }
    //}


    bool resetted = false;
    public void reset()
    {

        combo.GetComponent<comboCheck>().timer = 0;
        combo.GetComponent<createCombo>().stageCounter = 0;
        combo.GetComponent<SpriteRenderer>().sprite = null;
        Destroy(combo.GetComponent<PolygonCollider2D>());

        for (int i = 0; i < inv.transform.childCount; ++i)
        {
            GameObject invItem = inv.transform.GetChild(i).gameObject;
            if (invItem.GetComponent<SpriteRenderer>().sprite == null && invItem.GetComponent<Slot>().taken != null)
            {
                invItem.GetComponent<SpriteRenderer>().sprite = invItem.GetComponent<Slot>().taken;
                invItem.GetComponent<Slot>().taken = null;

                Destroy(invItem.GetComponent<PolygonCollider2D>());   //reset collider for special item in inventory
                invItem.AddComponent<PolygonCollider2D>();


                resetted = true;

                  

            }
        }
    }

    private void FixedUpdate()
    {
        if (resetted)
        {

            for (int i = 0; i < inv.transform.childCount; ++i)
            {
                GameObject invItem = inv.transform.GetChild(i).gameObject;
                Debug.Log(invItem.GetComponent<PolygonCollider2D>().bounds.size.x);
                if (invItem.GetComponent<PolygonCollider2D>().bounds.size.x > 0.4f)
                {
                    invItem.transform.localScale = new Vector3(0.5f, 0.5f, 0); //replace with accurate reduction
                }
                else
                {
                    invItem.transform.localScale = new Vector3(1, 1, 0);
                }
                resetted = false;
            }
        }




    }


    public void unSpool(string s)
    {
        GameObject inv = FindObjectOfType<Inventory>().gameObject;

        string firstPart = "";

        if (getRecipe(s) == "")
        {
            return;
        }
        else
        {
            string recipe = getRecipe(s);
            char[] chArray = new char[1] { char.Parse("_") };


            foreach (string str in recipe.Split(chArray))
            {
                for (int i = 0; i < inv.transform.childCount; ++i)
                {
                    bool specialFound = false;

                    if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite != null 
                        && inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.name == selectedItem)
                    {
                        inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null; //delete special from inv
                        specialFound = true;

                    }
                    if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite == null) //replace empties in inv with ingredients
                    {
                        Sprite sprite = Resources.Load("Combos/" + str, typeof(Sprite)) as Sprite;
                        inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = sprite;

                        if (specialFound) //updated selectedItem
                        {
                            firstPart = sprite.name;
                            specialFound = false;
                        }

                        Destroy(inv.transform.GetChild(i).GetComponent<PolygonCollider2D>());   //reset collider for special item in inventory (maybe replace with reset function)
                        inv.transform.GetChild(i).gameObject.AddComponent<PolygonCollider2D>();  
                        resetted = true;

                        break;
                    }
                }
            }
            selectedItem = null;
        }

        if (!controller) //reset actionText on unspool for pointer
        {
            for (int i = 0; i < inv.transform.childCount; ++i)
            {

                if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite != null)
                {
                    if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.name == firstPart)
                    {
                        //Debug.Log("UPDATED");
                        inv.transform.GetChild(i).GetComponent<Slot>().OnMouseEnter();
                        //text.GetComponent<TMP_Text>().text = "Press 'LMB' to select"; //reset actionText on unspool
                    }

                }

            }

            

           
        }



    }

    public string getRecipe(string i)
    {
        switch (i)
        {
            case "bbq":
                return "stick_sock";
            case "tent":
                return "sock_stick";
            case "birdBS":
                return "bottle_sock";
            case "birdGF":
                return "feather_grass";
            case "catkin":
                return "stick_ribbon";
            case "drum":
                return "bottle_rock";
            case "fire":
                return "stick_rock";
            case "flowerpot":
                return "bottle_catkin";
            case "pillow":
                return "sock_grass";
            case "plutonium":
                return "grass_bottle";
            case "present":
                return "stocking_ribbon";
            case "stocking":
                return "sock_rock";
            case "sushi":
                return "pillow_ribbon";
            case "teapot":
                return "acorn_bottle";
            case "tea":
                return "teapot_fire";
            case "kite":
                return "feather_ribbon";
            case "rocket":
                return "kite_plutonium";
            case "food":
                return "bbq_grass";
            case "snail":
                return "grass_rock_stick";
            case "lightning":
                return "drum_stick";
            case "squirrel":
                return "catkin_acorn";
            case "tree":
                return "squirrel_grass";
            case "baby":
                return "tree_feather_sock";
            default:
                return "";
        }
    }

    public string getSpecial(string i)
    {
        switch (i)
        {
            case "stick_sock":
                return "bbq";
            case "sock_stick":
                return "tent";
            case "acorn_bottle":
                return "teapot";
            case "teapot_fire":
                return "tea";
            case "bottle_catkin":
                return "flowerpot";
            case "bottle_rock":
                return "drum";
            case "bottle_sock":
                return "birdBS";
            case "feather_grass":
                return "birdGF";
            case "grass_bottle":
                return "plutonium";
            case "pillow_ribbon":
                return "sushi";
            case "present":
                return "stocking_ribbon";
            case "sock_grass":
                return "pillow";
            case "sock_rock":
                return "stocking";
            case "stick_ribbon":
                return "catkin";
            case "stick_rock":
                return "fire";
            case "feather_ribbon":
                return "kite";
            case "kite_plutonium":
                return "rocket";
            case "bbq_grass":
                return "food";
            case "grass_rock_stick":
                return "snail";
            case "drum_stick":
                return "lightning";
            case "catkin_acorn":
                return "squirrel";
            case "squirrel_grass":
                return "tree";
            case "tree_feather_sock":
                return "baby";
            default:
                return "";
        }
    }



}
