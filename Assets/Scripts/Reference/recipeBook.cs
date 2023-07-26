using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recipeBook : MonoBehaviour
{
    public Dictionary<combosEnum, combosEnum> recipe = new Dictionary<combosEnum, combosEnum>()
    {
        {combosEnum.sock_stick, combosEnum.tent},
        {combosEnum.bottle_sock, combosEnum.birdBS},
        {combosEnum.feather_grass, combosEnum.birdGF},
        {combosEnum.ribbon_stick, combosEnum.catkin},
        {combosEnum.bottle_rock, combosEnum.drum},
        {combosEnum.rock_stick, combosEnum.fire},
        {combosEnum.bottle_catkin, combosEnum.flowerpot},
        {combosEnum.grass_sock, combosEnum.pillow},
        {combosEnum.bottle_grass, combosEnum.plutonium},
        {combosEnum.rock_sock, combosEnum.stocking},
        {combosEnum.pillow_ribbon, combosEnum.sushi},
        {combosEnum.acorn_bottle, combosEnum.teapot},
        {combosEnum.fire_teapot, combosEnum.tea},
        {combosEnum.feather_ribbon, combosEnum.kite},
        {combosEnum.kite_plutonium, combosEnum.rocket},
        {combosEnum.grass_rock_stick, combosEnum.snail},
        {combosEnum.drum_stick, combosEnum.lightning},
        {combosEnum.acorn_catkin, combosEnum.squirrel},
        {combosEnum.grass_squirrel, combosEnum.tree},
        {combosEnum.feather_sock_tree, combosEnum.baby},
        {combosEnum.birdGF_stick, combosEnum.nest},
        {combosEnum.birdBS_stick, combosEnum.nest},
        {combosEnum.birdGF_nest, combosEnum.chicks},
        {combosEnum.birdBS_nest, combosEnum.chicks},
        {combosEnum.bottle_tree, combosEnum.earth}

        //bird nest

    };
}
