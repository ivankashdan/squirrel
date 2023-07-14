using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class placeItem : MonoBehaviour
{


    Pointer p;
    createCombo combo;
    actionText text;

    private void Start()
    {
        p = FindObjectOfType<Pointer>();
        combo = FindObjectOfType<createCombo>();
        text = FindObjectOfType<actionText>();

    }

 

    public void instantPlace()
    {

        if (p.holding != p.empty && combo.stageCounter == 0)
        {

            Sprite res;    //too speed up putting in new objects

            if (Resources.Load("Combos/" + p.holding.name) == null) 
            {
                res = Resources.Load("Items/" + p.holding.name, typeof(Sprite)) as Sprite;
            }
            else
            {
                res = Resources.Load("Combos/" + p.holding.name, typeof(Sprite)) as Sprite;
            }

            combo.gameObject.GetComponent<SpriteRenderer>().sprite = res;


            combo.gameObject.AddComponent<PolygonCollider2D>();


            p.holding = p.hand;

            Pointer.isHolding = false;


            combo.stageCounter = 1;


            objectTalk ot = FindObjectOfType<objectTalk>();
            ot.elderComment();

        }

        text.GetComponent<TMP_Text>().text = ""; //clear actionText
        p.holding = p.empty;

    }


    


   
}
