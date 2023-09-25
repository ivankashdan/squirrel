using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    float pWidth = 0;
    float slotWidth = 0.288f;

    public List<string> storedItem = new List<string>()
    {
        "acorn",
        "sock"
    }; //editable in Unity


    GameObject addSlot()
    {
        GameObject newSlot = new GameObject("Slot");
        newSlot.transform.parent = gameObject.transform;
        newSlot.transform.localPosition = new Vector3(pWidth, 0, 0);
        newSlot.transform.localScale = new Vector3(1, 1, 1);
        pWidth += slotWidth;
        newSlot.AddComponent<Slot>();
        return newSlot;
    }

    void populateInv(List<string> storedItem)
    {
        for (int i = 0; i < storedItem.Count; i++) //populate Inv with Starting Objects
        {
            GameObject newSlot = addSlot();

            GameObject newItem = new GameObject(storedItem[i]);
            newItem.transform.parent = newSlot.transform;
            newItem.AddComponent<Item>();
        }
    }

    void Start()
    {
        populateInv(storedItem);


    }

}
