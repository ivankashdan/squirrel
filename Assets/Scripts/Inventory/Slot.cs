using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{

    public Sprite taken;
    pVisible p;
    pVisible pointer;
    Sprite sprite;
    placeItem combo;
    actionText hoverText;
    Character whirl;
    Controls controls;
    selector select;

    bool hover;


    private void Start()
    {
        combo = FindObjectOfType<placeItem>();
        hoverText = FindObjectOfType<actionText>();
        whirl = FindObjectOfType<Character>();
        controls = FindObjectOfType<Controls>();
        select = FindObjectOfType<selector>();
    }


    void Update()
    {


        if (combo.GetComponent<SpriteRenderer>().sprite == null) //so long as there is no combo
        {
            if (hover) //if hovering over this slot
            {
                if (Input.GetMouseButtonUp(1))
                {
                    controls.unSpool(controls.selectedItem);
                }
            }
            
        }
       
        if (whirl.cSpoken)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        

    }


    public void OnMouseDown()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;

        if (GetComponent<SpriteRenderer>().sprite != null) //if slot has an item in
        {
            GetComponent<SpriteRenderer>().sprite = null;    //empty slot
            taken = sprite; //leave which item taken marker

            combo.CombineItem(sprite.name);  //combine items

        }

    }

    public void OnMouseEnter()  //controls text appears
    {
        hover = true;

        sprite = GetComponent<SpriteRenderer>().sprite;

        if (taken==null && sprite!=null)
        {

            if (controls.GetComponent<Controls>().controller == false)  //NEW CODE
            {

                controls.GetComponent<Controls>().selectedItem = gameObject.GetComponent<SpriteRenderer>().sprite.name;

                if (gameObject.GetComponent<SpriteRenderer>().sprite!=null)
                {
                    if (!whirl.cSpoken)
                    {
                        if (controls.getRecipe(controls.selectedItem) != "" && combo.GetComponent<SpriteRenderer>().sprite == null)
                        {
                            //Debug.Log("unspool detected");
                            hoverText.GetComponent<TMP_Text>().text = "Press 'RMB' to unspool / 'LMB' to select";

                        }
                        else
                        {
                            //Debug.Log(controls.getRecipe(controls.selectedItem));

                            hoverText.GetComponent<TMP_Text>().text = "Press 'LMB' to select";
                        }
                    }

                }
                
            }
           
        }
    }



    private void OnMouseExit() //controls text disappears
    {
        hover = false;


        if (!whirl.cSpoken)
        {

                if (controls.GetComponent<Controls>().controller == false)  //NEW CODE
                {
                controls.GetComponent<Controls>().selectedItem = null;
                hoverText.GetComponent<TMP_Text>().text = "";
                
            }

            if (hoverText.GetComponent<actionText>().itemText)
            {
                hoverText.GetComponent<TMP_Text>().text = "";
            }

        }

    }



    



    


    
}


