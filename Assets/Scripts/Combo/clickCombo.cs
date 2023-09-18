﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class clickCombo : MonoBehaviour
{

    public bool hover = false;

    gamePad controls;
    pVisible p;
    actionText txt;
    Character whirl;
    createCombo combo;
    Inventory inv;

    void Start()
    {
        controls = FindObjectOfType<gamePad>();
        p = FindObjectOfType<pVisible>();
        txt = FindObjectOfType<actionText>();
        whirl = FindObjectOfType<Character>();
        combo = FindObjectOfType<createCombo>();
        inv = FindObjectOfType<Inventory>();
        
    }


    private void OnMouseOver()
    {
        if (controls.controller == false)
        {
            if (whirl.cSpoken == false && combo.GetComponent<checkCombo>().timeOn == false)
            {

                txt.GetComponent<TMP_Text>().text = "Press 'LMB' to return";
            }

            // Debug.Log("entered");

            hover = true;
        }
    }


    void OnMouseExit()
    {
        if (controls.controller == false)
        {
            //Debug.Log("exited");
            hover = false;

            if (!whirl.cSpoken)
            {
                txt.GetComponent<TMP_Text>().text = "";
            }
        }

    }

    private void OnMouseDown()
    {
        if (controls.controller == false)
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite != null)
            {
                if (hover)
                {
                    if (whirl.cSpoken == false)
                    {
                        if (combo.GetComponent<checkCombo>().timeOn == false)
                        {
                            inv.reset();
                        }
                    }
                }
            }

        }
    }




}
