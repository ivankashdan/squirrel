using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    public bool mouseActive = true; //currently not changed by anything

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

    private void Update()
    {
        if (mouseActive)       
            FollowMouse();

    }











}
