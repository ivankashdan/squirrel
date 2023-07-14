using System.Collections;
using UnityEngine;
using System.IO;
public enum combosEnum
{
	acorn,
	acorn_fire,
	acorn_ribbon,
	baby,
	bbq,
	bbq_grass,
	birdBS,
	birdBS_drum,
	birdBS_fire,
	birdBS_fire_bottle,
	birdGF,
	birdGF_drum,
	birdGF_fire,
	birdGF_fire_bottle,
	bottle,
	bottle_acorn,
	bottle_catkin,
	bottle_feather,
	bottle_grass,
	bottle_ribbon,
	bottle_rock,
	bottle_sock,
	catkin,
	catkin_acorn,
	drum,
	drum_stick,
	feather,
	feather_acorn,
	feather_ribbon,
	fire,
	fire_plutonium,
	flowerpot,
	food,
	grass,
	grass_acorn,
	grass_feather,
	grass_ribbon,
	grass_rock,
	grass_rock_stick,
	grass_stick,
	kite,
	kite_plutonium,
	lightning,
	pillow,
	pillow_ribbon,
	pillow_rock_ribbon,
	plutonium,
	ribbon,
	rock,
	rock_acorn,
	rock_feather,
	rock_ribbon,
	rocket,
	rocket_rock,
	snail,
	snail_acorn,
	snail_bottle,
	snail_bottle_acorn,
	snail_bottle_acorn_ribbon,
	snail_bottle_ribbon,
	snail_bottle_ribbon_acorn,
	snail_ribbon,
	snail_ribbon_acorn,
	sock,
	sock_feather,
	sock_grass,
	sock_ribbon,
	sock_rock,
	sock_stick,
	squirrel,
	squirrel_grass,
	stick,
	stick_acorn,
	stick_bottle,
	stick_feather,
	stick_ribbon,
	stick_rock,
	stick_sock,
	stocking,
	stocking_ribbon,
	stocking_ribbon_birdBS,
	stocking_ribbon_birdGF,
	sushi,
	sushi_tea,
	tea,
	tea_birdGF,
	teapot,
	teapot_fire,
	tent,
	tent_plutonium,
	tree,
	tree_feather,
	tree_feather_sock,
}
public class comboCounters : MonoBehaviour
{
	public static int[] acornCounter = new int[30];
	public static int[] acorn_fireCounter = new int[30];
	public static int[] acorn_ribbonCounter = new int[30];
	public static int[] babyCounter = new int[30];
	public static int[] bbqCounter = new int[30];
	public static int[] bbq_grassCounter = new int[30];
	public static int[] birdBSCounter = new int[30];
	public static int[] birdBS_drumCounter = new int[30];
	public static int[] birdBS_fireCounter = new int[30];
	public static int[] birdBS_fire_bottleCounter = new int[30];
	public static int[] birdGFCounter = new int[30];
	public static int[] birdGF_drumCounter = new int[30];
	public static int[] birdGF_fireCounter = new int[30];
	public static int[] birdGF_fire_bottleCounter = new int[30];
	public static int[] bottleCounter = new int[30];
	public static int[] bottle_acornCounter = new int[30];
	public static int[] bottle_catkinCounter = new int[30];
	public static int[] bottle_featherCounter = new int[30];
	public static int[] bottle_grassCounter = new int[30];
	public static int[] bottle_ribbonCounter = new int[30];
	public static int[] bottle_rockCounter = new int[30];
	public static int[] bottle_sockCounter = new int[30];
	public static int[] catkinCounter = new int[30];
	public static int[] catkin_acornCounter = new int[30];
	public static int[] drumCounter = new int[30];
	public static int[] drum_stickCounter = new int[30];
	public static int[] featherCounter = new int[30];
	public static int[] feather_acornCounter = new int[30];
	public static int[] feather_ribbonCounter = new int[30];
	public static int[] fireCounter = new int[30];
	public static int[] fire_plutoniumCounter = new int[30];
	public static int[] flowerpotCounter = new int[30];
	public static int[] foodCounter = new int[30];
	public static int[] grassCounter = new int[30];
	public static int[] grass_acornCounter = new int[30];
	public static int[] grass_featherCounter = new int[30];
	public static int[] grass_ribbonCounter = new int[30];
	public static int[] grass_rockCounter = new int[30];
	public static int[] grass_rock_stickCounter = new int[30];
	public static int[] grass_stickCounter = new int[30];
	public static int[] kiteCounter = new int[30];
	public static int[] kite_plutoniumCounter = new int[30];
	public static int[] lightningCounter = new int[30];
	public static int[] pillowCounter = new int[30];
	public static int[] pillow_ribbonCounter = new int[30];
	public static int[] pillow_rock_ribbonCounter = new int[30];
	public static int[] plutoniumCounter = new int[30];
	public static int[] ribbonCounter = new int[30];
	public static int[] rockCounter = new int[30];
	public static int[] rock_acornCounter = new int[30];
	public static int[] rock_featherCounter = new int[30];
	public static int[] rock_ribbonCounter = new int[30];
	public static int[] rocketCounter = new int[30];
	public static int[] rocket_rockCounter = new int[30];
	public static int[] snailCounter = new int[30];
	public static int[] snail_acornCounter = new int[30];
	public static int[] snail_bottleCounter = new int[30];
	public static int[] snail_bottle_acornCounter = new int[30];
	public static int[] snail_bottle_acorn_ribbonCounter = new int[30];
	public static int[] snail_bottle_ribbonCounter = new int[30];
	public static int[] snail_bottle_ribbon_acornCounter = new int[30];
	public static int[] snail_ribbonCounter = new int[30];
	public static int[] snail_ribbon_acornCounter = new int[30];
	public static int[] sockCounter = new int[30];
	public static int[] sock_featherCounter = new int[30];
	public static int[] sock_grassCounter = new int[30];
	public static int[] sock_ribbonCounter = new int[30];
	public static int[] sock_rockCounter = new int[30];
	public static int[] sock_stickCounter = new int[30];
	public static int[] squirrelCounter = new int[30];
	public static int[] squirrel_grassCounter = new int[30];
	public static int[] stickCounter = new int[30];
	public static int[] stick_acornCounter = new int[30];
	public static int[] stick_bottleCounter = new int[30];
	public static int[] stick_featherCounter = new int[30];
	public static int[] stick_ribbonCounter = new int[30];
	public static int[] stick_rockCounter = new int[30];
	public static int[] stick_sockCounter = new int[30];
	public static int[] stockingCounter = new int[30];
	public static int[] stocking_ribbonCounter = new int[30];
	public static int[] stocking_ribbon_birdBSCounter = new int[30];
	public static int[] stocking_ribbon_birdGFCounter = new int[30];
	public static int[] sushiCounter = new int[30];
	public static int[] sushi_teaCounter = new int[30];
	public static int[] teaCounter = new int[30];
	public static int[] tea_birdGFCounter = new int[30];
	public static int[] teapotCounter = new int[30];
	public static int[] teapot_fireCounter = new int[30];
	public static int[] tentCounter = new int[30];
	public static int[] tent_plutoniumCounter = new int[30];
	public static int[] treeCounter = new int[30];
	public static int[] tree_featherCounter = new int[30];
	public static int[] tree_feather_sockCounter = new int[30];
}
public enum lessCombosEnum
{
	acorn,
	acorn_fire,
	acorn_ribbon,
	baby,
	bbq,
	bbq_grass,
	birdBS,
	birdBS_drum,
	birdBS_fire,
	birdBS_fire_bottle,
	birdGF,
	birdGF_drum,
	birdGF_fire,
	birdGF_fire_bottle,
	bottle,
	bottle_acorn,
	bottle_catkin,
	bottle_feather,
	bottle_grass,
	bottle_ribbon,
	bottle_rock,
	bottle_sock,
	catkin,
	catkin_acorn,
	drum,
	drum_stick,
	feather,
	feather_acorn,
	feather_ribbon,
	fire,
	fire_plutonium,
	flowerpot,
	food,
	grass,
	grass_acorn,
	grass_feather,
	grass_ribbon,
	grass_rock,
	grass_rock_stick,
	grass_stick,
	kite,
	kite_plutonium,
	lightning,
	pillow,
	pillow_ribbon,
	pillow_rock_ribbon,
	plutonium,
	ribbon,
	rock,
	rock_acorn,
	rock_feather,
	rock_ribbon,
	rocket,
	rocket_rock,
	snail,
	snail_acorn,
	snail_bottle,
	snail_bottle_acorn,
	snail_bottle_acorn_ribbon,
	snail_bottle_ribbon,
	snail_bottle_ribbon_acorn,
	snail_ribbon,
	snail_ribbon_acorn,
	sock,
	sock_feather,
	sock_grass,
	sock_ribbon,
	sock_rock,
	sock_stick,
	squirrel,
	squirrel_grass,
	stick,
	stick_acorn,
	stick_bottle,
	stick_feather,
	stick_ribbon,
	stick_rock,
	stick_sock,
	stocking,
	stocking_ribbon,
	stocking_ribbon_birdBS,
	stocking_ribbon_birdGF,
	sushi,
	sushi_tea,
	tea,
	tea_birdGF,
	teapot,
	teapot_fire,
	tent,
	tent_plutonium,
	tree,
	tree_feather,
	tree_feather_sock,
}
