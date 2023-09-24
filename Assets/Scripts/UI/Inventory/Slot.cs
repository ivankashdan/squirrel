using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{

    public Sprite taken;
    Sprite sprite;
    string currentSprite;

    createCombo combo;
    actionText hoverText;
    Character whirl;
    gamePad controls;
    Inventory inv;
    checkCombo check;

    bool hover;
    bool changed = false;



    private void Start()
    {
        combo = FindObjectOfType<createCombo>();
        hoverText = FindObjectOfType<actionText>();
        whirl = FindObjectOfType<Character>();
        controls = FindObjectOfType<gamePad>();
        inv = FindObjectOfType<Inventory>();
        check = FindObjectOfType<checkCombo>();

    }

    

    

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite != null)
        {

            if (gameObject.GetComponent<SpriteRenderer>().sprite.name != currentSprite)
            {
                changed = true;

            }
            if (changed)
            {
                resizeItem();

                changed = false;
                drawHotspot(gameObject);

            }


           

          


        }
        else
        {
            currentSprite = "";
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    public void resizeItem()
    {
        float sizeLimit = 0.4f;
        float itemSizeX = gameObject.GetComponent<PolygonCollider2D>().bounds.size.x;
        float itemSizeY = gameObject.GetComponent<PolygonCollider2D>().bounds.size.y;

        if (itemSizeX > sizeLimit || itemSizeY > sizeLimit)
        {
            while (itemSizeX > sizeLimit || itemSizeY > sizeLimit)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - 0.01f, gameObject.transform.localScale.y - 0.01f, 0);

                itemSizeX *= 0.99f;
                itemSizeY *= 0.99f;

            }
            //Debug.Log("new item size = " + itemSizeX + " and " + itemSizeY);

        }
        else
            transform.localScale = new Vector3(1, 1, 1);  //need this for unspool

        if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Behind")
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI"; //if item is hidden for resize, reveal

        currentSprite = gameObject.GetComponent<SpriteRenderer>().sprite.name;


    }

    void Update()
    {
        if (combo.GetComponent<SpriteRenderer>().sprite == null) //so long as there is no combo
        {
            if (hover) //if hovering over this slot
            {
                if (Input.GetMouseButtonUp(1))
                {
                    inv.unSpool(controls.selectedItem);
                }
            }
            
        }
       
        if (whirl.cSpoken || check.transformTrigger)
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

            if (controls.GetComponent<gamePad>().controller == false)  //NEW CODE
            {

                controls.GetComponent<gamePad>().selectedItem = gameObject.GetComponent<SpriteRenderer>().sprite.name;

                if (gameObject.GetComponent<SpriteRenderer>().sprite!=null)
                {
                    if (!whirl.cSpoken)
                    {
                        if (inv.getRecipe(controls.selectedItem) != "" && combo.GetComponent<SpriteRenderer>().sprite == null)
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

                if (controls.GetComponent<gamePad>().controller == false)  //NEW CODE
                {
                controls.GetComponent<gamePad>().selectedItem = null;
                hoverText.GetComponent<TMP_Text>().text = "";
                
            }

            if (hoverText.GetComponent<actionText>().itemText)
            {
                hoverText.GetComponent<TMP_Text>().text = "";
            }

        }

    }

    public void drawHotspot(GameObject obj) //WIP /////////Regnerate hotspot using saved file if exists  //there are 2 of these... need just 1
    {

        bool hsFound = false;
        if (Resources.Load("Hotspots/" + obj.GetComponent<SpriteRenderer>().sprite.name))
        {
            obj.GetComponent<SpriteRenderer>().sprite = Resources.Load("Hotspots/" + obj.GetComponent<SpriteRenderer>().sprite.name, typeof(Sprite)) as Sprite;
            hsFound = true;
        }
        if (obj.GetComponent<PolygonCollider2D>())
        {
            Destroy(obj.GetComponent<PolygonCollider2D>());
            obj.AddComponent<PolygonCollider2D>();
        }

        if (hsFound)
        {
            obj.GetComponent<SpriteRenderer>().sprite = Resources.Load("Combos/" + obj.GetComponent<SpriteRenderer>().sprite.name, typeof(Sprite)) as Sprite;
        }
    }










}


