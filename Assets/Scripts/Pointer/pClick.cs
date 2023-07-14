using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pClick : MonoBehaviour
{

	Character whirl;
	placeItem combo;

    private void Start()
    {
		whirl = FindObjectOfType<Character>();
		combo = FindObjectOfType<placeItem>();
	}

    private void OnMouseUp()  //skip timers on mouse click
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