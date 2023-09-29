using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    public bool mouseActive = true; //currently not changed by anything

    public bool clickContinue = false;

    private void Start()
    {
        Cursor.visible = false; //turn off hardware mouse
    }

    void FollowMouse()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Make sure the object stays at the same z-coordinate

        // Move the object to the mouse position
        transform.position = mousePosition;
    }


    public void clickContinueOn()
    {
        clickContinue = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        foreach (var slot in slots)
        {
            slot.SetActive(false);

        }

    }

    public void clickContinueOff()
    {
        clickContinue = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        foreach (var slot in slots)
        {
            slot.SetActive(true);

        }

    }


    private void Update()
    {
        if (mouseActive && !clickContinue)       
            FollowMouse();

        if (Input.GetMouseButtonDown(0))
        {
            if (clickContinue)
            {
                FindObjectOfType<Character>().clickThroughDialogue();
            }
        }
    }

   









}
