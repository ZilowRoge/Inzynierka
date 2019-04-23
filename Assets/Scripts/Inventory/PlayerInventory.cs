using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;
using Player;

namespace Inventory{
public class PlayerInventory : MonoBehaviour {
	private PlayerStats player_stats;
	public Transform drop_point;
	public ItemFactory item_factory;
	public PlayerInventoryUI inventory_ui;
	public Dictionary<EAmmoType, int> ammo_amount;
	public int first_aid_amount = 0;
	public int water_bottle_amount = 0;

	public int food_ration_amount = 0;

	public float current_weight = 0;
	
	// Use this for initialization
	void Start () {
		player_stats = GetComponent<PlayerStats>();
		ammo_amount = create_dictionary();
		inventory_ui = GetComponent<PlayerInventoryUI>();
		inventory_ui.set_drop_callback(drop);
		inventory_ui.set_use_callback(use);
		item_factory = GameObject.Find("GameManager").GetComponent<ItemFactory>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	public float get_current_weight()
	{
		return current_weight;
	}
	public void pick_up(Items.Item item)
	{
		current_weight += item.weight;
		switch (item.name) {
			case "First aid":
			first_aid_amount++;
			inventory_ui.update_first_aid_amount(1);
			break;
			case "Water Bottle":
			water_bottle_amount++;
			inventory_ui.update_watter_bottle(1);
			break;
			case "Food rations":
			food_ration_amount++;
			inventory_ui.update_food_ration(1);
			break;
			default:
			break;
		}
	}
	public void pick_up(Items.AmmoBox ammobox)
	{
		ammo_amount[ammobox.ammo_type] += ammobox.ammo_amount;
		Debug.Log("Ammo type = " + ammobox.ammo_type);
		inventory_ui.update_ammo(ammobox.ammo_type, ammobox.ammo_amount);
	}

	public void drop(int selected_item)
	{
		Debug.Log("DROP");
		switch (selected_item) {
			case 0:
			first_aid_amount--;
			inventory_ui.update_first_aid_amount(-1);
			item_factory.create_first_aid(drop_point.position);
			break;
			case 1:
			water_bottle_amount--;
			inventory_ui.update_watter_bottle(-1);
			item_factory.create_watter_bottle(drop_point.position);
			break;
			case 2:
			food_ration_amount--;
			inventory_ui.update_food_ration(-1);
			item_factory.create_food_ration(drop_point.position);
			break;
			case 3: //Calliber_9mm
			drop_ammo(EAmmoType.Calliber_9mm, 30);
			break;
			case 4: //Calliber_5
			drop_ammo(EAmmoType.Calliber_5, 30);
			break;
			case 5: //Calliber_762mm
			drop_ammo(EAmmoType.Calliber_762mm, 30);
			break;
			case 6: //Calliber_556mm
			drop_ammo(EAmmoType.Calliber_556mm, 30);
			break;
			default:
			break;
		}
	}

	public void use(int selected_item)
	{
		switch (selected_item) {
			case 0:
			first_aid_amount--;
			inventory_ui.update_first_aid_amount(-1);
			player_stats.curren_health_points += Common.first_aid_heal;
			break;
			case 1:
			water_bottle_amount--;
			inventory_ui.update_watter_bottle(-1);
			player_stats.current_thirst += Common.water_bottle_heal;
			break;
			case 2:
			food_ration_amount--;
			inventory_ui.update_food_ration(-1);
			player_stats.current_hunger += Common.hunger_heal;
			break;
			default:
			break;
		}
	}

	private void drop_ammo(EAmmoType type, int amount)
	{
		ammo_amount[type] -= amount;
		inventory_ui.update_ammo(type, -amount);
		item_factory.create_ammo(drop_point.position, type, amount);
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
		ammo_amount[EAmmoType.Calliber_5] = 0;
		ammo_amount[EAmmoType.Calliber_762mm] = 0;
		ammo_amount[EAmmoType.Calliber_556mm] = 0;
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