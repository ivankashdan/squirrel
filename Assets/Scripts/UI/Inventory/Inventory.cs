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

    public void checkTime(string i)
    {

        combosEnum cEnum = (combosEnum)System.Enum.Parse(typeof(combosEnum), i);

        switch (cEnum)
        {
            case combosEnum.fire:
            case combosEnum.tent:
            case combosEnum.lightning:
            case combosEnum.rocket:
            case combosEnum.squirrel:
            case combosEnum.tree:
                check.timeLength = 0;
                break;
            default:
                check.timeLength = check.timeLengthD;
                break;
        }

    }

    private void Start()
    {
        combo = FindObjectOfType<createCombo>();
        gPad = FindObjectOfType<gamePad>();
        check = FindObjectOfType<checkCombo>();
        rb = FindObjectOfType<recipeBook>();


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
            if (invItem.GetComponent<SpriteRenderer>().sprite == null && invItem.GetComponent<Slot>().taken != null)
            {

                invItem.GetComponent<SpriteRenderer>().sortingLayerName = "Behind"; //HIDE new item for resize


                invItem.GetComponent<SpriteRenderer>().sprite = invItem.GetComponent<Slot>().taken;
                invItem.GetComponent<Slot>().taken = null;

                Destroy(invItem.GetComponent<PolygonCollider2D>());   //reset collider for special item in inventory
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


            foreach (string str in recipe.Split(chArray))
            {
                for (int i = 0; i < inv.transform.childCount; ++i)
                {
                    bool specialFound = false;

                    if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite != null
                        && inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.name == selectedItem)
                    {
                        inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null; //delete special from inv
                        specialFound = true;

                    }
                    if (inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite == null) //replace empties in inv with ingredients
                    {

                        inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = "Behind";  //HIDE new item for resize

                        Sprite sprite = Resources.Load("Combos/" + str, typeof(Sprite)) as Sprite;
                        inv.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = sprite;

                        if (specialFound) //updated selectedItem
                        {
                            firstPart = sprite.name;
                            specialFound = false;
                        }

                        Destroy(inv.transform.GetChild(i).GetComponent<PolygonCollider2D>());   //reset collider for special item in inventory (maybe replace with reset function)
                        inv.transform.GetChild(i).gameObject.AddComponent<PolygonCollider2D>();
                       

                        break;
                    }
                }
            }
            selectedItem = null;
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
        return "";


    }





}