using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mouse : MonoBehaviour
{

    public bool mouseActive = true; //currently not changed by anything

    private SpriteRenderer spriteRenderer;
    private Sprite originalPointer;
    public Sprite itemPointer;

    Speech speech;
    Actions actions;

    TMP_Text actionText;

    Vector2 mousePosition;
    RaycastHit2D hit;
    RaycastHit2D hitItem;

    private void Start()
    {
        Cursor.visible = false; //turn off hardware mouse

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPointer = spriteRenderer.sprite;

        speech = FindObjectOfType<Speech>();
        actions = FindObjectOfType<Actions>();

        actionText = GameObject.FindWithTag("actionText").GetComponent<TMP_Text>();
        actionText.text = "";
    }

    void FollowMouse()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    private void Update()
    {
        if (!mouseActive)
            return;

        if (speech.clickContinue)
        {
            actionText.text = "Press 'LMB' to continue";

            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (speech.clickable)
            {
                if (Input.GetMouseButtonDown(0)) 
                {
                    speech.clickThroughDialogue();
                }
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;

            FollowMouse();

            if (actions.slowTransform != null) //need to update this so it works again...
            {
                actionText.text = ""; ////during a slow transform

                if (Input.GetMouseButtonDown(0))
                {
                    actions.skip = true;
                }
            }
            // Check if the mouse is over a collider
            else if (IsMouseOverCollider())
            {
                if (hit.collider.tag == "Combo")
                {
                    if (hit.collider.transform.childCount > 0)
                    {
                        actionText.text = "Press 'LMB' to return";
                    }
                    else
                    {
                        actionText.text = ""; //nothing in combo
                    }
                }
                else if (hit.collider.tag == "Inventory")
                {
                    actionText.text = "Press 'LMB' to select";
                }
                if (IsMouseOverItemCollider())
                {
                    spriteRenderer.sprite = itemPointer;

                    if (hitItem.collider.transform.parent.tag == "Slot")
                    {
                        if (Recipe.GetRecipe(hitItem.collider.name) != "")
                        {
                            actionText.text = "Press 'LMB' to select / 'RMB' to unspool";
                        }
                        if (Input.GetMouseButtonDown(0))
                        {
                            actions.SelectItem(hitItem.collider.gameObject);
                        }
                        else if (Input.GetMouseButtonDown(1))
                        {
                            actions.Unspool(hitItem.collider.gameObject);
                        }
                    }
                    else if (hitItem.collider.transform.parent.tag == "Combo")
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            actions.Return(hitItem.collider.gameObject);
                        }
                    }
                }
                else
                {
                    spriteRenderer.sprite = originalPointer;
                }
            }
            else
            {
                spriteRenderer.sprite = originalPointer;
                actionText.text = "";
            }
        }
    }

    bool IsMouseOverCollider()
    {
        hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        return hit.collider != null;
    }

    bool IsMouseOverItemCollider()
    {
        hitItem = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Items"));
        return hitItem.collider != null;
    }
}
