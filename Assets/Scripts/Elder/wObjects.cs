using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//public class wObjects : MonoBehaviour
public class wObjects : objectTalk
{

    int IdleCounter;

    bool windUp;
    float windTimer;

    Character cRoger;
    createCombo combo;
    lessCombosEnum current;

    private void Start()
    {
        combo = FindObjectOfType<createCombo>();
        cRoger = FindObjectOfType<Character>();
    }

    public void FixedUpdate() //could be useful later
    {
        windTimer++;

        if (windTimer == 200)
        {
            windTimer = 0;
        }

    }

    public override void elderComment()   //add to placeItem & createCombo << integrate back into Character 
    {

        Sprite s = combo.GetComponent<SpriteRenderer>().sprite;

        current = (lessCombosEnum)System.Enum.Parse(typeof(lessCombosEnum), s.name);

        if (s != null)
        {
            //Debug.Log(s.name);

            switch (current)
            {
                case lessCombosEnum.rock_ribbon:
                    if (rock_ribbonCounter[1] == 0)
                    {

                        cRoger.SayBackground("Ah... a pet, perhaps?");
                        //cRoger.Wait(4);
                        rock_ribbonCounter[1] = 1;

                    }
                    break;
                case lessCombosEnum.acorn:
                    if (acornCounter[1] == 0)
                    {

                        cRoger.SayBackground("A squirrel must have buried it...");
                        cRoger.Wait(1.5f);
                        acornCounter[1] = 1;

                    }
                    break;
                case lessCombosEnum.grass:
                    if (grassCounter[1] == 0)
                    {

                        cRoger.SayBackground("Plenty of grass around here");
                        cRoger.Wait(1f);
                        grassCounter[1] = 1;

                    }
                    break;
                case lessCombosEnum.ribbon:
                    if (ribbonCounter[1] == 0)
                    {

                        cRoger.SayBackground("A ribbon... perhaps it belonged to a kite?");
                        ribbonCounter[1] = 1;

                    }
                    break;
                case lessCombosEnum.sock:
                    if (sockCounter[1] == 0)
                    {
                        cRoger.SayBackground("Hmm... no, it's not one of mine");
                        //cRoger.Say("Yours?");
                        sockCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.bottle:
                    if (bottleCounter[1] == 0)
                    {
                        cRoger.SayBackground("Please don't leave that there");
                        bottleCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.sock_rock:
                    if (sock_rockCounter[1] == 0)
                    {
                        cRoger.Say("There's...");
                        sock_rockCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.stocking:
                    if (stockingCounter[1] == 0)
                    {
                        cRoger.Say("Still something at the bottom of the stocking");
                        stockingCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.stocking_ribbon:
                    if (stocking_ribbonCounter[1] == 0)
                    {
                        cRoger.Say("Merry christmas");
                        cRoger.Say("But who is it for?");
                        stocking_ribbonCounter[1] = 1;
                    }
                    else if (stocking_ribbonCounter[1] == 1)
                    {
                        cRoger.Say("Who is it for?");
                        stocking_ribbonCounter[1] = 2;
                    }
                    break;
                case lessCombosEnum.stocking_ribbon_birdBS:
                    if (stocking_ribbon_birdBSCounter[1] == 0)
                    {
                        cRoger.Say("How lovely. A heart-shaped stone");
                        stocking_ribbon_birdBSCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.stocking_ribbon_birdGF:
                    if (stocking_ribbon_birdGFCounter[1] == 0)
                    {
                        cRoger.Say("How thoughtful. A heart-shaped stone");
                        stocking_ribbon_birdGFCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.sock_grass:
                    if (sock_grassCounter[1] == 0)
                    {
                        //cRoger.Say("When did you last...");
                        cRoger.Say("Hmm...");
                        sock_grassCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.pillow:
                    if (pillowCounter[1] == 0)
                    {
                        //cRoger.Say("Lay down on the grass");
                        cRoger.Say("Rest awhile");
                        cRoger.Say("On the soft grass...");
                        //cRoger.Say("You've been working too hard");
                        // cRoger.Say("Lay down your weary head");
                        //cRoger.Say("It's been a long day...");
                        //cRoger.Say("Fall asleep, under the stars?");
                        pillowCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.pillow_ribbon:
                    if (pillow_ribbonCounter[1] == 0)
                    {
                        cRoger.Say("That looks...");
                        pillow_ribbonCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.sushi:
                    if (sushiCounter[1] == 0)
                    {
                        cRoger.Say("Delicious.");
                        sushiCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.grass_feather:
                    if (grass_featherCounter[1] == 0)
                    {
                        cRoger.Say("Ah...");
                        grass_featherCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.birdGF:
                    if (birdGFCounter[1] == 0)
                    {
                        cRoger.Say("One of the birds has landed");
                        birdGFCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.bottle_sock:
                    if (bottle_sockCounter[1] == 0)
                    {
                        cRoger.Say("Ah...");
                        bottle_sockCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.birdBS:
                    if (birdBSCounter[1] == 0)
                    {
                        cRoger.Say("One of the birds has landed");
                        birdBSCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.feather_ribbon:
                    if (feather_ribbonCounter[1] == 0)
                    {
                        cRoger.Say("Oh.");
                        feather_ribbonCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.kite:
                    if (kiteCounter[1] == 0)
                    {
                        cRoger.Say("A kite... with no tether");
                        cRoger.Say("Where will it blow next?");
                        kiteCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.bottle_grass:
                    if (bottle_grassCounter[1] == 0)
                    {
                        cRoger.Say("Oh dear...");
                        bottle_grassCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.plutonium:
                    if (plutoniumCounter[1] == 0)
                    {
                        cRoger.Say("It must have come from the power plant");
                        plutoniumCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.sock_stick:
                    break;
                case lessCombosEnum.tent:
                    if (tentCounter[1] == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("A pity.");
                        cRoger.Say("You can't stay here for long");
                        cRoger.Say("This is private land now");
                        //cRoger.Say("This is private land now, like everywhere else");
                        tentCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.tent_plutonium:
                    if (tent_plutoniumCounter[1] == 0)
                    {
                        cRoger.Say("No-one can stay here...");
                        //cRoger.Say("No-one at all");
                        tent_plutoniumCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.stick_sock:
                    break;
                case lessCombosEnum.bbq:
                    if (bbqCounter[1] == 0)
                    {
                        cRoger.Say("An alternative to meat");
                        bbqCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.bbq_grass:
                    if (bbq_grassCounter[1] == 0)
                    {
                        cRoger.Say("Some seasoning...");
                        bbq_grassCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.birdGF_drum:
                    if (birdGF_drumCounter[1] == 0)
                    {
                        //cRoger.Say("There it goes");
                        cRoger.Say("It flew away...");
                        birdGF_drumCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.birdBS_fire:
                    if (birdBS_fireCounter[1] == 0)
                    {
                        cRoger.Say("How could you?");
                        cRoger.Say("");
                        cRoger.Say("Good for a cold, however");
                        birdBS_fireCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.birdGF_fire:
                    if (birdGF_fireCounter[1] == 0)
                    {
                        cRoger.Say("How could you?");
                        cRoger.Say("");
                        cRoger.Say("Good for a cold, however");
                        birdGF_fireCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.birdGF_fire_bottle:
                    if (birdGF_fire_bottleCounter[1] == 0)
                    {
                        cRoger.Say("People will eat anything");
                        birdGF_fire_bottleCounter[1] = 1;
                    }
                    break;
               
                
                case lessCombosEnum.bottle_feather:
                    break;
                case lessCombosEnum.bottle_ribbon:
                    break;
                case lessCombosEnum.bottle_rock:
                    if (drumCounter[1] == 0)
                    {
                        cRoger.Say("A rudimentary instrument?");
                        drumCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.drum:
                    break;
                case lessCombosEnum.feather:
                    break;
                case lessCombosEnum.feather_acorn:
                    break;
                case lessCombosEnum.stick_rock:
                    break;
                case lessCombosEnum.fire:
                    if (fireCounter[1] == 0)
                    {
                        cRoger.Say("The tinder catches...");
                        cRoger.Say("Even in this wind");
                        fireCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.fire_plutonium:
                    if (fire_plutoniumCounter[1] == 0)
                    {
                        cRoger.Say("A fire that never stops burning");
                        fire_plutoniumCounter[1] = 1;
                    }
                    break;
                
                case lessCombosEnum.food:
                    if (foodCounter[1] == 0)
                    {
                        cRoger.Say("Voila!");
                        //cRoger.Say("When times are hard, eat well");
                        foodCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.grass_acorn:
                    break;
                case lessCombosEnum.grass_ribbon:
                    break;
                case lessCombosEnum.drum_stick:
                    break;
                case lessCombosEnum.lightning:
                    if (lightningCounter[1] == 0)
                    {
                        cRoger.Say("Lightning strikes with no warning");
                        cRoger.Say("The weather is not what it once was");
                        lightningCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.pillow_rock_ribbon:
                    if (pillow_rock_ribbonCounter[1] == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("Shh...");
                        cRoger.Say("It sleeps");
                        pillow_rock_ribbonCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.rock:
                    break;
                case lessCombosEnum.rock_acorn:
                    break;
                case lessCombosEnum.rock_feather:
                    break;
                case lessCombosEnum.kite_plutonium:
                    break;
                case lessCombosEnum.rocket:
                    if (rocketCounter[1] == 0)
                    {
                        cRoger.Say("You can fly far on these winds");
                        cRoger.Say("Farther than you can scarcely imagine");
                        rocketCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.rocket_rock:
                    if (rocket_rockCounter[1] == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("But the seed of life... needs fertile soil");
                        //cRoger.Say("It will wither and die in the void");
                        //cRoger.Say("You can escape from what remains of life");
                        // cRoger.Say("But if you are looking for life");
                        // cRoger.Say("Start here");
                        //cRoger.Say("But we all must come back to earth, eventually");
                        rocket_rockCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.grass_rock:
                    break;
                case lessCombosEnum.grass_stick:
                    break;
                case lessCombosEnum.grass_rock_stick:
                    if (grass_rock_stickCounter[1] == 0)
                    {
                        cRoger.Say("Soon enough, we have an ecosystem");
                        cRoger.Say("Look under the rock...");
                        cRoger.Say("");
                        grass_rock_stickCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.snail:
                    if (snailCounter[1] == 0)
                    {
                        //cRoger.Say("Teeming with life");
                        snailCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.snail_acorn:
                    if (snail_acornCounter[1] == 0)
                    {
                        cRoger.Say("Our little friend has evolved to eat almost anything");
                        snail_acornCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.snail_ribbon:
                    break;
                case lessCombosEnum.snail_ribbon_acorn:
                    if (snail_ribbon_acornCounter[1] == 0)
                    {
                        cRoger.Say("So cute");
                        snail_ribbon_acornCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.snail_bottle:
                    if (snail_bottleCounter[1] == 0)
                    {
                        cRoger.Say("The world seems... smaller than it was before");
                        snail_bottleCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.snail_bottle_acorn:
                    if (snail_bottle_acornCounter[1] == 0)
                    {
                        cRoger.Say("The food doesn't taste as good");
                        snail_bottle_acornCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.snail_bottle_ribbon:
                    break;
                case lessCombosEnum.snail_bottle_ribbon_acorn:
                    if (snail_bottle_ribbon_acornCounter[1] == 0)
                    {
                       // cRoger.Say("");
                       // cRoger.Say("Why do this to yourself?");
                        snail_bottle_ribbon_acornCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.snail_bottle_acorn_ribbon:
                    if (snail_bottle_acorn_ribbonCounter[1] == 0)
                    {
                       // cRoger.Say("");
                       // cRoger.Say("Why do this to yourself?");
                        snail_bottle_acorn_ribbonCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.sock_feather:
                    break;
                case lessCombosEnum.sock_ribbon:
                    break;
                case lessCombosEnum.stick_ribbon:
                    break;
                case lessCombosEnum.catkin:
                    if (catkinCounter[1] == 0)
                    {
                        cRoger.Say("The catkin is flowering");
                        catkinCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.bottle_catkin:
                    if (bottle_catkinCounter[1] == 0)
                    {
                        cRoger.Say("Pretty...");
                        //cRoger.Say("It is contained");
                        bottle_catkinCounter[1] = 1;
                    }
                        break;
                case lessCombosEnum.flowerpot:
                    if (flowerpotCounter[1] == 0)
                    {
                        cRoger.Say("");
                        //cRoger.Say("Oxalis");
                        cRoger.Say("It can grow no further");
                        //cRoger.Say("But where will it go from here");
                        flowerpotCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.catkin_acorn:
                    break;
                case lessCombosEnum.squirrel:
                    if (squirrelCounter[1] == 0)
                    {
                        cRoger.Say("Fruit and nuts will support a population");
                        squirrelCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.squirrel_grass:
                    break;
                case lessCombosEnum.tree:
                    if (treeCounter[1] == 0)
                    {
                        cRoger.Say("Each tree is a house");
                        //cRoger.Say("Each tree houses a population");
                        treeCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.tree_feather:
                    if (tree_featherCounter[1] == 0)
                    {
                        cRoger.Say("For a family");
                        //cRoger.Say("A population... a family");
                        tree_featherCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.tree_feather_sock:
                    if (tree_feather_sockCounter[1] == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("Warmth can be simulated");
                       // cRoger.Say("Life can be contained");

                        // cRoger.Say("We can't save them all");
                        // cRoger.Say("Not all will survive, but we can save...");
                        tree_feather_sockCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.baby:
                    if (babyCounter[1] == 0)
                    {
                       // cRoger.Say("");
                        //cRoger.Say("We can be born...");
                       // cRoger.Say("Alone.");
                        //cRoger.Say("While we are alone");
                        //cRoger.Say("");
                        //cRoger.Say("To what end?");
                        //cRoger.Say("One");
                        babyCounter[1] = 1;
                    }
                    break;
               

                case lessCombosEnum.acorn_ribbon:
                    break;
                case lessCombosEnum.stick:
                    break;
                case lessCombosEnum.stick_acorn:
                    break;
                case lessCombosEnum.stick_bottle:
                    break;
                case lessCombosEnum.stick_feather:
                    break;
                case lessCombosEnum.sushi_tea:
                    if (sushi_teaCounter[1] == 0)
                    {
                        cRoger.Say("Delicious, with a hot cup of tea");
                        sushi_teaCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.bottle_acorn:
                    if (bottle_acornCounter[1] == 0)
                    {
                        cRoger.Say("A nutty...");
                        bottle_acornCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.teapot:
                    if (teapotCounter[1] == 0)
                    {
                        cRoger.Say("Cuppa");
                        teapotCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.teapot_fire:
                    if (teapot_fireCounter[1] == 0)
                    {
                        cRoger.Say("");
                        //cRoger.Say("A pot of tea for two");
                        teapot_fireCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.tea:
                    if (teaCounter[1] == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("Be warm and welcome");
                        //cRoger.Say("Just the ticket");
                        teaCounter[1] = 1;
                    }
                    break;
                case lessCombosEnum.tea_birdGF:
                    if (tea_birdGFCounter[1] == 0)
                    {
                        cRoger.Say("");
                        cRoger.Say("Relax");
                        cRoger.Say("You've earned it");
                        tea_birdGFCounter[1] = 1;
                    }
                    break;
                //case lessCombosEnum.teapot_grass:
                //    if (teapot_grassCounter[1] == 0)
                //    {
                //        cRoger.Say("Let it brew...");
                //        teapot_grassCounter[1] = 1;
                //    }
                //    break;
                default:

                    break;



            }


        }



    }


 
    

}






