using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public int quantity = 0;
    public Sprite[] storedItem = new Sprite [8];

    float pWidth = 0;
    float slotWidth = 0.288f;

    private void Start()
    {

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


}
