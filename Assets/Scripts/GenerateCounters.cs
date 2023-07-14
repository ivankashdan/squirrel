#if UNITY_EDITOR
using System.Collections;
using UnityEngine;
using System.IO;


public class GenerateCounters : MonoBehaviour
{


    [UnityEditor.MenuItem("Tools/Update Counters")]



    public static void Go()
    {
        Character cRoger;
        createCombo combo;
        ArrayList comboNames;

        cRoger = FindObjectOfType<Character>();
        combo = FindObjectOfType<createCombo>();

        comboNames = new ArrayList();

        Object[] combos = Resources.LoadAll("Combos", typeof(Sprite));
        //Object[] items = Resources.LoadAll("Items", typeof(Sprite));

        Debug.Log(combos.Length);

        //foreach (Object c in items)
        //{
        //    comboNames.Add(c.name);
        //}

        foreach (Object c in combos)
        {
            comboNames.Add(c.name);
        }

        

        comboNames.Sort();




        
        string sName = "comboCounters";
        string enumName = "combosEnum";
        string filePathAndName = "Assets/Scripts/" + sName + ".cs";
        string SenumName = "lessCombosEnum";
        // string filePathAndName = "Assets/Scripts/" + enumName + ".cs";

        using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
        {

            //GenerateHeader();
            streamWriter.WriteLine("using System.Collections;");
            streamWriter.WriteLine("using UnityEngine;");
            streamWriter.WriteLine("using System.IO;");


            // GenerateEnum();
            streamWriter.WriteLine("public enum " + enumName);
            streamWriter.WriteLine("{");
            for (int i = 0; i < comboNames.Count; i++)
            {
                streamWriter.WriteLine("\t" + comboNames[i] + ",");
            }
            // streamWriter.WriteLine("\t" + comboNames[comboNames.Count]);
            //Debug.Log(comboNames.Count);
            streamWriter.WriteLine("}");



            //GenerateCounterClass();
            streamWriter.WriteLine("public class " + sName + " : MonoBehaviour");
            streamWriter.WriteLine("{");
            for (int i = 0; i < comboNames.Count; i++)
            {
                //string u = comboNames[i].ToString().Replace("-", "_");
                streamWriter.WriteLine("\t" + "public static int[] " + comboNames[i] + "Counter = new int[30];");

            }
            streamWriter.WriteLine("}");


            Debug.Log(comboNames.Count);
            //GenerateEnum1-7(); 
            for (int i = 0; i < comboNames.Count; i++)
            {

                string n = comboNames[i].ToString();

               
                if (n.Contains("LG"))
                {
                    //Debug.Log(comboNames[i] + " Removed");
                    comboNames.RemoveAt(i);
                    i--;
                }

                 else if (n.Contains("Z"))
                {
                    comboNames.RemoveAt(i);
                    i--;
                }

                 else if (n.Contains("PR"))
                {
                    comboNames.RemoveAt(i);
                    i--;
                }

                 else if (n.Contains("PR2"))
                {
                    comboNames.RemoveAt(i);
                    i--;
                }
            }

            Debug.Log(comboNames.Count);
            comboNames.Sort();

            streamWriter.WriteLine("public enum " + SenumName);
            streamWriter.WriteLine("{");
            for (int i = 0; i < comboNames.Count; i++)
            {
                streamWriter.WriteLine("\t" + comboNames[i] + ",");
            }
            // streamWriter.WriteLine("\t" + comboNames[comboNames.Count]);
            //Debug.Log(comboNames.Count);
            streamWriter.WriteLine("}");
        }




    }

}
#endif