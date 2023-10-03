using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokedex : MonoBehaviour
{

    private bool hasRun = false;

    GameObject overlay;

    Sprite[] silhouettes;

    public int rows = 3; // Number of rows
    public int columns = 3; // Number of columns
    public float spacing = 2f; // Spacing between images

    private void Start()
    {

        overlay = transform.GetChild(0).gameObject;
        

        // Calculate the size of the grid cell based on canvas size, rows, columns, and spacing
        RectTransform canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        RectTransform gridRect = GetComponent<RectTransform>();

        // Calculate the cell width and height based on the canvas size
        float cellWidth = (canvasRect.rect.width - (spacing * (columns - 1))) / columns;
        float cellHeight = (canvasRect.rect.height - (spacing * (rows - 1))) / rows;

        // Calculate the top-left corner position based on the canvas size
        float x = -canvasRect.rect.width / 2f + cellWidth / 2f;
        float y = canvasRect.rect.height / 2f - cellHeight / 2f;


       silhouettes = Resources.LoadAll<Sprite>("Hotspots");

       SortandEdit();

       int currentSilhouetteIndex = 0; // Track the current index of the silhouette

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Check if there are more silhouettes to assign
                if (currentSilhouetteIndex < silhouettes.Length)
                {
                    // Create a new Image object 
                    GameObject item = new GameObject("Slot");
                    item.transform.parent = overlay.transform;
                    item.tag = "Found";
                    Image image = item.AddComponent<Image>();

                    // Get its RectTransform for positioning
                    RectTransform elementRect = image.GetComponent<RectTransform>();

                    // Calculate the position based on row, column, and spacing
                    float xPos = col * (cellWidth + spacing) + x;
                    float yPos = -row * (cellHeight + spacing) + y;

                    // Set the position
                    elementRect.anchoredPosition = new Vector2(xPos, yPos);

                    // Assign the current silhouette to the image
                    image.sprite = silhouettes[currentSilhouetteIndex];
                    image.gameObject.name = image.sprite.name; //rename gameobject too

                    // Calculate the aspect ratio of the sprite
                    float aspectRatio = image.sprite.rect.width / image.sprite.rect.height;

                    // Set scaling factors for width and height (adjust these as needed)
                    float widthScaleFactor = 0.7f; // Example scale factor for width
                    float heightScaleFactor = 0.7f; // Example scale factor for height

                    // Calculate the size
                    float newWidth = cellWidth * widthScaleFactor;
                    float newHeight = newWidth / aspectRatio;

                    // Set the size and scale
                    elementRect.sizeDelta = new Vector2(newWidth, newHeight);
                    item.transform.localScale = new Vector3(widthScaleFactor, heightScaleFactor, 1f);

                    currentSilhouetteIndex++; // Move to the next image slot
                }
                else
                {
                    // No more silhouettes to assign, break out of the loop
                    break;
                }
            }
        }

        overlay.SetActive(false); //starting condition
    }


    void SortandEdit()
    {
        List<Sprite> spriteList = new List<Sprite>();
        HashSet<string> addedComboNames = new HashSet<string>();

        foreach (var special in Recipe.ListSpecials())
        {
            spriteList.Add(Resources.Load<Sprite>("Hotspots/" + special));
            addedComboNames.Add(special);

            for (int i = 0; i < silhouettes.Length; i++)
            {
                Sprite combo = silhouettes[i];
                if (combo.name.Contains(special) //must contain the special above, and be a combo but not precombo
                    && combo.name.Contains("_") && !Recipe.IsPreCombo(combo.name))  
                {
                    // Check if this combo name has already been added
                    if (!addedComboNames.Contains(combo.name))
                    {
                        spriteList.Add(combo);
                        addedComboNames.Add(combo.name);
                    }
                }
            }
        }
        silhouettes = spriteList.ToArray();
    }


    private void Update()
    {
        if (overlay.activeSelf && !hasRun)
        {
           
            Dialogue found = FindObjectOfType<Dialogue>();
            GameObject[] slots = GameObject.FindGameObjectsWithTag("Found");
            foreach (string l in found.log)
            {
                foreach (var slot in slots)
                {
                    Image image = slot.GetComponent<Image>();

                    if (l == image.sprite.name)
                    {
                        image.sprite = Resources.Load<Sprite>("Combos/" + l);
                        break;
                    }
                }
            }
            Debug.Log("Refreshed");
            hasRun = true; // Set the flag to true to indicate that it has run
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            overlay.SetActive(!overlay.activeSelf);
            hasRun = false; // Reset the flag when the overlay is deactivated
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            overlay.SetActive(!overlay.activeSelf);
        }
    }




}
