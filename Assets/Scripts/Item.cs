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
        }
    }

    private void Awake()
    {

        transform.localPosition = new Vector3(0, 0, 0);
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
            if (Resources.Load("SFX/" + transform.name)) //play Sounds if it exists
                transform.parent.GetComponent<AudioSource>().PlayOneShot(Resources.Load("SFX/" + transform.name, typeof(AudioClip)) as AudioClip);

            FindObjectOfType<cDialogue>().elderComment(transform.name);
        }

        Inventory inv = FindObjectOfType<Inventory>();
        inv.StartCoroutine(inv.HideItems()); //refresh inventory, because an item was changed

    }


 
 




}
