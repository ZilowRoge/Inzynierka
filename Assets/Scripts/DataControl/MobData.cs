using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataControl{
[Serializable]
public class MobsData {
	public List<Mob> mob_list;
	public MobsData()
	{
		mob_list = new List<Mob>();
	}
	public void add_item(Mob mob)
	{
		mob_list.Add(mob);
	}
}
[Serializable]
public class Mob {
	public float pos_x;
	public float pos_y;
	public float pos_z;
	public string mob_name;
	
}
}