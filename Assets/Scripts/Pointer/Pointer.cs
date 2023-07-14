using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class Pointer : MonoBehaviour
{



    [SerializeField] private Camera mainCamera;


    public Sprite holding;
    public Sprite empty;
    public Sprite hand;
    public Sprite talk;
    public Sprite compass;
    public Sprite magnify;
    public static bool isHolding;
    public bool holdingItem;

    public Sprite savedBlock;
    public bool isBlocking;
    public bool toSavedPos;
    public Vector3 savedPos;
    public Vector3 mouseWorldPosition;


    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    Character whirl;



    void Start()
    {
        whirl = FindObjectOfType<Character>();

        GetComponent<SpriteRenderer>().sprite = empty;

    }


    private void FixedUpdate()
    {
        if (!whirl.cSpoken && !isBlocking)
        {
            GetComponent<SpriteRenderer>().sprite = empty;
        }
    }


    void Update()
    {
        Cursor.visible = false;

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
            else {

                mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPosition.z = 0f;
            }

            transform.position = mouseWorldPosition;


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

    public void cursorOn()
    {
        savedPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        GetComponent<SpriteRenderer>().sprite = null;

        isBlocking = true;
    }

    public void cursorOff() 
    {
        holding = empty;
        toSavedPos = true;
        isBlocking = false;
    }


}
