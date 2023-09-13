using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor.Experimental.GraphView;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class genConst : MonoBehaviour
{

    Sprite[] combos;

    float zoomSens = 0.5f;

    float interval = 0.4f;

    float wPos = 0;
    float yPos = 0;




    List<Sprite> sCombos = new List<Sprite>();

    private void Start()
    {

        combos = Resources.LoadAll("Combos", typeof (Sprite)).Cast<Sprite>().ToArray();

        List<string> names = new List<string>();
    
        foreach (Sprite s in combos) 
        { 
        names.Add(s.name);
        }

        names.Sort();  //alphabetize sprites before population


        foreach (string n in names)
        {
            sCombos.Add(Resources.Load("Combos/" + n, typeof(Sprite)) as Sprite);
        }



        foreach (Sprite c in sCombos)  //first pass - populate
        {

            GameObject newItem = new GameObject(c.name);
            newItem.AddComponent<SpriteRenderer>().sprite = c;
            newItem.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            newItem.AddComponent<PolygonCollider2D>();
            newItem.AddComponent<itemValues>();

            newItem.transform.parent = gameObject.transform;

            newItem.transform.localPosition = new Vector3(wPos, yPos, 1);

            newItem.transform.localScale = new Vector3(1, 1, 1);
            
            if (wPos >= (10 * interval))
            {
                wPos = 0;
                yPos -= interval;
            }
            else
            {
                wPos += interval;
            }
            
            //Debug.Log(c.name);
        }

     

        


        //secondPass();

        // drawLine();

        //Camera.main.orthographicSize = 0.005f;


    }


    void drawLine()
    {


        GameObject line = new GameObject("Line");    //draw line
        line.transform.parent = gameObject.transform;
        line.transform.localPosition = new Vector3(0, 0, 0);
        line.transform.localScale = new Vector3(1, 1, 1);
        line.AddComponent<LineRenderer>().positionCount = sCombos.Count;

        LineRenderer l = line.GetComponent<LineRenderer>();
        l.sortingLayerName = "Behind";

        //float lWidth = 0.00005f; 
        float lWidth = 0.05f;
        Color lColour = Color.black;

        l.startColor = lColour;
        l.endColor = lColour;
        l.startWidth = lWidth;
        l.endWidth = lWidth;
        l.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));


        for (int i = 0; i < l.positionCount; i++)
        {
            Vector3 comboLoc = line.transform.parent.GetChild(i).transform.localPosition;

            l.SetPosition(i, new Vector3(comboLoc.x, comboLoc.y, comboLoc.z));

        }
    }



    void updatePos(itemValues.comboType type) 
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<itemValues>().cType == type)
            {
                transform.GetChild(i).transform.localPosition = new Vector3(wPos, yPos, 1);

                if (wPos >= (10 * interval))
                {
                    wPos = 0;
                    yPos -= interval;
                }
                else
                {
                    wPos += interval;
                }

            }

          

        }

        wPos = 0;      //break between
        yPos -= 2*interval;

    }

    bool posUpdated = false;



    List<combosEnum> starterList = new List<combosEnum>()
    {
        combosEnum.acorn,
        combosEnum.bottle,
        combosEnum.feather,
        combosEnum.grass,
        combosEnum.ribbon,
        combosEnum.rock,
        combosEnum.sock,
        combosEnum.stick

    };


    List<combosEnum> inaccessibleList = new List<combosEnum>();

    private void Update()
    {

        
        if (posUpdated == false) //second pass
        {

            wPos = 0;
            yPos = 0;
            //updatePos(itemValues.comboType.starter);

            //updatePos(itemValues.comboType.special);
            //updatePos(itemValues.comboType.combo);
            //updatePos(itemValues.comboType.preSpecial);


            for (int i = 0; i < transform.childCount; i++) //check for broken specials
            {

                if (transform.GetChild(i).GetComponent<itemValues>().cType == itemValues.comboType.starter)
                {
                    bool foundAnywhere = false;

                    foreach (var s in starterList)
                    {

                        if((combosEnum)System.Enum.Parse(typeof(combosEnum), transform.GetChild(i).transform.name)  == s)
                        {
                            foundAnywhere = true;
                        }

                    }

                    if (foundAnywhere == false)
                    {
                        Debug.Log(transform.GetChild(i).gameObject.name + " inaccessible special");
                        transform.GetChild(i).GetComponent<itemValues>().cType = itemValues.comboType.inaccessible;
                        inaccessibleList.Add((combosEnum)System.Enum.Parse(typeof(combosEnum), transform.GetChild(i).gameObject.name));
                    }

                }

            }


            for (int i = 0; i < transform.childCount; i++) //check for broken combos
            {

                if (transform.GetChild(i).GetComponent<itemValues>().cType == itemValues.comboType.combo)
                {
                    bool foundInaccessibleComponent= false;

                    foreach (var c in transform.GetChild(i).GetComponent<itemValues>().components) // for each of combos components
                    {

                        if (inaccessibleList.Contains(c))
                        {
                            foundInaccessibleComponent = true;
                        }

                    }
                    if (foundInaccessibleComponent)
                    {
                        Debug.Log(transform.GetChild(i).gameObject.name + " inaccessible combo");
                        transform.GetChild(i).GetComponent<itemValues>().cType = itemValues.comboType.inaccessible;
                        inaccessibleList.Add((combosEnum)System.Enum.Parse(typeof(combosEnum), transform.GetChild(i).gameObject.name));
                    }


                    //for (int ii = 0; ii < transform.childCount; ii++)
                    //{

                    //    foreach (var c in transform.GetChild(i).GetComponent<itemValues>().components) // for each of combos components, check all combos
                    //    {

                    //        if (transform.GetChild(ii).transform.name == c.ToString())  //if checked combo's name is the combo component
                    //        {
                    //            //Debug.Log(transform.GetChild(ii).transform.name + " contains " + c.ToString()); 
                             
                    //            if (transform.GetChild(ii).GetComponent<itemValues>().cType != itemValues.comboType.inaccessible)  //check if checked combo's type is inaccessible
                    //            {
                                   

                    //                foundAnywhere = true;
                    //            }

                    //        }
                    //    }

                    //}
                    //if (foundAnywhere == false)
                    //{
                    //    Debug.Log(transform.GetChild(i).gameObject.name + " inaccessible combo");
                    //    transform.GetChild(i).GetComponent<itemValues>().cType = itemValues.comboType.inaccessible;
                    //    //Destroy(transform.GetChild(i).gameObject);
                    //}

                }



            }



            for (int s = 0; s < 20; s++) //second pass
            {
                for (int i = 0; i < transform.childCount; i++)
                {


                    if (transform.GetChild(i).GetComponent<itemValues>().cType == itemValues.comboType.preSpecial 
                        ||transform.GetChild(i).GetComponent<itemValues>().cType == itemValues.comboType.inaccessible
                        )
                    {
                        //Destroy(transform.GetChild(i).gameObject);

                    }

                    else
                    {

                        if (transform.GetChild(i).GetComponent<itemValues>().stage == s)
                        {
                            transform.GetChild(i).transform.localPosition = new Vector3(wPos, yPos, 1);

                            if (wPos >= (10 * interval))
                            {
                                wPos = 0;
                                yPos -= interval;
                            }
                            else
                            {
                                wPos += interval;
                            }

                        }

                    }


                }

            }

            wPos = 0;      //break between
            yPos -= interval;

            updatePos(itemValues.comboType.inaccessible);
            updatePos(itemValues.comboType.preSpecial);


            posUpdated = true;

        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            Camera.main.orthographicSize += zoomSens;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (Camera.main.orthographicSize > zoomSens)
                Camera.main.orthographicSize -= zoomSens;
        }





        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Camera cam = FindObjectOfType<Camera>();
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x + zoomSens, cam.transform.localPosition.y, cam.transform.localPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Camera cam = FindObjectOfType<Camera>();
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x - zoomSens, cam.transform.localPosition.y, cam.transform.localPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Camera cam = FindObjectOfType<Camera>();
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y + zoomSens, cam.transform.localPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Camera cam = FindObjectOfType<Camera>();
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y - zoomSens, cam.transform.localPosition.z);
        }
        
    }



    void secondPass()
    {
        for (int i = 0; i < transform.childCount; i++) //second pass   << all this second pass stuff crashes compute. Better off only generating once. Write logic first
        {
            if (transform.GetChild(i).name.Contains("_")) //find combo
            {
                string[] components = transform.GetChild(i).name.Split('_');

                for (int ii = 0; ii < transform.childCount; ii++)
                {
                    foreach (string comp in components)
                    {
                        if (transform.GetChild(ii).name == comp)
                        {
                            float newX = transform.GetChild(ii).transform.localPosition.x;     //position it in same x as component
                            float newY = transform.GetChild(ii).transform.localPosition.y
                                // + transform.GetChild(ii).GetComponent<itemValues>().stacked
                                ;  //postiion it in same y as component + itemvalue;
                                   // transform.GetChild(ii).GetComponent<itemValues>().stacked++;

                            GameObject newItem = Instantiate(transform.GetChild(i).gameObject);
                            newItem.name = newItem.GetComponent<SpriteRenderer>().sprite.name;
                            newItem.transform.parent = gameObject.transform;
                            newItem.transform.localPosition = new Vector3(newX, newY, 1);
                            newItem.transform.localScale = new Vector3(1, 1, 1);

                        }


                    }

                }


                Destroy(transform.GetChild(i).gameObject);
            }
        }

    }

}
