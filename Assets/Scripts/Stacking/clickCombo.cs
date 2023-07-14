using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class clickCombo : MonoBehaviour
{

    public bool hover = false;

    Controls controls;
    Pointer p;
    actionText txt;
    Character whirl;
    createCombo combo;

    void Start()
    {
        controls = FindObjectOfType<Controls>();
        p = FindObjectOfType<Pointer>();
        txt = FindObjectOfType<actionText>();
        whirl = FindObjectOfType<Character>();
        combo = FindObjectOfType<createCombo>();
    }


    private void OnMouseOver()
    {
        if (whirl.cSpoken == false && combo.GetComponent<comboCheck>().timeOn == false)
        {

            txt.GetComponent<TMP_Text>().text = "Press 'LMB' to return";
        }

        // Debug.Log("entered");

        hover = true;
        p.holding = p.hand;
    }


    void OnMouseExit()
    {
        //Debug.Log("exited");
        hover = false;
        p.holding = p.empty;

        if (!whirl.cSpoken)
        {
            txt.GetComponent<TMP_Text>().text = "";
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
                        if (combo.GetComponent<comboCheck>().timeOn == false)
                        {
                            controls.reset();
                        }
                    }
                }
            }

        }
    }




}
