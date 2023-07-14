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


    //BELOW IS CODE FOR TRIMMED SPACING (NOT USED CURRENTLY)
    public float padding = 0;


    private void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            updatePos();
        }


    }

    void updatePos()
    {


        float prev = 0;

        for (int i = 0; i < transform.childCount; i++) //replace 10 eventually
        {


            if (transform.GetChild(i).GetComponent<SpriteRenderer>().sprite == null)
            {
            }
           else
            {


                float bounds = transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.bounds.size.x;
                transform.GetChild(i).transform.localPosition = new Vector3(prev+(bounds/2), 0, 0);
                prev += (bounds
                    + padding
                    );  //spacing added to previous width
                Debug.Log(prev);


            }


        }


    }

   

}
