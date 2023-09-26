using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    //List<Item> ingredients;
    //int stage;
    const float posDefault = 0f;
    const float scaleUp = 0.076f;

    private void Awake()
    {
        transform.localPosition = new Vector3(posDefault, posDefault, posDefault);
        transform.localScale = new Vector3(scaleUp, scaleUp, scaleUp);

        SpriteRenderer sprite = gameObject.AddComponent<SpriteRenderer>();  

        if (Resources.Load("Hotspots/" + transform.name)) //add hotspot collider if exists
        {
            sprite.sprite = Resources.Load("Hotspots/" + transform.name, typeof(Sprite)) as Sprite;   
            gameObject.AddComponent<PolygonCollider2D>();
        }
        if (Resources.Load("Combos/" + transform.name)) //add sprite
        {
            sprite.sprite = Resources.Load("Combos/" + transform.name, typeof(Sprite)) as Sprite; 
            sprite.sortingLayerName = "UI";
            if (!gameObject.GetComponent<PolygonCollider2D>()) //add collider if no hotspot
                gameObject.AddComponent<PolygonCollider2D>();
        }

        Inventory inv = FindObjectOfType<Inventory>();
        inv.StartCoroutine(inv.HideItems()); //refresh inventory, because an item was changed

    }


    private void OnMouseOver()   //need to figure out how to get this into 'Mouse' script
    {
        if (FindObjectOfType<Mouse>().mouseActive)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (transform.parent.name == "Slot")
                {
                    FindObjectOfType<Actions>().SelectItem(gameObject);

                }
                else if (transform.parent.name == "Combo")
                {
                    FindObjectOfType<Actions>().Return(gameObject);
                }

            }
            else if (Input.GetMouseButtonDown(1))
            {
                FindObjectOfType<Actions>().Unspool(gameObject);

            }

        }

    }






}
