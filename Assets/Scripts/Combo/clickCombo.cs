using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class clickCombo : MonoBehaviour
{

    public bool hover = false;

    Controls controls;
    pVisible p;
    actionText txt;
    Character whirl;
    placeItem combo;

    void Start()
    {
        controls = FindObjectOfType<Controls>();
        p = FindObjectOfType<pVisible>();
        txt = FindObjectOfType<actionText>();
        whirl = FindObjectOfType<Character>();
        combo = FindObjectOfType<placeItem>();
    }


    private void OnMouseOver()
    {
        if (whirl.cSpoken == false && combo.GetComponent<comboCheck>().timeOn == false)
        {

            txt.GetComponent<TMP_Text>().text = "Press 'LMB' to return";
        }

        // Debug.Log("entered");

        hover = true;
    }


    void OnMouseExit()
    {
        //Debug.Log("exited");
        hover = false;

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
