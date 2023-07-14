using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointerChange : MonoBehaviour
{

    public Sprite icon;
    public bool NoChange;

    private void OnMouseEnter()
    {
        if (NoChange == false)
        {
            Pointer p = FindObjectOfType<Pointer>();

            if (p.holdingItem == false)
            {
                p.holding = icon;
            }

        }



    }

    private void OnMouseExit()
    {
        if (NoChange == false)
        {

            Pointer p = FindObjectOfType<Pointer>();

            if (p.holding == icon)
            {
                p.holding = p.empty;
            }
        }
    }
}
