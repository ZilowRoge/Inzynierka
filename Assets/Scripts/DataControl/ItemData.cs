using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataControl{
[Serializable]
public class ItemData {
	public List<Item> item_list;
	public ItemData()
	{
		item_list = new List<Item>();
	}
	public void add_item(Item item)
	{
		item_list.Add(item);
	}
}
[Serializable]
public class Item {
	public float pos_x;
	public float pos_y;
	public float pos_z;
	public string item_name;
	
}
}