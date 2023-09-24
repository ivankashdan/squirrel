using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class hideItems : MonoBehaviour
{
    Inventory inv;
    checkCombo check;
    pVisible p;

    string storedName;
    

    private void Start()
    {
        inv = FindObjectOfType<Inventory>();
        check = FindObjectOfType<checkCombo>();
        p = FindObjectOfType<pVisible>();

    }

    public string newComboName(string addition, string original)
    {

        List<string> parts = new List<string>();

        if (original.Contains("_"))
        {
            parts = original.Split(char.Parse("_")).ToList();
        }
        else
        {
            parts.Add(original);
        }

        parts.Add(addition);
        parts.Sort();
        string name = "";

        foreach (string p in parts)
        {
            if (name != "")
                name += "_";
            name += p;
        }

        return name;

    }


    private void FixedUpdate()
    {

        if (gameObject.GetComponent<SpriteRenderer>().sprite == null) //if combo is occupied by no Object (do nothing) 
        {
            storedName = "_";
            return;
        }
        else 
        //if (!check.transformTrigger && p.gameObject.GetComponent<SpriteRenderer>().sprite!=null)
        {

            string comboName = gameObject.GetComponent<SpriteRenderer>().sprite.name;  

            if (storedName != comboName)   //if combo is the same as before, do nothing
            {
                storedName = comboName; //if combo is new, store name


                for (int index = 0; index < inv.transform.childCount; ++index)
                {
                    GameObject invItem = inv.transform.GetChild(index).gameObject; //invItem

                    if (invItem.GetComponent<SpriteRenderer>().sprite == null)
                    {
                        if (invItem.GetComponent<Slot>().taken != null) //if invItem is empty, and taken has something in that goes with combo, reinstate in Slot
                        {
                            string takenName = invItem.GetComponent<Slot>().taken.name;  //slotItem Taken Name
                            string newCombo = newComboName(takenName, comboName);

                            if (Resources.Load("Combos/" + newCombo) 
                                || Resources.Load("Combos/" + inv.getSpecial(newCombo))
                                )  //get what would be the new combo name, and check if it exists before making the item available
                            {
                                invItem.GetComponent<SpriteRenderer>().sprite = invItem.GetComponent<Slot>().taken;
                                invItem.GetComponent<Slot>().taken = null;
                            }
                            
                        }
                    }
                    else if (invItem.GetComponent<SpriteRenderer>().sprite != null)  //if invItem is not empty, remove from Slot and send to taken
                    {
                        string slotItem = invItem.GetComponent<SpriteRenderer>().sprite.name; //slotItem Name
                        string special = newComboName(slotItem, comboName);

                        if (Resources.Load("Combos/" + special) == false 
                            && Resources.Load("Combos/" + inv.getSpecial(special)) == false)
                        {
                            //Debug.Log(newComboName(slotItem, comboName) + " is false");
                            invItem.GetComponent<Slot>().taken = invItem.GetComponent<SpriteRenderer>().sprite;  
                            invItem.GetComponent<SpriteRenderer>().sprite = null;
                        }

                    }

                }

            }
 
            
        }
        
    }


}
