using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialSelect : MonoBehaviour
{

    GameObject control;
    
    void Start()
    {
        control = FindObjectOfType<gamePad>().gameObject;

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }



   
    void Update()
    {
        
        if (control.GetComponent<gamePad>().selectedItem != null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}
