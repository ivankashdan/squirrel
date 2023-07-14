using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialSelect : MonoBehaviour
{

    GameObject control;
    
    void Start()
    {
        control = FindObjectOfType<Controls>().gameObject;

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }



   
    void Update()
    {
        
        if (control.GetComponent<Controls>().selectedItem != null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}
