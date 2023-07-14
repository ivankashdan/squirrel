using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkInv : MonoBehaviour
{
    private Inventory inv;
    private Pointer p;

    string storedName;

    public List<string> possCombos = new List<string>();
    

    private void Start()
    {
        inv = FindObjectOfType<Inventory>();

    }

    private void Update()
    {




        if (gameObject.GetComponent<SpriteRenderer>().sprite == null) //if combo is occupied by no Object (do nothing) 
        {
            storedName = "_";
            return;
        }
        else
        {

            string comboName = this.gameObject.GetComponent<SpriteRenderer>().sprite.name;  //comboName

            if (storedName != comboName)
            {
                possCombos.Clear();

                for (int index = 0; index < inv.transform.childCount; ++index)
                {
                    GameObject invItem = this.inv.transform.GetChild(index).gameObject; //invItem

                    if (invItem.GetComponent<SpriteRenderer>().sprite == null)
                    {
                        if (invItem.GetComponent<Slot>().taken != null) //if invItem is empty, and taken has something in that goes with combo, reinstate in Slot
                        {
                            string takenName = invItem.GetComponent<Slot>().taken.name;  //slotItem Taken Name


                            //need to reinstate var combos here too, but it is an expensive check


                            if ((Resources.Load("Combos/" + comboName + "_" + takenName))
                                || (Resources.Load("Combos/" + takenName + "_" + comboName)))
                            {
                                //Debug.Log("Trueee!");
                                invItem.GetComponent<SpriteRenderer>().sprite = invItem.GetComponent<Slot>().taken;
                                invItem.GetComponent<Slot>().taken = null;
                            }
                        }
                    }
                    else if (invItem.GetComponent<SpriteRenderer>().sprite != null)  //if invItem is not empty
                    {

                        string slotItem = invItem.GetComponent<SpriteRenderer>().sprite.name; //slotItem Name

                        bool varPresent = false;

                        if (comboName.Contains("_"))  //check all possible variations for 3-tier if 2-tier combo present
                        {

                            //Debug.Log("check made");

                            string[] parts = comboName.Split(char.Parse("_")); //break into parts

                            List<string> par = new List<string>();
                            List<string> var = new List<string>();

                            genList(parts, slotItem, par, var);

                            for (int v = 0; v < var.Count; v++)
                            {
                                if (Resources.Load("Combos/" + var[v]))
                                {
                                    varPresent = true;
                                    possCombos.Add(var[v]);
                                    //Debug.Log("combo found!");
                                    break;
                                }

                            }
                            foreach (string p in possCombos)
                            {
                                Debug.Log(p);
                            }
                            //foreach (string v in var)
                            //{
                            //    //Debug.Log(v);
                            //}

                        }

                        if ((Resources.Load("Combos/" + comboName + "_" + slotItem) == false)
                            && (Resources.Load("Combos/" + slotItem + "_" + comboName) == false)
                            && varPresent == false)
                        {
                            invItem.GetComponent<Slot>().taken = invItem.GetComponent<SpriteRenderer>().sprite;  
                            invItem.GetComponent<SpriteRenderer>().sprite = null;
                        }

                    }

                }

            }

            storedName = comboName;
            
        }
        
    }

    public void genList(string[] p, string h, List<string> par, List<string> var)
    {
        for (int a = 0; a < p.Length; a++)
        {
            par.Add(p[a]);

        }

        par.Add(h);

        if (par.Count == 3)
        {

            for (int a = 0; a < par.Count; a++)
            {
                for (int b = 0; b < par.Count; b++)
                {
                    for (int c = 0; c < par.Count; c++)
                    {

                        if (par[c] != par[b] && par[c] != par[a] && par[b] != par[a])
                        {
                            var.Add(par[c] + "_" + par[b] + "_" + par[a]);
                        }


                    }

                }

            }



        }

    }

}
