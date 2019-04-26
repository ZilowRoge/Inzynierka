using System;
using System.Collections;
using System.Collections.Generic;
namespace DataControl{
[Serializable]
public class InventoryData {
	public int first_aid;
	public int water_bottle;
	public int food_ration;

	public int ammo_9mm;
	public int ammo_5;
	public int ammo_556mm;
	public int ammo_762mm;

	public float current_weight;
}
}