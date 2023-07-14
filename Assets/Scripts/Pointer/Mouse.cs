using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    Controls controls;
    Pointer p;
    Character whirl;
    createCombo combo;

    void Start()
    {
        controls = FindObjectOfType<Controls>();
        p = FindObjectOfType<Pointer>();
        whirl = FindObjectOfType<Character>();
        combo = FindObjectOfType<createCombo>();
    }


    private void OnMouseUp()
    {
        if (whirl.GetComponent<Character>().cSpoken)
        {
            combo.GetComponent<comboCheck>().timer = combo.GetComponent<comboCheck>().timeLengthD;
        }

        else if (combo.GetComponent<comboCheck>().timeOn)
        {
            combo.GetComponent<comboCheck>().timer = combo.GetComponent<comboCheck>().timeLengthD;
        }
    }


}
