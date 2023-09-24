using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class createCombo : MonoBehaviour
{

    public int stageCounter;

    pVisible p;
    actionText text;
    hideItems check;
    Inventory inv;


    private void Start()
    {
        p = FindObjectOfType<pVisible>();
        text = FindObjectOfType<actionText>();
        check = FindObjectOfType<hideItems>();
        inv = FindObjectOfType<Inventory>();


    }

    public void CombineItem(string item)
    {

        if (stageCounter == 0) //place single item
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Combos/" + item, typeof(Sprite)) as Sprite;
        }
        else // combine items
        {
            string existing = gameObject.GetComponent<SpriteRenderer>().sprite.name;
            string comboName = check.newComboName(item, existing);

            if (Resources.Load("Combos/" + comboName))
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Combos/" + comboName, typeof(Sprite)) as Sprite;
            }
            else if (Resources.Load("Combos/" + inv.getSpecial(comboName)))  //check recipes for instant combo
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Combos/" + inv.getSpecial(comboName), typeof(Sprite)) as Sprite;
            }

            Destroy(GetComponent<PolygonCollider2D>());
        }

        gameObject.AddComponent<PolygonCollider2D>();   //add collider

        stageCounter++;

        string newCombo = gameObject.GetComponent<SpriteRenderer>().sprite.name;

        if (Resources.Load("SFX/" + newCombo)) //play Sounds if it exists
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("SFX/" + newCombo, typeof(AudioClip)) as AudioClip);
        }

        text.GetComponent<TMP_Text>().text = ""; //clear actionText

        cDialogue wO = FindObjectOfType<cDialogue>();
        wO.elderComment();

    }




}
