using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{


    public void ResizeItem(GameObject item, float scaleDefault)
    {
        float sizeLimit = 0.4f;
        PolygonCollider2D collider = item.GetComponent<PolygonCollider2D>();

        if (collider != null)
        {
            // Get the current bounds of the polygon collider
            Bounds bounds = collider.bounds;

            // Calculate the largest scale factor required to fit within the size limit
            float largestScaleFactor = Mathf.Clamp(sizeLimit / Mathf.Max(bounds.size.x, bounds.size.y), 0.1f, 1f);

            // Apply the scale factor uniformly to maintain aspect ratio
            Vector3 newScale = new Vector3(largestScaleFactor * scaleDefault, largestScaleFactor * scaleDefault, 1f);
            item.transform.localScale = newScale;

            // Destroy and recreate the Polygon Collider2D component
            Destroy(collider);
            PolygonCollider2D newCollider = item.AddComponent<PolygonCollider2D>();
        }
    }

    private void Awake()
    {

        const float posDefault = 0f;
        transform.localPosition = new Vector3(posDefault, posDefault, posDefault);

        float scaleDefault = 0.076f;
        transform.localScale = new Vector3(scaleDefault, scaleDefault, 1f);

        
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

        if (transform.parent.tag == "Slot") //resize
        {
            ResizeItem(gameObject, scaleDefault);
        }
        else if (transform.parent.tag == "Combo")
        {
            FindObjectOfType<cDialogue>().elderComment(transform.name);
        }

        Inventory inv = FindObjectOfType<Inventory>();
        inv.StartCoroutine(inv.HideItems()); //refresh inventory, because an item was changed

    }


    private void OnMouseOver()   //need to figure out how to get this into 'Mouse' script
    {
        Mouse mouse = FindObjectOfType<Mouse>();
        Character character = FindObjectOfType<Character>();
        if (mouse.mouseActive && !character.clickContinue)
        {
            //actionText 
            GameObject actionText = GameObject.FindWithTag("actionText");
            actionText.GetComponent<TMP_Text>().text = "Select item with 'LMB'";


            if (Input.GetMouseButtonDown(0))
            {
                if (transform.parent.tag == "Slot")
                {
                    FindObjectOfType<Actions>().SelectItem(gameObject);

                }
                else if (transform.parent.tag == "Combo")
                {
                    FindObjectOfType<Actions>().Return(gameObject);
                }

            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (transform.parent.tag == "Slot")
                {
                    FindObjectOfType<Actions>().Unspool(gameObject);
                }
            }

        }

    }






}
