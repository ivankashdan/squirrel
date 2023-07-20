using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class actionText : MonoBehaviour
{

    public bool itemText;


    GameObject select;
    GameObject whirl;
    GameObject combo;
    GameObject inv;
    gamePad gPad;

    int timer;
    int delay = 10;


    private void Start()
    {

        select = FindObjectOfType<selector>().gameObject;
        whirl = FindObjectOfType<Character>().gameObject;
        combo = FindObjectOfType<placeItem>().gameObject;
        inv = FindObjectOfType<Inventory>().gameObject;
        gPad = FindObjectOfType<gamePad>();

    }

 

    bool checkInv()
    {
        for (int i = 0; i < inv.transform.childCount; i++)
        {
            if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite != null)
            {
                return false;
            }

           
        }
        return true;

    }

  


    void FixedUpdate()
    {

        if (gPad.controller == false)
        {
            if (whirl.GetComponent<Character>().cSpoken)
            {


                gameObject.GetComponent<TMP_Text>().text = "Press 'LMB' to continue";


            }
            else
            {
                if (gameObject.GetComponent<TMP_Text>().text == "Press 'LMB' to continue")
                {
                    gameObject.GetComponent<TMP_Text>().text = "";
                }

            }
        }

        if (gPad.controller)  //if controller present
        {
            timer++;

            if (timer > delay)
            {

                if (select.GetComponent<SpriteRenderer>().enabled)
                {
                    if (combo.GetComponent<SpriteRenderer>().sprite == null) //quick fix - don't let unspool happen when stacking
                    {

                        if (inv.GetComponent<Inventory>().getRecipe(gPad.GetComponent<gamePad>().selectedItem) != "")
                        {
                            Debug.Log("Unspool detected");

                            gameObject.GetComponent<TMP_Text>().text = "Press 'B' to unspool / 'A' to select";
                        }
                        else
                        {
                            gameObject.GetComponent<TMP_Text>().text = "Press 'A' to select";
                        }



                    }

                    else
                    {
                        gameObject.GetComponent<TMP_Text>().text = "Press 'A' to select / 'Y' to return";
                    }

                }
                else if (whirl.GetComponent<Character>().cSpoken)
                {
                    gameObject.GetComponent<TMP_Text>().text = "Press 'A' to continue";
                }
                else if (combo.GetComponent<comboCheck>().timeOn)
                {
                    gameObject.GetComponent<TMP_Text>().text = "";
                }
                else if (combo.GetComponent<SpriteRenderer>().sprite != null && checkInv())
                {
                    gameObject.GetComponent<TMP_Text>().text = "Press 'Y' to return";

                }
                else
                {
                    gameObject.GetComponent<TMP_Text>().text = "";

                }

                timer = 0;

            }

        }




    }


}
