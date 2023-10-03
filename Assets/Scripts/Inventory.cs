using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Inventory : MonoBehaviour
{
    bool refreshing = false;
    float pWidth = 0;

    public List<string> storedItem = new List<string>()
    {
        "acorn",
        "sock"
    }; //editable in Unity


    GameObject AddSlot(float positionX, float slotWidth)
    {
        GameObject newSlot = new GameObject("Slot");
        newSlot.tag = "Slot";
        newSlot.transform.parent = gameObject.transform;

        const float defaultPositionY = 0;
        const float defaultPositionZ = 0;
        newSlot.transform.localPosition = new Vector3(positionX, defaultPositionY, defaultPositionZ);

        newSlot.transform.localScale = Vector3.one;

        pWidth += slotWidth;

        return newSlot;
    }

    void PopulateInv(List<string> storedItem)
    {
        foreach (string item in storedItem) //populate Inv with Starting Objects
        {
            GameObject newSlot = AddSlot(pWidth, 0.288f);

            Actions actions = FindObjectOfType<Actions>();
            actions.AddItem(item, newSlot.transform);
        }
    }

    void Start()
    {
        PopulateInv(storedItem);
    }

    public IEnumerator HideItems()
    {
        if (refreshing == false)
        {


            refreshing = true;

            string special = "";

            yield return new WaitForSeconds(0.01f);


            foreach (Transform slot in transform)
            {
                if (slot.transform.childCount > 0)
                {
                    Actions action = FindObjectOfType<Actions>();
                    GameObject combo = GameObject.FindWithTag("Combo");

                    GameObject item = slot.GetChild(0).gameObject;

                    if (combo.transform.childCount == 0) //turned on if no combo
                    {
                        Reveal(item);

                    }
                    else
                    {
                        string existing = combo.transform.GetChild(0).name;
                        special = action.NewComboName(item.name, existing);

                        if (Resources.Load("Combos/" + special) == false && Resources.Load("Combos/" + Recipe.GetSpecial(special)) == false)
                        {
                            Hide(item); //hide if it can't make something
                        }
                        else
                        {
                            Reveal(item); //reveal if it can make something
                        }
                    }
                }
            }
            refreshing = false;
        }
       
    }
    void Hide(GameObject item)
    {
        item.GetComponent<SpriteRenderer>().enabled = false;
        item.GetComponent<PolygonCollider2D>().enabled = false;
    }

    void Reveal(GameObject item)
    {
        item.GetComponent<SpriteRenderer>().enabled = true;
        item.GetComponent<PolygonCollider2D>().enabled = true;
        
    }


    public void ToggleInvVisible(bool visible)
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        foreach (var slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                GameObject item = slot.transform.GetChild(0).gameObject;
                SpriteRenderer sprite = item.GetComponent<SpriteRenderer>();

                if (visible)
                {
                    sprite.sortingLayerName = "UI";
                }
                else
                {
                    sprite.sortingLayerName = "Hidden";
                }
            }
        }
    }



}
