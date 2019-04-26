using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace DataControl{
public class DataHandler : MonoBehaviour {
	string item_data_path;
	string player_data_save_path;
	string player_inventory_save_path;
	string mob_data_path;

	public Transform items_parent;
	public Transform mobs_parent;

	public bool finished;

	// Use this for initialization
	void Start () {
		player_data_save_path = Path.Combine(Application.persistentDataPath, "PlayerData.txt");
		player_inventory_save_path = Path.Combine(Application.persistentDataPath, "InventoryData.txt");
		item_data_path = Path.Combine(Application.persistentDataPath, "ItemData.txt");
		mob_data_path = Path.Combine(Application.persistentDataPath, "MobData.txt");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void save_player_data(PlayerData data)
	{
		string json_data = JsonUtility.ToJson(data);
		using (StreamWriter writter = File.CreateText(player_data_save_path))
		{
			writter.Write(json_data);
		}
	}

	public void save_inventory(InventoryData data)
	{
		string json_data = JsonUtility.ToJson(data);
		using (StreamWriter writter = File.CreateText(player_inventory_save_path)) {
			writter.Write(json_data);
		}
	}

	public void save_items()
	{
		string json_data = JsonUtility.ToJson(prepare_items_for_save());
		using (StreamWriter writter = File.CreateText(item_data_path)) {
			//Debug.Log(json_data);
			writter.Write(json_data);
		}
	}

	public void save_mobs()
	{
		string json_data = JsonUtility.ToJson(prepare_mobs_for_save());
		using (StreamWriter writter = File.CreateText(mob_data_path)) {
			//Debug.Log(json_data);
			writter.Write(json_data);
		}
	}

	public PlayerData load_player_data()
	{
		using (StreamReader streamReader = File.OpenText(player_data_save_path)) {
			string jsonString = streamReader.ReadToEnd();
			return JsonUtility.FromJson<PlayerData>(jsonString);
		}
	}

	public InventoryData load_inventory()
	{
		using (StreamReader streamReader = File.OpenText(player_inventory_save_path)) {
			string jsonString = streamReader.ReadToEnd();
			return JsonUtility.FromJson<InventoryData>(jsonString);
		}
	}

	public ItemData load_items()
	{
		Debug.Log("Loading items");
		using (StreamReader streamReader = File.OpenText(item_data_path)) {
			string jsonString = streamReader.ReadToEnd();
			return JsonUtility.FromJson<ItemData>(jsonString);
		}
	}

	public MobsData load_mobs()
	{
		Debug.Log("Loading items");
		using (StreamReader streamReader = File.OpenText(mob_data_path)) {
			string jsonString = streamReader.ReadToEnd();
			return JsonUtility.FromJson<MobsData>(jsonString);
		}
	}

	private ItemData prepare_items_for_save()
	{
		//finished = false;
		ItemData item_data = new ItemData();
		for (int i = 0 ;i < items_parent.GetChildCount(); i++) {
			GameObject item = items_parent.GetChild(i).gameObject;
			Item item_to_save = new Item();
			item_to_save.pos_x = item.transform.position.x;
			item_to_save.pos_y = item.transform.position.y;
			item_to_save.pos_z = item.transform.position.z;
			string name = item.name.Split('(')[0];
			item_to_save.item_name = name;
			item_data.add_item(item_to_save);
		}
		//Debug.Log("Save items: " + item_data.item_list.Count);
		return item_data;
		//finished = true;
		//yield return new WaitForSeconds (delay);
	}

	private MobsData prepare_mobs_for_save()
	{
		//finished = false;
		MobsData mobs_data = new MobsData();
		for (int i = 0 ;i < mobs_parent.GetChildCount(); i++) {
			GameObject mob = mobs_parent.GetChild(i).gameObject;
			Mob mob_to_save = new Mob();
			mob_to_save.pos_x = mob.transform.position.x;
			mob_to_save.pos_y = mob.transform.position.y;
			mob_to_save.pos_z = mob.transform.position.z;
			string name_split = mob.name.Split(' ')[0];
			string name = name_split.Split('(')[0];
			mob_to_save.mob_name = name;
			mobs_data.add_item(mob_to_save);
		}
		//Debug.Log("Save items: " + item_data.item_list.Count);
		return mobs_data;
		//finished = true;
		//yield return new WaitForSeconds (delay);
	}
}
}