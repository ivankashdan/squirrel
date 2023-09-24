using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static combosEnum;

public class recipeBook : MonoBehaviour
{

    public string[] instant = new string[]
    {
        "sock_stick",
        "rock_stick",
        "kite_plutonium",
        "drum_stick",
        "squirrel_stick"
    };


    public Dictionary<combosEnum, combosEnum> recipe = new Dictionary<combosEnum, combosEnum>()
    {
        {sock_stick, tent},
        {feather_grass, birdGF},
        {ribbon_stick, catkin},
        {bottle_rock, drum},
        {rock_stick, fire},
        {bottle_catkin, flowerpot},
        {grass_sock, pillow},
        {bottle_grass, plutonium},
        {rock_sock, stocking},
        {pillow_ribbon, sushi},
        {acorn_bottle, teapot},
        {fire_teapot, tea},
        {feather_ribbon, kite},
        {kite_plutonium, rocket},
        {grass_rock_stick, snail},
        {drum_stick, lightning},
        {acorn_grass, squirrel},
        {squirrel_stick, tree},
        {feather_sock_tree, baby},
        {bottle_tree, earth}

    };
}
