using System.Collections;
using System.Collections.Generic;
using Inventory.Items;
using UnityEngine;
namespace Inventory {
public class ItemFactory : MonoBehaviour {

	public FirstAid first_aid_prefab;

	public WaterBottle water_bottle_prefab;

	public FoodRation food_rations;

	public void create_first_aid(Vector3 position)
	{
		Debug.Log("Create first aid");
		Instantiate(first_aid_prefab, position, new Quaternion(100.0f, 10.0f, 20.0f, -40.0f));
	}

	public void create_watter_bottle(Vector3 position)
	{
		Instantiate(water_bottle_prefab, position, new Quaternion(-10.0f, -20.0f, 10.0f, 10.0f));
	}

	public void create_ammo(Vector3 position, Weapon.EAmmoType ammotype, int amount)
	{
		Debug.Log("Item Factory ammotype = " + ammotype);
		GameObject ammobox = Common.get_object_from_type(ammotype); 
		GameObject obj = Instantiate(ammobox, position, new Quaternion(50.0f, 20.0f, 10.0f, 0.0f));
		obj.GetComponent<AmmoBox>().ammo_amount = amount;
	}

	public void create_food_ration(Vector3 position)
	{
		Debug.Log("Create food rations");
		Instantiate(food_rations, position, new Quaternion(-10.0f, -20.0f, 10.0f, 10.0f));
	}
}
}// namespcae Inventory
