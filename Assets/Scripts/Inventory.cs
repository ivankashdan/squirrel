using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    float pWidth = 0;
    const float slotWidth = 0.288f;
    const float posDefault = 0;
    const float scaleDefault = 1;

    bool refreshing = false;



    public List<string> storedItem = new List<string>()
    {
        "acorn",
        "sock"
    }; //editable in Unity


    GameObject AddSlot()
    {
        GameObject newSlot = new GameObject("Slot");
        newSlot.transform.parent = gameObject.transform;
        newSlot.transform.localPosition = new Vector3(pWidth, posDefault, posDefault);
        newSlot.transform.localScale = new Vector3(scaleDefault, scaleDefault, scaleDefault); ;
        pWidth += slotWidth;
        newSlot.AddComponent<Slot>();
        return newSlot;
    }

    void PopulateInv(List<string> storedItem)
    {
        Actions actions = FindObjectOfType<Actions>();

        foreach (string item in storedItem) //populate Inv with Starting Objects
        {
            GameObject newSlot = AddSlot();
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
            yield return new WaitForSeconds(0.01f);

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
                        string special = action.NewComboName(item.name, existing);

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

    
   


}
