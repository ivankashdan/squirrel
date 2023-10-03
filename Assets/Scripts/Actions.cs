using System.Collections;
using System.Collections.Generic;
using System.Linq; //for 'ToList()'
using UnityEngine;

public class Actions : MonoBehaviour
{
   
    public void AddItem(string name, Transform destination)
    {
        GameObject newItem = new GameObject(name);
        newItem.transform.parent = destination;
        newItem.layer = LayerMask.NameToLayer("Items");
        newItem.AddComponent<Item>();

    }

    IEnumerator DelayTransform(string special)
    {
        yield return new WaitForSeconds(0.1f);

        Speech speech = FindObjectOfType<Speech>();
        while (speech.toDo.Count > 0)
        {
            yield return null;
        }

        GameObject combo = GameObject.FindWithTag("Combo");
        GameObject existing = combo.transform.GetChild(0).gameObject; 
        Destroy(existing);

        AddItem(special, combo.transform);
    }

    public void SelectItem(GameObject item)
    {

        Destroy(item); //remove item from inventory

        GameObject combo = GameObject.FindWithTag("Combo");

        if (combo.transform.childCount == 0) //place
        {
            AddItem(item.name, combo.transform);
        }
        else //combo
        {

            GameObject existing = combo.transform.GetChild(0).gameObject;
            string comboName = NewComboName(item.name, existing.name);

            Destroy(existing);

            if (Resources.Load("Combos/" + comboName)) //add combo if it exist
            {
                string special = Recipe.GetSpecial(comboName);
                AddItem(comboName, combo.transform);

                if (special!="") //transform into special if it exist
                {
                    Speech speech = FindObjectOfType<Speech>();
                    if (FindObjectOfType<Dialogue>().checkLog(special) == -1 && speech.toDo.Count > 0)
                    {
                        StartCoroutine(DelayTransform(special));
                    }
                    else
                    {
                        slowTransform = StartCoroutine(SlowTransform(special));
                    }
                }

            }
            else if (Resources.Load("Combos/" + Recipe.GetSpecial(comboName)))  //check for instant combo
            {
                AddItem(Recipe.GetSpecial(comboName), combo.transform);
            }

        }


    }

    public void Unspool(GameObject item)
    {
        if (Recipe.GetRecipe(item.name) == "")
        {
            return;
        }
        else
        {
            Destroy(item);

            StartCoroutine(DelaySpool(item.name)); 

        }
    }

    public void Return(GameObject item)
    {

        Destroy(item); //remove item from combo

        if (item.name.Contains('_'))
        {
            foreach (string part in item.name.Split('_')) //return combo parts to inventory
            {
                Distribute(part);
            }
        }
        else
        {
            Distribute(item.name); //return lone item/special to inventory
        } 

    }

    void Distribute(string item)
    {

        Inventory inv = FindObjectOfType<Inventory>();

        for (int i = 0; i < inv.transform.childCount; i++) //check left to right for empty slots
        {
            Transform slot = inv.transform.GetChild(i);

            if (slot.transform.childCount == 0)
            {
                AddItem(item, slot); //put in empty slot
                return;
            }


        }

    }

    public string NewComboName(string addition, string original)
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

    public Coroutine slowTransform;
    public bool skip = false; 
   
    private IEnumerator SlowTransform(string special)
    {

        Inventory inv = FindObjectOfType<Inventory>();
        inv.ToggleInvVisible(false);

        yield return new WaitForSeconds(0.1f);

        skip = false; //allow to skip wait if there is a mouse click
        float startTime = Time.time;
        while (!skip && Time.time - startTime < 1f)  
        {
            yield return null;
        }
        skip = false;
        slowTransform = null;


        GameObject combo = GameObject.FindWithTag("Combo");
        if (combo.transform.childCount == 0)
        {
            yield break;
        }
        GameObject existing = combo.transform.GetChild(0).gameObject; //need something to protect if move early here
        Destroy(existing);

        AddItem(special, combo.transform);

        yield return new WaitForSeconds(0.1f);

        inv.ToggleInvVisible(true);
    }

    public void SkipTransform(string special)
    {
        GameObject combo = GameObject.FindWithTag("Combo");  //could replace above
        GameObject existing = combo.transform.GetChild(0).gameObject; //need something to protect if move early here
        Destroy(existing);

        AddItem(special, combo.transform);
    }
    private IEnumerator DelaySpool(string item)
    {
        yield return new WaitForSeconds(0.1f);

        string recipe = Recipe.GetRecipe(item);

        foreach (string part in recipe.Split('_'))
        {
            Distribute(part);

        }
    }


  

}
