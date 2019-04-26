using System;
using System.Collections;
using System.Collections.Generic;
namespace DataControl{
[Serializable]
public class PlayerData {
	public float pos_x;
	public float pos_y;
	public float pos_z;

	public float health;
	public float hunger;
	public float thirst;
	public string slot1_weapon_name;
	public string slot2_weapon_name;
}
}