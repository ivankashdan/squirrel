using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Dialogue : MonoBehaviour
{
    public List<string> log = new List<string>();
    public List<string> objectives = new List<string>();

    public int CheckLog(string combo)
    {
        int count = 0;
        foreach (string i in log)
        {
            if (i == combo)
            {
                count++;
            }
        }
        return count - 1;
    }

    int hintNumber = 0;

    public void Hint() 
    {
        if (objectives.Count > 0)
        {
            Speech cRoger = FindObjectOfType<Speech>();

            // Increment hintNumber and wrap around if it exceeds the number of objectives
            hintNumber++;
            if (hintNumber >= objectives.Count)
            {
                hintNumber = 0;
            }

            cRoger.Say(objectives[hintNumber]);
        }

    }

    string GetObjective(string item) //WIP
    {
        switch (item)
        {
            case "acorn":
            case "squirrel":
                return "How do I draw out the squirrel?";
            case "ribbon":
            case "kite":
                return "How to make the kite fly?";
            default:
                return "";
        }
    }

    void CheckObjective(string item) //WIP
    {
        if (CheckLog(item) > 0) //if item name is in log
        {
            objectives.Remove(GetObjective(item)); //remove the objective for this item
        }
        else //if item name is not in log
        {
            objectives.Add(GetObjective(item)); //add the objective for this item
        }
    }

    public void ElderComment(string item)
    {
        //checkObjective(item);

        log.Add(item); 
        int c = CheckLog(item);

        Speech cRoger = FindObjectOfType<Speech>();
        if (c == 1)
        {
            switch (item)
            {
                case "present":
                    cRoger.Say("Who is it for?");
                    break;
            }
        }
        else if (c == 0)
        {
            switch (item)
            {
                case "birdGF_ribbon":
                    cRoger.Say("Stunning!");
                    break;
                case "ribbon_rock":
                    cRoger.SayBackground("Ah... a pet, perhaps?");
                    break;
                case "acorn":
                    cRoger.SayBackground("A squirrel must have buried it..."); //
                    break;
                case "grass":
                    cRoger.SayBackground("Plenty of grass around here");
                    break;
                case "ribbon":
                    cRoger.SayBackground("A ribbon... perhaps it belonged to a <color=#ffd5d5>kite</color>?"); //<color=#ff00ffff> </color>
                    break;
                case "sock":
                    cRoger.SayBackground("Hmm... no, it's not one of mine");
                    break;
                case "bottle":
                    cRoger.SayBackground("Please don't leave that there");
                    break;
                case "rock_sock":
                    cRoger.Say("There's...");
                    break;
                case "stocking":
                    cRoger.Say("Still something at the bottom of the stocking");
                    break;
                case "present":
                    cRoger.Say("Merry christmas");
                    cRoger.Say("But who is it for?"); //
                    break;
                case "birdGF_present":
                    cRoger.Say("How thoughtful. A heart-shaped stone");
                    break;
                case "grass_sock":
                    cRoger.Say("Hmm...");
                    break;
                case "pillow":
                    cRoger.Say("Rest awhile");
                    cRoger.Say("On the soft grass...");
                    break;
                case "pillow_ribbon":
                    cRoger.Say("That looks...");
                    break;
                case "sushi":
                    cRoger.Say("Delicious.");
                    break;
                case "feather_grass":
                    cRoger.Say("Ah...");
                    break;
                case "birdGF":
                    cRoger.Say("One of the birds has landed");
                    break;
                case "feather_ribbon":
                    cRoger.Say("Oh.");
                    break;
                case "kite":
                    cRoger.Say("A kite... with no tether");
                    cRoger.Say("Where will it blow next?"); //
                    break;
                case "bottle_grass":
                    cRoger.Say("Oh dear...");
                    break;
                case "plutonium":
                    cRoger.Say("It must have come from the power plant");
                    break;
                case "tent":
                    cRoger.Say("");
                    cRoger.Say("A pity.");
                    cRoger.Say("You can't stay here for long");
                    cRoger.Say("This is private land now");
                    break;
                case "plutonium_tent":
                    cRoger.Say("No-one can stay here...");
                    break;
                case "birdGF_drum":
                    cRoger.Say("It flew away...");
                    break;
                case "birdGF_fire":
                    cRoger.Say("How could you?");
                    cRoger.Say("");
                    cRoger.Say("Good for a cold, however");
                    break;
                case "birdGF_bottle_fire":
                    cRoger.Say("People will eat anything");
                    break;
                case "bottle_rock":
                    cRoger.Say("A rudimentary instrument?");
                    break;
                case "fire":
                    cRoger.Say("The tinder catches...");
                    cRoger.Say("Even in this wind"); ;
                    break;
                case "fire_plutonium":
                    cRoger.Say("A fire that never stops burning");
                    break;
                case "acorn_grass":
                    cRoger.Say("Fruit and nuts will support a population");
                    break;
                case "lightning":
                    cRoger.Say("Lightning strikes with no warning");
                    cRoger.Say("The weather is not what it once was");
                    break;
                case "pillow_ribbon_rock":
                    cRoger.Say("");
                    cRoger.Say("Shh...");
                    cRoger.Say("It sleeps");
                    break;
                case "rocket":
                    cRoger.Say("You can fly far on these winds");
                    cRoger.Say("Farther than you can scarcely imagine");
                    break;
                case "rock_rocket":
                    cRoger.Say("");
                    cRoger.Say("But the seed of life... needs fertile soil");
                    break;
                case "grass_rock_stick":
                    cRoger.Say("Soon enough, we have an ecosystem");
                    cRoger.Say("Look under the rock...");
                    break;
                case "acorn_snail":
                    cRoger.Say("Our little friend has evolved to eat almost anything");
                    break;
                case "acorn_ribbon_snail":
                    cRoger.Say("So cute");
                    break;
                case "bottle_snail":
                    cRoger.Say("The world seems... smaller than it was before"); //
                    break;
                case "acorn_bottle_snail":
                    cRoger.Say("The food doesn't taste as good");
                    break;
                case "catkin":
                    cRoger.Say("The catkin is flowering");
                    break;
                case "bottle_catkin":
                    cRoger.Say("Pretty...");
                    break;
                case "flowerpot":
                    cRoger.Say("");
                    cRoger.Say("It can grow no further"); //
                    break;
                case "tree":
                    cRoger.Say("Each tree is a house");
                    break;
                case "bottle_tree":
                    cRoger.Say("Each house...");
                    break;
                case "earth":
                    cRoger.Say("Is a world");
                    break;
                case "earth_rock":
                    cRoger.Say("There are other houses");
                    cRoger.Say("But there is only one we can call home");
                    //cRoger.Say("An abandoned home");
                    break;
                case "earth_sock":
                    cRoger.Say("");
                    cRoger.Say("It burns...");
                    break;
                case "feather_tree":
                    cRoger.Say("For a family"); //
                    break;
                case "bottle_feather_tree":
                    cRoger.Say("");
                    cRoger.Say("The things we'll eat"); //
                    cRoger.Say("As long as it's in a bottle");
                    //cRoger.Say("Never mind where it comes from");
                    break;
                case "feather_sock_tree":
                    cRoger.Say("");
                    cRoger.Say("Warmth can be simulated"); //
                    break;
                case "sushi_tea":
                    cRoger.Say("Delicious, with a hot cup of tea");
                    break;
                case "acorn_bottle":
                    cRoger.Say("A nutty...");
                    break;
                case "teapot":
                    cRoger.Say("Cuppa");
                    break;
                case "fire_teapot":
                    cRoger.Say("");
                    break;
                case "tea":
                    cRoger.Say("");
                    cRoger.Say("Be warm and welcome");
                    break;
                case "birdGF_tea":
                    cRoger.Say("");
                    cRoger.Say("Relax");
                    cRoger.Say("You've earned it");
                    break;
                default:
                    Debug.Log(item + " dialogue not found");
                    break;
            }

        }

    }
}