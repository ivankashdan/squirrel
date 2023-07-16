using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    pVisible p;

    float stickTimer = 0;
    bool stickSwitch = true;

    public bool controller;


    //Dictionary<string, string> recipes = new Dictionary<string, string>()
    //{

    //    {"sock_stick", "tent"},
    //    {"bottle_sock", "birdBS"},
    //    {"feather_grass", "birdGF"},
    //    {"ribbon_stick", "catkin"},
    //    {"bottle_rock", "drum"},
    //    {"rock_stick", "fire"},
    //    {"bottle_catkin", "flowerpot"},
    //    {"grass_sock", "pillow"},
    //    {"bottle_grass", "plutonium"},
    //    //{"ribbon_stocking", "present"},
    //    {"rock_sock", "stocking"},
    //    {"pillow_ribbon", "sushi"},
    //    {"acorn_bottle", "teapot"},
    //    {"fire_teapot", "tea" },
    //    {"feather_ribbon", "kite"},
    //    {"kite_plutonium", "rocket"},
    //    {"grass_rock_stick", "snail"},
    //    {"drum_stick","lightning"},

    //    {"acorn_catkin","squirrel"},
    //    {"grass_squirrel", "tree"},
    //    {"feather_sock_tree", "baby"}
    //    //{"stick_sock", "bbq"},
    //    //{"bbq_grass", "food"},
    //};

    Dictionary<combosEnum, combosEnum> recipe = new Dictionary<combosEnum, combosEnum>()
    {
        {combosEnum.sock_stick, combosEnum.tent},
        {combosEnum.bottle_sock, combosEnum.birdBS},
        {combosEnum.feather_grass, combosEnum.birdGF},
        {combosEnum.ribbon_stick, combosEnum.catkin},
        {combosEnum.bottle_rock, combosEnum.drum},
        {combosEnum.rock_stick, combosEnum.fire},
        {combosEnum.bottle_catkin, combosEnum.flowerpot},
        {combosEnum.grass_sock, combosEnum.pillow},
        {combosEnum.bottle_grass, combosEnum.plutonium},
        {combosEnum.rock_sock, combosEnum.stocking},
        {combosEnum.pillow_ribbon, combosEnum.sushi},
        {combosEnum.acorn_bottle, combosEnum.teapot},
        {combosEnum.fire_teapot, combosEnum.tea},
        {combosEnum.feather_ribbon, combosEnum.kite},
        {combosEnum.kite_plutonium, combosEnum.rocket},
        {combosEnum.grass_rock_stick, combosEnum.snail},
        {combosEnum.drum_stick, combosEnum.lightning},
        {combosEnum.acorn_catkin, combosEnum.squirrel},
        {combosEnum.grass_squirrel, combosEnum.tree},
        {combosEnum.feather_sock_tree, combosEnum.baby}
    };

   


    private void Start()
    {
        combo = FindObjectOfType<placeItem>().gameObject;
        whirl = FindObjectOfType<Character>().gameObject;
        inv = FindObjectOfType<Inventory>().gameObject;
        text = FindObjectOfType<actionText>();
        p = FindObjectOfType<pVisible>();

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
        combo.GetComponent<placeItem>().stageCounter = 0;
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
                //Debug.Log(invItem.GetComponent<PolygonCollider2D>().bounds.size.x);
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


    public void unSpool(string selectedItem)
    {
        GameObject inv = FindObjectOfType<Inventory>().gameObject;

        string firstPart = "";

        if (getRecipe(selectedItem) == "")
        {
            return;
        }
        else
        {
            string recipe = getRecipe(selectedItem);
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

    public string getRecipe(string value)
    {

        if (value != "")
        {
            combosEnum v = (combosEnum)System.Enum.Parse(typeof(combosEnum), value);  //must be enum = i turned into an enum

            if (recipe.ContainsValue(v))
            {
                foreach (var r in recipe)
                {
                    if (r.Value == v)
                    {
                        Debug.Log(r.Key.ToString());
                        return r.Key.ToString();
                    }
                }

            }

        }
        return "";


    }

    public string getSpecial(string key)
    {

        if (key != "")
        {
            combosEnum k = (combosEnum)System.Enum.Parse(typeof(combosEnum), key);  //must be enum = i turned into an enum

            if (recipe.ContainsKey(k))
            {
                foreach (var r in recipe)
                {
                    if (r.Key == k)
                    {
                        Debug.Log(r.Value.ToString());
                        return r.Value.ToString();
                    }
                }

            }

        }
        return "";


    }





}
