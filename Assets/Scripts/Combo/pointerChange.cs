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
            pVisible p = FindObjectOfType<pVisible>();

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

            pVisible p = FindObjectOfType<pVisible>();

            if (p.holding == icon)
            {
                p.holding = p.empty;
            }
        }
    }
}
