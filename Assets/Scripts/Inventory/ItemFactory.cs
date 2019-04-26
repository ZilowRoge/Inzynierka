using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Items;
using UnityEngine;
namespace Inventory {
public class ItemFactory : MonoBehaviour {

	public FirstAid first_aid_prefab;

	public WaterBottle water_bottle_prefab;

	public FoodRation food_rations;
	public GameObject m107_prefab;
	public GameObject ak47_prefab;
	public GameObject scar_prefab;
	public GameObject m4a1_prefab;
	public GameObject g36c_prefab;
	public GameObject mp5_prefab;
	public GameObject beretta_prefab;
	public GameObject colt_prefab;
	public GameObject deagle_prefab;
	public GameObject glock_prefab;
	public GameObject item_parent;
	
	public Common common;
	public void create_first_aid(Vector3 position)
	{
		Debug.Log("Create first aid");
		Instantiate(first_aid_prefab, position, new Quaternion(100.0f, 10.0f, 20.0f, -40.0f), item_parent.transform);
	}

	public void create_watter_bottle(Vector3 position)
	{
		Instantiate(water_bottle_prefab, position, new Quaternion(-10.0f, -20.0f, 10.0f, 10.0f), item_parent.transform);
	}

	public void create_ammo(Vector3 position, Weapon.EAmmoType ammotype, int amount)
	{
		Debug.Log("Item Factory ammotype = " + ammotype);
		GameObject ammobox = common.get_object_from_type(ammotype);
		GameObject obj = Instantiate(ammobox, position, new Quaternion(50.0f, 20.0f, 10.0f, 0.0f), item_parent.transform);
		obj.GetComponent<AmmoBox>().ammo_amount = amount;
	}

	public void create_food_ration(Vector3 position)
	{
		Debug.Log("Create food rations");
		Instantiate(food_rations, position, new Quaternion(-10.0f, -20.0f, 10.0f, 10.0f), item_parent.transform);
	}

	public GameObject get_weapon_from_name(string name)
	{
		GameObject weapon = null;
		switch(name)
		{
			case "M107":
			weapon = m107_prefab;
			break;
			case "AK-47":
			weapon = ak47_prefab;
			break;
			case "M4A1":
			weapon = m4a1_prefab;
			break;
			case "G36C":
			weapon = g36c_prefab;
			break;
			case "SCAR-H":
			weapon = scar_prefab;
			break;
			case "MP5":
			weapon = mp5_prefab;
			break;
			case "Beretta_M9":
			weapon = beretta_prefab;
			break;
			case "Colt_M1911":
			weapon = colt_prefab;
			break;
			case "Desert_Eagle":
			weapon = deagle_prefab;
			break;
			case "Glock_18":
			weapon = glock_prefab;
			break;
		}

		return weapon;
	}

	internal void create_weapon(string name, Vector3 pos)
	{
		Debug.Log("Weapon name: " + name);
		GameObject weapon = get_weapon_from_name(name);
		Instantiate(weapon, pos, new Quaternion(0,0,0,0), item_parent.transform);
	}
    }
}// namespcae Inventory
