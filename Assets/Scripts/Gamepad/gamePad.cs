using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class gamePad : MonoBehaviour
{

    public string selectedItem;
    public bool rumble;


    public int slotNumber;
    private bool leftRight;
    private GameObject combo;
    private GameObject whirl;
    private Inventory inv;
    pVisible p;

    float stickTimer = 0;
    bool stickSwitch = true;

    public bool controller;


    
    private void Start()
    {
        combo = FindObjectOfType<createCombo>().gameObject;
        whirl = FindObjectOfType<Character>().gameObject;
        inv = FindObjectOfType<Inventory>();
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
                    combo.GetComponent<checkCombo>().timer = combo.GetComponent<checkCombo>().timeLengthD;

            }
            else if (combo.GetComponent<checkCombo>().timeOn)
            {
                if (current.aButton.wasPressedThisFrame || current.yButton.wasPressedThisFrame || current.bButton.wasPressedThisFrame)
                {
                    combo.GetComponent<checkCombo>().timer = combo.GetComponent<checkCombo>().timeLengthD;
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
                    combo.GetComponent<checkCombo>().timer = 0;

                if (current.yButton.wasPressedThisFrame)
                {
                

                    
                    inv.reset();
                    
                }

                if (current.bButton.wasPressedThisFrame)
                {
                    if (combo.GetComponent<SpriteRenderer>().sprite==null)  //quick fix - don't let unspool happen when stacking
                    {
                        if (FindObjectOfType<selector>().GetComponent<SpriteRenderer>().enabled)
                        {
                            inv.unSpool(selectedItem);
                        }
                    }

                 
                }
            }



            stickTimer += Time.deltaTime;
            

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
        }
    }



    



}
