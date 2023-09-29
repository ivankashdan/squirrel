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
        newItem.AddComponent<Item>();

    }

    IEnumerator DelayTransform(string special)
    {

        yield return new WaitForSeconds(0.1f);

        Character character = FindObjectOfType<Character>();
        while (character.toDo.Count > 0)
        {
            yield return null;
        }

        Combo combo = FindObjectOfType<Combo>();
        GameObject existing = combo.transform.GetChild(0).gameObject; 
        Destroy(existing);

        Recipe rb = FindObjectOfType<Recipe>();
        AddItem(special, combo.transform);
    }

    public void SelectItem(GameObject item)
    {

        Destroy(item); //remove item from inventory

        Combo combo = FindObjectOfType<Combo>();

        if (combo.transform.childCount == 0) //place
        {
            AddItem(item.name, combo.transform);
        }
        else //combo
        {

            GameObject existing = combo.transform.GetChild(0).gameObject;
            string comboName = NewComboName(item.name, existing.name);

            Destroy(existing);

            Recipe rb = FindObjectOfType<Recipe>();

            if (Resources.Load("Combos/" + comboName)) //add combo if it exist
            {
                AddItem(comboName, combo.transform);

                string special = rb.GetSpecial(comboName);

                if (special!="") //transform into special if it exist
                {
                    if (FindObjectOfType<cDialogue>().checkLog(special) == -1)
                    {
                        StartCoroutine(DelayTransform(special));
                    }
                    else
                    {
                        //StartCoroutine(DelayTransform(special));
                        StartCoroutine(SlowTransform(comboName));
                    }
                }

            }
            else if (Resources.Load("Combos/" + rb.GetSpecial(comboName)))  //check for instant combo
            {
                AddItem(rb.GetSpecial(comboName), combo.transform);
            }

        }


    }

    public void Unspool(GameObject item)
    {
        Recipe rb = FindObjectOfType<Recipe>();
        if (rb.GetRecipe(item.name) == "")
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

    private IEnumerator SlowTransform(string comboName)
    {
        yield return new WaitForSeconds(1);

        Combo combo = FindObjectOfType<Combo>();
        if (combo.transform.childCount == 0)
        {
            yield break;
        }
        GameObject existing = combo.transform.GetChild(0).gameObject; //need something to protect if move early here
        Destroy(existing);

        Recipe rb = FindObjectOfType<Recipe>();
        AddItem(rb.GetSpecial(comboName), combo.transform);
    }

    private IEnumerator DelaySpool(string item)
    {
        yield return new WaitForSeconds(0.1f);

        Recipe rb = FindObjectOfType<Recipe>();
        string recipe = rb.GetRecipe(item);

        foreach (string part in recipe.Split('_'))
        {
            Distribute(part);

        }
    }


  

}
