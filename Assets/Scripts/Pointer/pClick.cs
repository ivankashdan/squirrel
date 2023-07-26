using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pClick : MonoBehaviour
{

	Character whirl;
	createCombo combo;

    private void Start()
    {
		whirl = FindObjectOfType<Character>();
		combo = FindObjectOfType<createCombo>();
	}

    private void OnMouseUp()  //skip timers on mouse click
    {
        if (whirl.GetComponent<Character>().cSpoken)
        {
            combo.GetComponent<checkCombo>().timer = combo.GetComponent<checkCombo>().timeLengthD;
        }

        else if (combo.GetComponent<checkCombo>().timeOn)
        {
            combo.GetComponent<checkCombo>().timer = combo.GetComponent<checkCombo>().timeLengthD;
        }
    }


}