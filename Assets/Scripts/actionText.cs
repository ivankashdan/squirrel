using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class actionText : MonoBehaviour
{

    public bool itemText;

    GameObject a;
    GameObject b;
    GameObject y;

    GameObject select;
    GameObject whirl;
    GameObject combo;
    GameObject control;
    GameObject inv;
    //GameObject voice;

    pVisible p;

    int timer;
    int delay = 10;


    private void Start()
    {
        a = FindObjectOfType<followSelect>().gameObject;
        b = FindObjectOfType<specialSelect>().gameObject;
        y = FindObjectOfType<checkInvEmpty>().gameObject;

        select = FindObjectOfType<selector>().gameObject;
        whirl = FindObjectOfType<Character>().gameObject;
        combo = FindObjectOfType<placeItem>().gameObject;
        control = FindObjectOfType<Controls>().gameObject;
        inv = FindObjectOfType<Inventory>().gameObject;
        //voice = FindObjectOfType<colourRoom>().gameObject;

        p = FindObjectOfType<pVisible>();

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

        if (control.gameObject.GetComponent<Controls>().controller == false)
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





        if (control.gameObject.GetComponent<Controls>().controller)  //if controller present
        {
            timer++;

            if (timer > delay)
            {

                if (select.GetComponent<SpriteRenderer>().enabled)
                {
                    if (combo.GetComponent<SpriteRenderer>().sprite == null) //quick fix - don't let unspool happen when stacking
                    {

                        if (control.GetComponent<Controls>().getRecipe(control.GetComponent<Controls>().selectedItem) != "")
                        {
                            Debug.Log("Unspool detected");

                            gameObject.GetComponent<TMP_Text>().text = "Press 'B' to unspool / 'A' to select";
                            //a.GetComponent<SpriteRenderer>().enabled = true;
                            //b.GetComponent<SpriteRenderer>().enabled = true;
                            //y.GetComponent<SpriteRenderer>().enabled = false;

                            //a.transform.localPosition = new Vector3((0 + 2.33f), a.transform.localPosition.y, a.transform.localPosition.z);
                        }
                        else
                        {
                            gameObject.GetComponent<TMP_Text>().text = "Press 'A' to select";
                            //a.GetComponent<SpriteRenderer>().enabled = true;
                            //b.GetComponent<SpriteRenderer>().enabled = false;
                            //y.GetComponent<SpriteRenderer>().enabled = false;

                            //a.transform.localPosition = new Vector3((0 - 0.72f), a.transform.localPosition.y, a.transform.localPosition.z);
                        }



                    }

                    else
                    {
                        gameObject.GetComponent<TMP_Text>().text = "Press 'A' to select / 'Y' to return";
                        //a.GetComponent<SpriteRenderer>().enabled = true;
                        //b.GetComponent<SpriteRenderer>().enabled = false;
                        //y.GetComponent<SpriteRenderer>().enabled = false;

                        //a.transform.localPosition = new Vector3((0-0.72f), a.transform.localPosition.y, a.transform.localPosition.z);

                    }

                }
                else if (whirl.GetComponent<Character>().cSpoken)
                {
                    gameObject.GetComponent<TMP_Text>().text = "Press 'A' to continue";
                    //a.GetComponent<SpriteRenderer>().enabled = false;
                    //b.GetComponent<SpriteRenderer>().enabled = false;
                    //y.GetComponent<SpriteRenderer>().enabled = false;


                }
                else if (combo.GetComponent<comboCheck>().timeOn)
                {
                    gameObject.GetComponent<TMP_Text>().text = "";
                    //a.GetComponent<SpriteRenderer>().enabled = false;
                    //b.GetComponent<SpriteRenderer>().enabled = false;
                    //y.GetComponent<SpriteRenderer>().enabled = false;
                }
                else if (combo.GetComponent<SpriteRenderer>().sprite != null && checkInv())
                {
                    gameObject.GetComponent<TMP_Text>().text = "Press 'Y' to return";
                    //a.GetComponent<SpriteRenderer>().enabled = false;
                    //b.GetComponent<SpriteRenderer>().enabled = false;
                    //y.GetComponent<SpriteRenderer>().enabled = true;

                }
                else
                {
                    gameObject.GetComponent<TMP_Text>().text = "";
                    //a.GetComponent<SpriteRenderer>().enabled = false;
                    //b.GetComponent<SpriteRenderer>().enabled = false;
                    //y.GetComponent<SpriteRenderer>().enabled = false;

                }

                timer = 0;

            }





            //if (a.GetComponent<SpriteRenderer>().enabled)
            //{
            //    gameObject.GetComponent<TMP_Text>().text = "Select";
            //}
            //else if (y.GetComponent<SpriteRenderer>().enabled)
            //{
            //    gameObject.GetComponent<TMP_Text>().text = "Reset";
            //}
            //else
            //{
            //    gameObject.GetComponent<TMP_Text>().text = "";
            //}


        }




    }


}
