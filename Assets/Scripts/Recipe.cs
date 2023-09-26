using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{

    public Dictionary<string, string> recipe = new Dictionary<string, string>()
    {
       
        {"sock_stick", "tent"},
        {"feather_grass", "birdGF"},
        {"ribbon_stick", "catkin"},
        {"bottle_rock", "drum" }
        //{rock_stick, fire},
        //{bottle_catkin, flowerpot},
        //{grass_sock, pillow},
        //{bottle_grass, plutonium},
        //{rock_sock, stocking},
        //{pillow_ribbon, sushi},
        //{acorn_bottle, teapot},
        //{fire_teapot, tea},
        //{feather_ribbon, kite},
        //{kite_plutonium, rocket},
        //{grass_rock_stick, snail},
        //{drum_stick, lightning},
        //{acorn_grass, squirrel},
        //{squirrel_stick, tree},
        //{feather_sock_tree, baby},
        //{bottle_tree, earth}

    };


    public string GetSpecial(string key)
    {


        if (recipe.ContainsKey(key))
        {
            foreach (var r in recipe)
            {
                if (r.Key == key)
                {
                    return r.Value;
                }
            }

        }
        return "";
    }

    public string GetRecipe(string value)
    {

        if (recipe.ContainsValue(value))
        {
            foreach (var r in recipe)
            {
                if (r.Value == value)
                {
                    return r.Key.ToString();
                }
            }

        }
        return "";
    }

}
