using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    public bool mouseActive = true; //currently not changed by anything
    Character character;


    private void Start()
    {
        Cursor.visible = false; //turn off hardware mouse
        character = FindObjectOfType<Character>();
    }

    void FollowMouse()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Make sure the object stays at the same z-coordinate

        // Move the object to the mouse position
        transform.position = mousePosition;
    }

    private void Update()
    {
        if (mouseActive)
        {
            if (character.clickContinue)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                FollowMouse();
            }

        }
        if (Input.GetMouseButtonDown(0))
        {

            if (character.clickable)
            {
                FindObjectOfType<Character>().clickThroughDialogue();
            }

            Actions actions = FindObjectOfType<Actions>();
            if (actions.slowTransform != null) 
            {
                actions.skip = true;
                //actions.SkipTransform();    //<< need to come up with better way of doing this
            }
        }
    }

   









}
