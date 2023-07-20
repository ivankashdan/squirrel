using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class pVisible : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;


    public Sprite holding;
    public Sprite empty;

    public static bool isHolding;
    public bool holdingItem;

    public bool isBlocking;
    public bool toSavedPos;
    public Vector3 savedPos;
    public Vector3 mouseWorldPosition;

    Character whirl;

    void Start()
    {
        whirl = FindObjectOfType<Character>();

        GetComponent<SpriteRenderer>().sprite = empty;

    }
 

    void Update()
    {


        if (Input.GetKeyUp(KeyCode.Escape))  //Quit function
        {
            Application.Quit();
        }



        Cursor.visible = false; //hardware cursor invisible

        if (isBlocking)
        {

            transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        }

        else
        {
            holdingItem = isHolding;
            if (toSavedPos)
            {

                toSavedPos = false;
            }
            else
            {

                mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPosition.z = 0f;
            }

            transform.position = mouseWorldPosition;


        }


    }

    private void FixedUpdate()
    {
        if (!whirl.cSpoken && !isBlocking)
        {
            GetComponent<SpriteRenderer>().sprite = empty;
        }
    }


    public void toggleCursor()
    {

        if (isBlocking)   //if currently blocking
        {


            holding = empty;

            toSavedPos = true;
            isBlocking = false;


        }
        else   //if not blocking
        {
            savedPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            GetComponent<SpriteRenderer>().sprite = null;



            isBlocking = true;
        }


    }



}
