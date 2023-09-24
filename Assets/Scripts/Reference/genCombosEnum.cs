#if UNITY_EDITOR
using System.Collections;
using UnityEngine;
using System.IO;


public class genCombosEnum : MonoBehaviour
{


    [UnityEditor.MenuItem("Tools/Update Counters")]



    public static void Go()
    {
        Character cRoger;
        createCombo combo;
        ArrayList comboNames;
        recipeBook recipe;

        cRoger = FindObjectOfType<Character>();
        combo = FindObjectOfType<createCombo>();
        recipe = FindObjectOfType<recipeBook>();

        comboNames = new ArrayList();

        Object[] combos = Resources.LoadAll("Combos/", typeof(Sprite));

        Debug.Log(combos.Length);


        foreach (Object c in combos)
        {
            comboNames.Add(c.name);
        }

        foreach (string s in recipe.instant) //add instant strings
        {
            comboNames.Add(s);
        }

        comboNames.Sort();

        string enumName = "combosEnum";
        string filePathAndName = "Assets/Scripts/Reference/" + enumName + ".cs";

        using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
        {

            //GenerateHeader();
            streamWriter.WriteLine("using System.Collections;");
            streamWriter.WriteLine("using UnityEngine;");
            streamWriter.WriteLine("using System.IO;");

            Debug.Log(comboNames.Count);


            //GenerateEnum(); 
            for (int i = 0; i < comboNames.Count; i++)
            {

                string n = comboNames[i].ToString();

               
            }

            Debug.Log(comboNames.Count);
            comboNames.Sort();

            streamWriter.WriteLine("public enum " + enumName);
            streamWriter.WriteLine("{");
            for (int i = 0; i < comboNames.Count; i++)
            {
                streamWriter.WriteLine("\t" + comboNames[i] + ",");
            }
            streamWriter.WriteLine("}");
        }




    }

}
#endif