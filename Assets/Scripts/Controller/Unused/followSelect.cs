using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class followSelect : MonoBehaviour
{

    GameObject select;
    GameObject txt;
    GameObject combo;
    GameObject y;
    GameObject whirl;

    private void Start()
    {
        select = FindObjectOfType<selector>().gameObject;
        txt = FindObjectOfType<actionText>().gameObject;
        combo = FindObjectOfType<placeItem>().gameObject;
        y = FindObjectOfType<checkInvEmpty>().gameObject;
        whirl = FindObjectOfType<Character>().gameObject;
    }

    void Update()
    {
     
        if (y.GetComponent<SpriteRenderer>().enabled || whirl.GetComponent<Character>().cSpoken || combo.GetComponent<comboCheck>().timeOn)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }


        if (gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            
            Vector3 pos = select.transform.position;
            pos.y = pos.y + 0.3f;

            gameObject.transform.position = pos;
        }

       

        




    }
}
