using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{

    public static Dictionary<string, string> recipe = new Dictionary<string, string>()
    {
       
        {"sock_stick", "tent"},
        {"feather_grass", "birdGF"},
        {"ribbon_stick", "catkin"},
        {"bottle_rock", "drum"},
        {"rock_stick", "fire"},
        {"bottle_catkin", "flowerpot"},
        {"grass_sock","pillow"},
        {"bottle_grass", "plutonium"},
        {"rock_sock", "stocking" },
        {"pillow_ribbon", "sushi"},
        {"acorn_bottle", "teapot"},
        {"fire_teapot", "tea"},
        {"feather_ribbon", "kite"},
        {"kite_plutonium", "rocket" },
        {"grass_rock_stick", "snail" },
        {"drum_stick", "lightning"},
        {"acorn_grass", "squirrel"},
        {"squirrel_stick", "tree"},
        {"feather_sock_tree", "baby"},
        {"bottle_tree", "earth"}
    };


    public static List<string> ListStarters()
    {
        List<string> starters = new List<string>();
        //WIP
        return starters;
    }

    public static List<string> ListSpecials()
    {
        List<string> specials = new List<string>();

        foreach (var r in recipe)
        {
            specials.Add(r.Value);

        }

        specials.Sort((a, b) => a.CompareTo(b)); //sort alphabetically

        return specials;
    }

    public static bool ContainsSpecial(string combo)
    {
        string[] parts = combo.Split("_");

        foreach (var p in parts)
        {
            if (IsSpecial(p))
            {
                return true;
            }
        }
        return false;

    }

    public static bool IsSpecial(string item)
    {
        if (recipe.ContainsValue(item))
        {
            return true;
        }
        return false;
    }

    public static bool IsPreCombo(string item)
    {
        if (recipe.ContainsKey(item))
        {
            return true;
        }
        return false;
    }

    public static string GetSpecial(string key)
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

    public static string GetRecipe(string value)
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
