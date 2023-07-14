using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class createCombo : MonoBehaviour
{

    public int stageCounter;

    Pointer p;
    actionText text;



    private void Start()
    {
        p = FindObjectOfType<Pointer>();
        text = FindObjectOfType<actionText>();
       
    }


    public void instantCombine()
    {

        if (p.isBlocking == false)
        {

            if (stageCounter != 0)
            {
                

                string spriteName = gameObject.GetComponent<SpriteRenderer>().sprite.name;
                string holdingName = p.holding.name;

                Sprite newCombo = null;

               
                if (Resources.Load("Combos/" + spriteName + "_" + holdingName)) 
                {

                newCombo = Resources.Load("Combos/" + spriteName + "_" + holdingName, typeof(Sprite)) as Sprite;

                }   
                else if (Resources.Load("Combos/" + holdingName + "_" + spriteName)) //check for reverse name
                {
                    newCombo = Resources.Load("Combos/" + holdingName + "_" + spriteName, typeof(Sprite)) as Sprite;
                }

                else if (spriteName.Contains("_"))   //check for all possible variations of text 
                {

                    foreach (string i in gameObject.GetComponent<checkInv>().possCombos)
                    {
                        if (i.Contains(holdingName))
                        {
                            newCombo = Resources.Load("Combos/" + i, typeof(Sprite)) as Sprite;
                        }

                    }
                    //BELOW IS THE MORE EXPENSIVE VERSION THAT DOES IT'S OWN CHECK RATHER THAN RELYING ON CHECKINV
                        //< WOULD INSERT HERE


                }


                if (Resources.Load("SFX/" + newCombo.name))
                {
                    gameObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("SFX/" + newCombo.name, typeof(AudioClip)) as AudioClip);
                }

                gameObject.GetComponent<SpriteRenderer>().sprite = newCombo;

                   
                Destroy(GetComponent<PolygonCollider2D>());

                
                

                gameObject.AddComponent<PolygonCollider2D>();   //add collider
                stageCounter++;

                objectTalk ot = FindObjectOfType<objectTalk>();
                ot.elderComment();


                

                GetComponent<SpriteRenderer>().color = Color.white;
                p.holding = p.hand;

                Pointer.isHolding = false;


            }


        }

        text.GetComponent<TMP_Text>().text = ""; //clear actionText
        p.holding = p.empty;
    }




}
