using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkInvEmpty : MonoBehaviour
{

    
    GameObject inv;
    GameObject combo;
    GameObject a;
    GameObject whirl;

    bool invEmpty;

    void Start()
    {
        inv = FindObjectOfType<Inventory>().gameObject;
        combo = FindObjectOfType<createCombo>().gameObject;
        a = FindObjectOfType<followSelect>().gameObject;
        whirl = FindObjectOfType<Character>().gameObject;

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
    }

    public void checkInv()
    {
        for (int i = 0; i < inv.transform.childCount; i++)
        {
            if (inv.transform.GetChild(i).transform.GetComponent<SpriteRenderer>().sprite != null)
            {
                invEmpty = false;
                return;
            }

        }

        invEmpty = true;

        //if (combo.GetComponent<comboCheck>().timeOn == false)
        //{

            //gameObject.GetComponent<SpriteRenderer>().enabled = true;
       // Debug.Log("y appears");
       // }

    }

    void Update()
    {

        if (combo.GetComponent<checkCombo>().timeOn || whirl.GetComponent<Character>().cSpoken) // final 1 is temporary!
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            
            

        }
        else if (combo.GetComponent<SpriteRenderer>().sprite == null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            invEmpty = false;
        }
        else if (invEmpty)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
            
        
        
        

        

        

    }
}
