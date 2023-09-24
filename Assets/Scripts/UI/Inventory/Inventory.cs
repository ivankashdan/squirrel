using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public int quantity = 0;
    public Sprite[] storedItem = new Sprite [8];

    float pWidth = 0;
    float slotWidth = 0.288f;

    createCombo combo;
    gamePad gPad;
    checkCombo check;
    recipeBook rb;
    Slot slot;

    private void Start()
    {
        combo = FindObjectOfType<createCombo>();
        gPad = FindObjectOfType<gamePad>();
        check = FindObjectOfType<checkCombo>();
        rb = FindObjectOfType<recipeBook>();
        slot = FindObjectOfType<Slot>();


        for (int i = 0; i < storedItem.Length; i++) //populate Inv with Starting Objects
        {

            GameObject newItem = new GameObject("Slot");
            newItem.AddComponent<SpriteRenderer>().sprite = storedItem[i];
            newItem.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            newItem.AddComponent<PolygonCollider2D>();
            newItem.AddComponent<Slot>();
            newItem.transform.parent = gameObject.transform;
            newItem.transform.localPosition = new Vector3(pWidth, 0, 0);
            newItem.transform.localScale = new Vector3(1, 1, 1);
            pWidth += slotWidth;
            //pWidth += storedItem[i].bounds.size.x;

            quantity++;
        }


    }

 

    public void reset()
    {

        combo.GetComponent<checkCombo>().timer = 0;
        combo.GetComponent<createCombo>().stageCounter = 0;
        combo.GetComponent<SpriteRenderer>().sprite = null;
        Destroy(combo.GetComponent<PolygonCollider2D>());

        for (int i = 0; i < transform.childCount; ++i)
        {
            GameObject invItem = transform.GetChild(i).gameObject;
            if (invItem.GetComponent<SpriteRenderer>().sprite == null && invItem.GetComponent<Slot>().taken != null) // if empty, and taken is not empty
            {

                invItem.GetComponent<SpriteRenderer>().sortingLayerName = "Behind"; //HIDE new item for resize


                invItem.GetComponent<SpriteRenderer>().sprite = invItem.GetComponent<Slot>().taken; //turn taken into held
                invItem.GetComponent<Slot>().taken = null;  //remove taken

                Destroy(invItem.GetComponent<PolygonCollider2D>());   //reset collider for slot
                invItem.AddComponent<PolygonCollider2D>();

            }
        }
    }


    public void unSpool(string selectedItem)
    {
        GameObject inv = FindObjectOfType<Inventory>().gameObject;

        string firstPart = "";

        if (getRecipe(selectedItem) == "")
        {
            return;
        }
        else
        {
            string recipe = getRecipe(selectedItem);
            char[] chArray = new char[1] { char.Parse("_") };

            bool specialFound = false;

            for (int i = 0; i < inv.transform.childCount; ++i) //find Special
            {

                if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite != null
                    && inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.name == selectedItem)
                {
                    inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null; //delete special from inv
                    
                    specialFound = true;
                    break;
                }
            }

            if (specialFound)
            {

                foreach (string str in recipe.Split(chArray))
                {

                    for (int i = 0; i < inv.transform.childCount; ++i)
                    {
                        if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite == null) //replace empties in inv with ingredients
                        {

                            //inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = "Behind";  //HIDE new item for resize
                            //reset();

                            Sprite sprite = Resources.Load("Combos/" + str, typeof(Sprite)) as Sprite;
                            inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = sprite;
                            reset();
                            //Destroy(inv.transform.GetChild(i).GetComponent<PolygonCollider2D>());   //reset collider for special item in inventory (maybe replace with reset function)
                            //inv.transform.GetChild(i).gameObject.AddComponent<PolygonCollider2D>();

                            break;
                        }

                    }





                }

            }

        }

        if (!gPad.controller) //reset actionText on unspool for pointer
        {
            for (int i = 0; i < inv.transform.childCount; ++i)
            {

                if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite != null)
                {
                    if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.name == firstPart)
                    {
                        inv.transform.GetChild(i).GetComponent<Slot>().OnMouseEnter();
                    }

                }

            }




        }

    }

    public string getRecipe(string value)
    {

        if (value != "")
        {
            combosEnum v = (combosEnum)System.Enum.Parse(typeof(combosEnum), value);  //must be enum = i turned into an enum

            if (rb.recipe.ContainsValue(v))
            {
                foreach (var r in rb.recipe)
                {
                    if (r.Value == v)
                    {
                        //Debug.Log(r.Key.ToString());
                        return r.Key.ToString();
                    }
                }

            }

        }
        return "";


    }

    public string getSpecial(string key)
    {

        if (key != "")
        {
            if (System.Enum.TryParse<combosEnum>(key, out _))
            {
                combosEnum k = (combosEnum)System.Enum.Parse(typeof(combosEnum), key);  //must be enum = i turned into an enum

                if (rb.recipe.ContainsKey(k))
                {
                    foreach (var r in rb.recipe)
                    {
                        if (r.Key == k)
                        {
                            //Debug.Log(r.Value.ToString());
                            return r.Value.ToString();
                        }
                    }

                }
            }

       

        }
        return "";


    }





}
