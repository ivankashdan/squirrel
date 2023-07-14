using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class wObjects : MonoBehaviour
{

    Character cRoger;
    placeItem combo;
    combosEnum current; //do I need this eNum? Need to generate currently in order to 

    
    List<string> log = new List<string>();


    private void Start()
    {
        combo = FindObjectOfType<placeItem>();
        cRoger = FindObjectOfType<Character>();
    }

    int checkLog(string combo)
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

    public void elderComment()   // integrate back into Character?
    {

        Sprite s = combo.GetComponent<SpriteRenderer>().sprite;

        current = (combosEnum)System.Enum.Parse(typeof(combosEnum), s.name);


        if (s != null)
        {
            log.Add(s.name);
            int c = checkLog(s.name);

            Debug.Log(c);

           
            switch (current)
            {
                case combosEnum.rock_ribbon:
                    if (c == 0)
                    {
                        cRoger.SayBackground("Ah... a pet, perhaps?");
                    }
                    break;
                case combosEnum.acorn:
                    if (c == 0)
                    {

                        cRoger.SayBackground("A squirrel must have buried it...");
                        cRoger.Wait(1.5f);

                    }
                    break;
                case combosEnum.grass:
                    if (c == 0)
                    {

                        cRoger.SayBackground("Plenty of grass around here");
                        cRoger.Wait(1f);

                    }
                    break;
                case combosEnum.ribbon:
                    if (c == 0)
                    {

                        cRoger.SayBackground("A ribbon... perhaps it belonged to a kite?");

                    }
                    break;
                case combosEnum.sock:
                    if (c == 0)
                    {
                        cRoger.SayBackground("Hmm... no, it's not one of mine");
                    }
                    break;
                case combosEnum.bottle:
                    if (c == 0)
                    {
                        cRoger.SayBackground("Please don't leave that there");
                    }
                    break;
                case combosEnum.sock_rock:
                    if (c == 0)
                    {
                        cRoger.Say("There's...");
                    }
                    break;
                case combosEnum.stocking:
                    if (c == 0)
                    {
                        cRoger.Say("Still something at the bottom of the stocking");
                    }
                    break;
                case combosEnum.stocking_ribbon:
                    if (c == 0)
                    {
                        cRoger.Say("Merry christmas");
                        cRoger.Say("But who is it for?");
                    }
                    else if (c == 1)
                    {
                        cRoger.Say("Who is it for?");
                    }
                    break;
                case combosEnum.stocking_ribbon_birdBS:
                    if (c == 0)
                    {
                        cRoger.Say("How lovely. A heart-shaped stone");
                    }
                    break;
                case combosEnum.stocking_ribbon_birdGF:
                    if (c == 0)
                    {
                        cRoger.Say("How thoughtful. A heart-shaped stone");
                    }
                    break;
                case combosEnum.sock_grass:
                    if (c == 0)
                    {
                        cRoger.Say("Hmm...");
                    }
                    break;
                case combosEnum.pillow:
                    if (c == 0)
                    {
                        cRoger.Say("Rest awhile");
                        cRoger.Say("On the soft grass...");
                    }
                    break;
                case combosEnum.pillow_ribbon:
                    if (c == 0)
                    {
                        cRoger.Say("That looks...");
                    }
                    break;
                case combosEnum.sushi:
                    if (c == 0)
                    {
                        cRoger.Say("Delicious.");
                    }
                    break;
                case combosEnum.grass_feather:
                    if (c == 0)
                    {
                        cRoger.Say("Ah...");
                    }
                    break;
                case combosEnum.birdGF:
                    if (c == 0)
                    {
                        cRoger.Say("One of the birds has landed");
                    }
                    break;
                case combosEnum.bottle_sock:
                    if (c == 0)
                    {
                        cRoger.Say("Ah...");
                    }
                    break;
                case combosEnum.birdBS:
                    if (c == 0)
                    {
                        cRoger.Say("One of the birds has landed");
                    }
                    break;
                case combosEnum.feather_ribbon:
                    if (c == 0)
                    {
                        cRoger.Say("Oh.");
                    }
                    break;
                case combosEnum.kite:
                    if (c == 0)
                    {
                        cRoger.Say("A kite... with no tether");
                        cRoger.Say("Where will it blow next?");
                    }
                    break;
                case combosEnum.bottle_grass:
                    if (c == 0)
                    {
                        cRoger.Say("Oh dear...");
                    }
                    break;
                case combosEnum.plutonium:
                    if (c == 0)
                    {
                        cRoger.Say("It must have come from the power plant");
                    }
                    break;
                case combosEnum.sock_stick:
                    break;
                case combosEnum.tent:
                    if (c == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("A pity.");
                        cRoger.Say("You can't stay here for long");
                        cRoger.Say("This is private land now");
                    }
                    break;
                case combosEnum.tent_plutonium:
                    if (c == 0)
                    {
                        cRoger.Say("No-one can stay here...");
                    }
                    break;
                case combosEnum.stick_sock:
                    break;
                case combosEnum.bbq:
                    if (c == 0)
                    {
                        cRoger.Say("An alternative to meat");
                    }
                    break;
                case combosEnum.bbq_grass:
                    if (c == 0)
                    {
                        cRoger.Say("Some seasoning...");
                    }
                    break;
                case combosEnum.birdGF_drum:
                    if (c == 0)
                    {
                        cRoger.Say("It flew away...");
                    }
                    break;
                case combosEnum.birdBS_fire:
                    if (c == 0)
                    {
                        cRoger.Say("How could you?");
                        cRoger.Say("");
                        cRoger.Say("Good for a cold, however");
                    }
                    break;
                case combosEnum.birdGF_fire:
                    if (c == 0)
                    {
                        cRoger.Say("How could you?");
                        cRoger.Say("");
                        cRoger.Say("Good for a cold, however");
                    }
                    break;
                case combosEnum.birdGF_fire_bottle:
                    if (c == 0)
                    {
                        cRoger.Say("People will eat anything");
                    }
                    break;
                case combosEnum.bottle_feather:
                    break;
                case combosEnum.bottle_ribbon:
                    break;
                case combosEnum.bottle_rock:
                    if (c == 0)
                    {
                        cRoger.Say("A rudimentary instrument?");
                    }
                    break;
                case combosEnum.drum:
                    break;
                case combosEnum.feather:
                    break;
                case combosEnum.feather_acorn:
                    break;
                case combosEnum.stick_rock:
                    break;
                case combosEnum.fire:
                    if (c == 0)
                    {
                        cRoger.Say("The tinder catches...");
                        cRoger.Say("Even in this wind");
                    }
                    break;
                case combosEnum.fire_plutonium:
                    if (c == 0)
                    {
                        cRoger.Say("A fire that never stops burning");
                    }
                    break;
                
                case combosEnum.food:
                    if (c == 0)
                    {
                        cRoger.Say("Voila!");
                    }
                    break;
                case combosEnum.grass_acorn:
                    break;
                case combosEnum.grass_ribbon:
                    break;
                case combosEnum.drum_stick:
                    break;
                case combosEnum.lightning:
                    if (c == 0)
                    {
                        cRoger.Say("Lightning strikes with no warning");
                        cRoger.Say("The weather is not what it once was");
                    }
                    break;
                case combosEnum.pillow_rock_ribbon:
                    if (c == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("Shh...");
                        cRoger.Say("It sleeps");
                    }
                    break;
                case combosEnum.rock:
                    break;
                case combosEnum.rock_acorn:
                    break;
                case combosEnum.rock_feather:
                    break;
                case combosEnum.kite_plutonium:
                    break;
                case combosEnum.rocket:
                    if (c == 0)
                    {
                        cRoger.Say("You can fly far on these winds");
                        cRoger.Say("Farther than you can scarcely imagine");
                    }
                    break;
                case combosEnum.rocket_rock:
                    if (c == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("But the seed of life... needs fertile soil");
                    }
                    break;
                case combosEnum.grass_rock:
                    break;
                case combosEnum.grass_stick:
                    break;
                case combosEnum.grass_rock_stick:
                    if (c == 0)
                    {
                        cRoger.Say("Soon enough, we have an ecosystem");
                        cRoger.Say("Look under the rock...");
                        cRoger.Say("");
                    }
                    break;
                case combosEnum.snail:
                    break;
                case combosEnum.snail_acorn:
                    if (c == 0)
                    {
                        cRoger.Say("Our little friend has evolved to eat almost anything");
                    }
                    break;
                case combosEnum.snail_ribbon:
                    break;
                case combosEnum.snail_ribbon_acorn:
                    if (c == 0)
                    {
                        cRoger.Say("So cute");
                    }
                    break;
                case combosEnum.snail_bottle:
                    if (c == 0)
                    {
                        cRoger.Say("The world seems... smaller than it was before");
                    }
                    break;
                case combosEnum.snail_bottle_acorn:
                    if (c == 0)
                    {
                        cRoger.Say("The food doesn't taste as good");
                    }
                    break;
                case combosEnum.snail_bottle_ribbon:
                    break;
                case combosEnum.snail_bottle_ribbon_acorn:
                    break;
                case combosEnum.snail_bottle_acorn_ribbon:
                    break;
                case combosEnum.sock_feather:
                    break;
                case combosEnum.sock_ribbon:
                    break;
                case combosEnum.stick_ribbon:
                    break;
                case combosEnum.catkin:
                    if (c == 0)
                    {
                        cRoger.Say("The catkin is flowering");
                    }
                    break;
                case combosEnum.bottle_catkin:
                    if (c == 0)
                    {
                        cRoger.Say("Pretty...");
                    }
                        break;
                case combosEnum.flowerpot:
                    if (c == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("It can grow no further");
                    }
                    break;
                case combosEnum.catkin_acorn:
                    break;
                case combosEnum.squirrel:
                    if (c == 0)
                    {
                        cRoger.Say("Fruit and nuts will support a population");
                    }
                    break;
                case combosEnum.squirrel_grass:
                    break;
                case combosEnum.tree:
                    if (c == 0)
                    {
                        cRoger.Say("Each tree is a house");
                    }
                    break;
                case combosEnum.tree_feather:
                    if (c == 0)
                    {
                        cRoger.Say("For a family");
                    }
                    break;
                case combosEnum.tree_feather_sock:
                    if (c == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("Warmth can be simulated");
                    }
                    break;
                case combosEnum.baby:
                    break;
                case combosEnum.acorn_ribbon:
                    break;
                case combosEnum.stick:
                    break;
                case combosEnum.stick_acorn:
                    break;
                case combosEnum.stick_bottle:
                    break;
                case combosEnum.stick_feather:
                    break;
                case combosEnum.sushi_tea:
                    if (c == 0)
                    {
                        cRoger.Say("Delicious, with a hot cup of tea");
                    }
                    break;
                case combosEnum.bottle_acorn:
                    if (c == 0)
                    {
                        cRoger.Say("A nutty...");
                    }
                    break;
                case combosEnum.teapot:
                    if (c == 0)
                    {
                        cRoger.Say("Cuppa");
                    }
                    break;
                case combosEnum.teapot_fire:
                    if (c == 0)
                    {
                        cRoger.Say("");
                    }
                    break;
                case combosEnum.tea:
                    if (c == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("Be warm and welcome");
                    }
                    break;
                case combosEnum.tea_birdGF:
                    if (c == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("Relax");
                        cRoger.Say("You've earned it");
                    }
                    break;
                default:

                    break;



            }


        }



    }


 
    

}






