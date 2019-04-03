using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Weapon;
using Player;

namespace Inventory{
public class PlayerInventory : MonoBehaviour {
	public List<Items.Item> items_in_backpack = new List<Items.Item>();
	public PlayerInventoryUI inventory_ui;
	public Dictionary<EAmmoType, int> ammo_amount;
	public int first_aid_amount = 0;
	public int water_bottle_amount = 0;
	
	// Use this for initialization
	void Start () {
		ammo_amount = create_dictionary();
		inventory_ui = GetComponent<PlayerInventoryUI>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void pick_up(Items.Item item)
	{
		//items_in_backpack.Add(item);
		switch (item.name) {
			case "First aid":
			first_aid_amount++;
			inventory_ui.update_first_aid_amount();
			break;
			case "Water Bottle":
			water_bottle_amount++;
			inventory_ui.update_watter_bottle();
			break;
			default:
			break;
		}
	}
	public void pick_up(Items.AmmoBox ammobox)
	{
		ammo_amount[ammobox.ammo_type] += ammobox.ammo_amount;
		inventory_ui.update_ammo(ammobox.ammo_type, ammobox.ammo_amount);
	}

	public int get_ammo_amount(int ammo)
	{
		EAmmoType ammo_t = (EAmmoType)ammo;
		return ammo_amount[ammo_t];
	}

	public void set_ammo_amount(int ammo, int amount_used)
	{
		Debug.Log("set ammo");
		EAmmoType ammo_t = (EAmmoType)ammo;
		ammo_amount[ammo_t] -= amount_used;
		inventory_ui.update_ammo(ammo_t, -amount_used);
		//remove_ammo(ammo_t, amount);
	}

	private static Dictionary<EAmmoType, int> create_dictionary()
	{
		Dictionary<EAmmoType, int> ammo_amount = new Dictionary<EAmmoType, int>();
		ammo_amount[EAmmoType.Calliber_9mm] = 0;
		ammo_amount[EAmmoType.Calliber_762mm] = 0;
		ammo_amount[EAmmoType.Calliber_556mm] = 0;
		ammo_amount[EAmmoType.Calliber_5] = 0;
		return ammo_amount;
	}
/*
	public void remove_ammo(EAmmoType type, int ammo_left){

		remove_ammo(Common.ammo_type_to_string[type]);
		if(ammo_left > 0){
			Debug.Log("Ammoleft" + (ammo_left > 0));
			while(ammo_left > 0)
			{
				Debug.Log("Ammo left " + ammo_left);
				int ammo_packs = ammo_left/Common.max_ammo;
				if (ammo_packs >= 1) {
					add_ammo_item_to_inventory(type, Common.max_ammo);
					ammo_left -= Common.max_ammo;
				} else if(ammo_left > 0) {
					add_ammo_item_to_inventory(type, ammo_left);
					ammo_left = 0;
				}
			}
		}
	}

	
	private Pair<EAmmoType, int> get_ammo<T>(T item) where T : Items.Item
	{
		System.Type item_type = item.GetType();
		string[] arr = item_type.ToString().Split('.');
		string type = arr[arr.Length-1];

		if (type == "AmmoBox") {
			EAmmoType ammo_type = ((Items.AmmoBox)(Object)item).ammo_type;
			int ammo_amount = ((Items.AmmoBox)(Object)item).ammo_amount;
			return new Pair<EAmmoType, int>(ammo_type, ammo_amount);
		}
		return null;
	}

	private Items.AmmoBox convert_to_ammobox<T>(T item) where T : Items.Item
	{
		System.Type item_type = item.GetType();
		string[] arr = item_type.ToString().Split('.');
		string type = arr[arr.Length-1];

		Items.AmmoBox ammobox = null;

		if (type == "AmmoBox") {
			ammobox = ((Items.AmmoBox)(Object)item);
		}

		return ammobox;
	}

	private void add_ammo_item_to_inventory(EAmmoType type, int amount)
	{
		GameObject ammobox = get_object_from_path(Common.ammo_type_to_path[type]); 
		ammobox.name = ammobox.name.Replace("(Clone)", "");
		Items.AmmoBox script = ammobox.GetComponent<Items.AmmoBox>();
		script.on_pickup();
		items_in_backpack.Add(script);
		//Destroy(ammobox);
		Debug.Log("Add ammo " + amount);
	}
	
	private void remove_ammo(string item_name)
	{
		items_in_backpack.RemoveAll(
				delegate(Items.Item item)
				{
					Items.AmmoBox ammobox = convert_to_ammobox(item);
					ammobox.ammo_amount = 0;
					return item.name.Equals(item_name);
				}
			);
	}
	GameObject get_object_from_path(string path){
		Debug.Log(path);
		GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
		GameObject obj = (GameObject)Instantiate(prefab,Vector3.zero, Quaternion.identity);

		return obj; 
	}*/

}
}

/*public class Item{
		public string name;
	}
	public class Ammobox : Item {
		public int amount;
	}
	
	static List<Item> items = new List<Item>();
	
	public static void do_sth<T>(T item) where T : Item
	{
		Type titem = item.GetType();
		Console.WriteLine(titem.ToString());
	}
	public static void Main()
	{
		Ammobox ammo = new Ammobox();
		items.Add(ammo);
		do_sth(items[0]);
		Console.WriteLine("Hello World");
	}
 */