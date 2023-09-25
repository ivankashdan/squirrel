using System.Collections;
using System.Collections.Generic;
using System.Linq; //for 'ToList()'
using UnityEngine;

public class Actions : MonoBehaviour
{

    public string newComboName(string addition, string original)
    {

        List<string> parts = new List<string>();

        if (original.Contains("_"))
        {
            parts = original.Split(char.Parse("_")).ToList();
        }
        else
        {
            parts.Add(original);
        }

        parts.Add(addition);
        parts.Sort();
        string name = "";

        foreach (string p in parts)
        {
            if (name != "")
                name += "_";
            name += p;
        }

        return name;

    }



    void addItem(string name, Transform destination)
    {
        GameObject newItem = new GameObject(name);
        newItem.transform.parent = destination;
        newItem.AddComponent<Item>();
    }

    public string getSpecial(string key)
    {

        Recipe rb = FindObjectOfType<Recipe>();

        if (rb.recipe.ContainsKey(key))
        {
            foreach (var r in rb.recipe)
            {
                if (r.Key == key)
                {
                    return r.Value;
                }
            }

        }
        return "";
    }




    private IEnumerator Transform (string comboName)
    {
        yield return new WaitForSeconds(1);

        Debug.Log("co-routine completed");
        Combo combo = FindObjectOfType<Combo>();
        GameObject existing = combo.transform.GetChild(0).gameObject; //need something to protect if move early here
        Destroy(existing);

        addItem(getSpecial(comboName), combo.transform);
    }

    public void SelectItem(GameObject item)
    {

        Destroy(item); //remove item from inventory

        Combo combo = FindObjectOfType<Combo>();

        if (combo.transform.childCount == 0) //place
        {
            addItem(item.name, combo.transform);
        }
        else //combo
        {

            GameObject existing = combo.transform.GetChild(0).gameObject;
            string comboName = newComboName(item.name, existing.name);

            Destroy(existing);

            
            if (Resources.Load("Combos/" + comboName)) //add combo if it exist
            {

                addItem(comboName, combo.transform);

                if (getSpecial(comboName)!="") //transform into special if it exist
                {
                    StartCoroutine(Transform(comboName));
                }

            }
            else if (Resources.Load("Combos/" + getSpecial(comboName)))  //check for instant combo
            {
                addItem(getSpecial(comboName), combo.transform);
            }

        }


    }


    public string getRecipe(string value)
    {
        Recipe rb = FindObjectOfType<Recipe>();

        if (rb.recipe.ContainsValue(value))
        {
            foreach (var r in rb.recipe)
            {
                if (r.Value == value)
                {
                    return r.Key.ToString();
                }
            }

        }
        return "";
    }


    private IEnumerator DelaySpool (string recipe)
    {
        yield return new WaitForSeconds(0.1f);

        foreach (string part in recipe.Split('_'))
        {
            Inventory inv = FindObjectOfType<Inventory>();

            for (int i = 0; i < inv.transform.childCount; i++) //check left to right for empty slots
            {
                Transform slot = inv.transform.GetChild(i);

                if (slot.transform.childCount == 0)
                {
                    GameObject newItem = new GameObject(part); //put in empty slot
                    newItem.transform.parent = slot.transform;
                    newItem.AddComponent<Item>();
                    break;
                }


            }


        }
    }

    void Distribute(string item)
    {
        
    }

    public void Unspool(GameObject item)
    {

        if (getRecipe(item.name) == "")
        {
            return;
        }
        else
        {
            Destroy(item);
            
            string recipe = getRecipe(item.name);

            StartCoroutine(DelaySpool(recipe)); 

        }
    }


   


    public void Return(GameObject item)
    {
        Destroy(item); //remove item from combo

        Inventory inv = FindObjectOfType<Inventory>();



        for (int i = 0; i < inv.transform.childCount; i++) //check left to right for empty slots
        {
            Transform slot = inv.transform.GetChild(i);

            if (slot.transform.childCount == 0)
            {
                GameObject newItem = new GameObject(item.name); //put in empty slot
                newItem.transform.parent = slot.transform;
                newItem.AddComponent<Item>();
                return;
            }


        }


    }
    

}
