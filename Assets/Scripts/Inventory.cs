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
        newSlot.AddComponent<Slot>();

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

            //Hidden(true);

            foreach (Transform slot in transform)
            {
                if (slot.transform.childCount > 0)
                {
                    Actions action = FindObjectOfType<Actions>();
                    Recipe rb = FindObjectOfType<Recipe>();
                    Combo combo = FindObjectOfType<Combo>();

                    GameObject item = slot.GetChild(0).gameObject;

                    if (combo.transform.childCount == 0) //turned on if no combo
                    {
                        Reveal(item);

                    }
                    else
                    {
                        string existing = combo.transform.GetChild(0).name;
                        special = action.NewComboName(item.name, existing);

                        if (Resources.Load("Combos/" + special) == false && Resources.Load("Combos/" + rb.GetSpecial(special)) == false)
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

            //Hidden(false);

            refreshing = false;
        }
       
    }

    void Hidden(bool b)
    {
        Character character = FindObjectOfType<Character>();
        if (!character.clickContinue)
        {
            character.ToggleInvVisible(!b); //reveal inv aftering processing
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

    
   


}
