using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{

    public Sprite taken;
    Pointer p;
    Pointer pointer;
    Sprite sprite;
    createCombo combo;
    actionText hoverText;
    Character whirl;
    Controls controls;
    selector select;

    bool hover;


    private void Start()
    {
        combo = FindObjectOfType<createCombo>();
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
        p = FindObjectOfType<Pointer>();
        pointer = p.GetComponent<Pointer>();
        sprite = GetComponent<SpriteRenderer>().sprite;


        if (GetComponent<SpriteRenderer>().sprite != null)
        {

            pointer.holding = sprite;   //addtopointer

            taken = sprite;
            GetComponent<SpriteRenderer>().sprite = null;    //empty slot


            if (combo.stageCounter == 0)
            {
                FindObjectOfType<placeItem>().instantPlace();
            }
            else
            {
                FindObjectOfType<createCombo>().instantCombine();
            }

        }

    }

    public void OnMouseEnter()  //controls text appears
    {
        hover = true;

        p = FindObjectOfType<Pointer>();

        sprite = GetComponent<SpriteRenderer>().sprite;

        if (p.holdingItem == false && taken==null && sprite!=null)
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
                            hoverText.GetComponent<TMP_Text>().text = "Press 'RMB' to unspool / 'LMB' to select";

                        }
                        else
                        {
                            hoverText.GetComponent<TMP_Text>().text = "Press 'LMB' to select";
                        }
                    }

                    p.holding = p.hand;
                }
                
            }
           
        }
    }



    private void OnMouseExit() //controls text disappears
    {
        hover = false;

        p = FindObjectOfType<Pointer>();

        if (p.holdingItem == false && !whirl.cSpoken)
        {

            p.holding = p.empty;

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


