using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class placeItem : MonoBehaviour
{

    public int stageCounter;

    pVisible p;
    actionText text;



    private void Start()
    {
        p = FindObjectOfType<pVisible>();
        text = FindObjectOfType<actionText>();
       
    }

    public void CombineItem(string item)
    {
        string newCombo = item;

        if (stageCounter == 0) //place single item
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Combos/" + item, typeof(Sprite)) as Sprite;
        }
        else // combine items
        {
            newCombo = Combine(item);
        }

        gameObject.AddComponent<PolygonCollider2D>();   //add collider

        stageCounter++;

        if (Resources.Load("SFX/" + newCombo)) //play Sounds if it exists
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("SFX/" + newCombo, typeof(AudioClip)) as AudioClip);
        }

        text.GetComponent<TMP_Text>().text = ""; //clear actionText

        wObjects wO = FindObjectOfType<wObjects>();
        wO.elderComment();

    }

    public string Combine(string item)
    {

        string existing = gameObject.GetComponent<SpriteRenderer>().sprite.name;
       
        Sprite newCombo = null;

               
        if (Resources.Load("Combos/" + existing + "_" + item))    //NEED TO UPDATE ALL THIS SO IT JUST DOES 1 ALPHABETICAL CHECK???
        {

        newCombo = Resources.Load("Combos/" + existing + "_" + item, typeof(Sprite)) as Sprite;

        }   
        else if (Resources.Load("Combos/" + item + "_" + existing)) //check for reverse name
        {
            newCombo = Resources.Load("Combos/" + item  + "_" + existing, typeof(Sprite)) as Sprite;
        }

        else if (existing.Contains("_"))   //check for all possible variations of text 
        {

            foreach (string i in gameObject.GetComponent<checkInv>().possCombos)
            {
                if (i.Contains(item))
                {
                    newCombo = Resources.Load("Combos/" + i, typeof(Sprite)) as Sprite;
                }

            }

        }

        gameObject.GetComponent<SpriteRenderer>().sprite = newCombo;

        Destroy(GetComponent<PolygonCollider2D>());

        return newCombo.name;
    }




}
