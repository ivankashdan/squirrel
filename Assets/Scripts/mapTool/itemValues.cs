using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class itemValues : MonoBehaviour
{
    Inventory inv;
    recipeBook rb;
    textAvailable textA;
    textValues textV;

    public enum comboType
    {
        special,
        preSpecial,
        combo,
        starter,
        inaccessible
    }

    public comboType cType;

    public int stage = 1;

    public List<combosEnum> components = new List<combosEnum>();
    public List<combosEnum> pieces = new List<combosEnum>();
    public List<combosEnum> all = new List<combosEnum>();


    private void Start()
    {
        rb = FindObjectOfType<recipeBook>();
        textA = FindObjectOfType<textAvailable>();
        textV = FindObjectOfType<textValues>();
        
        
        cType = findType();
        //Debug.Log(cType);

        if (cType == comboType.combo)  
        {

            string[] parts = transform.name.Split('_');
            foreach (string p in parts)
            {
                components.Add((combosEnum)System.Enum.Parse(typeof(combosEnum), p));     //if it's a combo, add to components the parts that make it
            }

            breakdownC((combosEnum)System.Enum.Parse(typeof(combosEnum), transform.name)); //populate pieces list //MAKE WORK FOR COMBOS
            aBreakdownC((combosEnum)System.Enum.Parse(typeof(combosEnum), transform.name));

        }
        else if (cType == comboType.special)                                              // if it's a special, add to components the parts that make it
        {

            //components.Add((combosEnum)System.Enum.Parse(typeof(combosEnum), transform.name)); //first add special



            foreach (var r in rb.recipe)
            {

                if (r.Value == (combosEnum)System.Enum.Parse(typeof(combosEnum), transform.name))  //then break recipe for special into parts, and add them
                {
                    string[] parts = r.Key.ToString().Split('_');


                    foreach (string p in parts)
                    {



                        components.Add((combosEnum)System.Enum.Parse(typeof(combosEnum), p));


                    }


                }

            }


            breakdownS((combosEnum)System.Enum.Parse(typeof(combosEnum), transform.name)); //populate pieces list //MAKE WORK FOR COMBOS
            aBreakdownS((combosEnum)System.Enum.Parse(typeof(combosEnum), transform.name));


        }
        else if (cType == comboType.starter)                                         // if it's a start, add nothing to components
        {
            //components.Add((combosEnum)System.Enum.Parse(typeof(combosEnum), transform.name));
        }


        if (pieces.Count > 0)
        {
            stage = pieces.Count;
        }
        
    }



    string[] parts;


    void breakdownC(combosEnum c)
    {

            parts = c.ToString().Split('_');

            foreach (string p in parts)
            {
                combosEnum enumP = (combosEnum)System.Enum.Parse(typeof(combosEnum), p);

                bool special = false;

                if (rb.recipe.ContainsValue(enumP))
                {
                    special = true;
                    breakdownS(enumP);
                }

                if (special == false)
                {
                    pieces.Add(enumP);

                }


            }

        
    }


    void breakdownS (combosEnum c)
    {

        foreach (var r in rb.recipe)
        {

            if (r.Value == c)   //then break recipe for special into parts, and add them
            {
                parts = r.Key.ToString().Split('_');

                foreach (string p in parts)
                {
                    combosEnum enumP = (combosEnum)System.Enum.Parse(typeof(combosEnum), p);

                    bool special = false;

                    if (rb.recipe.ContainsValue(enumP))
                    {
                        special = true;
                        breakdownS(enumP);
                    }

                    if (special == false)
                    {
                        pieces.Add(enumP);

                    }

                }

            }
            

        }

    }


    void aBreakdownS(combosEnum c)
    {

        foreach (var r in rb.recipe)
        {

            if (r.Value == c)   //then break recipe for special into parts, and add them
            {
                parts = r.Key.ToString().Split('_');

                foreach (string p in parts)
                {
                    combosEnum enumP = (combosEnum)System.Enum.Parse(typeof(combosEnum), p);


                    if (rb.recipe.ContainsValue(enumP))
                    {
                        all.Add(enumP);
                        aBreakdownS(enumP);
                    }
                    else
                    {
                        all.Add(enumP);
                    }
                        


                }

            }


        }

    }

    void aBreakdownC(combosEnum c)
    {

        parts = c.ToString().Split('_');

        foreach (string p in parts)
        {
            combosEnum enumP = (combosEnum)System.Enum.Parse(typeof(combosEnum), p);

            if (rb.recipe.ContainsValue(enumP))
            {
                all.Add(enumP);
                aBreakdownS(enumP);
            }
            else
            {
                all.Add(enumP);
            }





        }


    }






    List <combosEnum> optionsLeft = new List<combosEnum>();

    private void OnMouseDown()
    {

        textA.GetComponent<TMP_Text>().text = "Available:<br>";

        textV.GetComponent<TMP_Text>().text = transform.name + "<br>Stage " + stage + " " + cType.ToString() + "<br>";
        foreach (var a in all)
        {
            
            if (components.Contains(a))
            {
                textV.GetComponent<TMP_Text>().text += "<color=blue>" + a.ToString() + "</color><br>";
            }

            else if (rb.recipe.ContainsValue(a))
            {
                textV.GetComponent<TMP_Text>().text += "<color=yellow>" + a.ToString() + "</color><br>";
            }
            else
            {
                textV.GetComponent<TMP_Text>().text += a.ToString() + "<br>";
            }

            
        }
        textV.GetComponent<TMP_Text>().text += "<br>";



        combosEnum comboName = (combosEnum)System.Enum.Parse(typeof(combosEnum), transform.name);

    

        for (int i = 0; i < transform.parent.childCount; i++)
        {

            bool keepItem = false;

            combosEnum childName = (combosEnum)System.Enum.Parse(typeof(combosEnum), transform.parent.GetChild(i).name);


            if (transform.parent.GetChild(i).GetComponent<itemValues>().cType == comboType.preSpecial) //make false if child is a preSpecial, need to bump this up 
            {
                keepItem = false;
            }
            else
            {

                if (childName == comboName)  //if childname matches this combo's name, keep
                {
                    keepItem = true;
                }




                foreach (var childComponent in transform.parent.GetChild(i).GetComponent<itemValues>().all)
                {

                    if (childComponent == comboName) //if childs components match this combo's name, keep //keep combos this combo goes into
                    {

                      
                        keepItem = true;
                    }

                  

                }


                if (cType == comboType.combo)
                {
                    int componentCount = components.Count;


                    foreach (var childComponent in transform.parent.GetChild(i).GetComponent<itemValues>().all)
                    {
                        foreach (var c in components)
                        {

                            if (c == childComponent)
                            {
                                componentCount -= 1;

                            }

                        }
                    }

                    if (componentCount == 0)
                    {
                        keepItem = true;
                    }
                }
                    
                   

                bool pieceClash = false;

                foreach (var c in pieces)
                {

                    if (transform.parent.GetChild(i).GetComponent<itemValues>().pieces.Contains(c)  //if child contains any of this combos pieces
                        || childName == c   //if child name = component name
                        
                       ) 
                    {

                        
                            pieceClash = true;
                        

                    }


                }

                if (pieceClash == false)
                {

                    bool alreadyPresent = false;
                    for (int ii = 0; ii < transform.parent.childCount; ii++)
                    {
                        if (transform.parent.GetChild(ii).transform.name.Contains((childName.ToString())) &&  //final check, make sure combo isn't already present. need to optimize this
                            transform.parent.GetChild(ii).transform.name.Contains((comboName.ToString()))) 
                        {
                            alreadyPresent = true;
                        }

                    }
                    if (alreadyPresent == false)
                    optionsLeft.Add(childName);


                }



            }

           


            if (keepItem == false)
            {
               

                transform.parent.GetChild(i).gameObject.SetActive(false);
            }

            if (keepItem && childName!=comboName)
            {
                textV.GetComponent<TMP_Text>().text += "<color=lightblue>" + childName.ToString() + "</color>" + "<br>";

                if (transform.parent.GetChild(i).GetComponent<itemValues>().cType == comboType.special)
                {
                    foreach (var c in transform.parent.GetChild(i).GetComponent<itemValues>().components)
                    {

                        textV.GetComponent<TMP_Text>().text += c.ToString() + "<br>";
                    }
                }
             

            }

          
        }


        foreach (var item in optionsLeft)
        {
            //if (rb.recipe.ContainsValue(item))
            //{
            //    textA.GetComponent<TMP_Text>().text += "<color=yellow>" + item + "</color><br>";
            //}
            //else
            //{
                textA.GetComponent<TMP_Text>().text += item + "<br>";
           // }
                
            //Debug.Log(item);
        }



    }

    


    public comboType findType()
    {
        if (transform.name.Contains("_"))  //determine type
        {
            if (rb.recipe.ContainsKey((combosEnum)System.Enum.Parse(typeof(combosEnum), transform.name)))
            {
                return comboType.preSpecial;
            }
            else
            {
                return comboType.combo;
            }
        }
        else
        {
            if (rb.recipe.ContainsValue((combosEnum)System.Enum.Parse(typeof(combosEnum), transform.name)))
            {
                return comboType.special;
            }
            else
            {
                return comboType.starter;
            }

        }
    }




    //public int stacked = 1;


    //private void OnCollisionEnter(Collision collision)
    //{


    //    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1, transform.localPosition.z);



    //}


}
