using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class selector : MonoBehaviour
{


    Inventory inv;
    gamePad controls;

    private void Start()
    {

        inv = FindAnyObjectByType<Inventory>();
        controls = FindObjectOfType<gamePad>();
    }



    void Update()
    {

        if (controls.controller)
        {
            if (Gamepad.current == null) 
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                return;
            }
        }


        if (FindObjectOfType<Character>().GetComponent<Character>().cSpoken)
        {
            if (gameObject.GetComponent<SpriteRenderer>().enabled)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }

        }
        else
        {
            if (controls.controller)
            {
                if (gameObject.GetComponent<SpriteRenderer>().enabled == false)   
                {

                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            if (inv.GetComponent<gamePad>().selectedItem != null)
            {
                string selected = inv.GetComponent<gamePad>().selectedItem;

                for (int i = 0; i < inv.transform.childCount; i++)
                {

                    if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite != null)
                    {
                        if (selected == inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.name)
                        {
                            gameObject.transform.position = inv.transform.GetChild(i).position;
                            return;
                        }
                    }


                }

                gameObject.GetComponent<SpriteRenderer>().enabled = false;  //make selector invisible if no items to select

            }
        }


       
        

       


    }
}
