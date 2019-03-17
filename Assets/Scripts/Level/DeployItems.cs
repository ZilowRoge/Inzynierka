using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;
using Inventory.Items;
namespace Level {
	
public class DeployItems : MonoBehaviour {

	public List<GameObject> deploy_points;
	public List<WeaponBehavior> weapon_prefabs;
	
	public List<AmmoBox> ammobox_prefabs;

	public FirstAid first_aid_prefab;

	public WaterBottle water_bottle_prefab;

	private EAmmoType ammo_type_to_use = EAmmoType.Calliber_9mm;

	// Use this for initialization
	void Start () {
		/*for (int i = 0; i < deploy_points.Count; i++) {
			deploy(deploy_points[i].gameObject.transform.position);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void deploy(Vector3 position)
	{
		bool can_deploy_weapon = Random.value >= 0.5; //jesli 0 deploy supply

		if (can_deploy_weapon) {
			deploy_weapon(position);
			deploy_ammo(position);
		} else {
			deploy_first_aid(position);
			deploy_water(position);
			deploy_ammo(position);
		}
	}

	private void deploy_weapon(Vector3 position)
	{
		int weapon_id = Mathf.RoundToInt(Random.Range(-0.4f, 12.4f));
		while (weapon_id > weapon_prefabs.Count - 1) {
			Debug.Log("ERR (DeployItems.cs): weapon_id > weapon_prefabs.Count - 1");
			weapon_id = Mathf.RoundToInt(Random.Range(0.0f, 12.0f));
		}
		ammo_type_to_use = (EAmmoType)weapon_prefabs[weapon_id].get_ammo_type();
		Instantiate(weapon_prefabs[weapon_id], position, new Quaternion(10.0f, 0.0f, 5.0f, 0.0f));
	}

	private void deploy_ammo(Vector3 position)
	{
		int change_ammo = Mathf.RoundToInt(Random.Range(0.0f, 100.0f));
		
		if (change_ammo > 90.0f) {
			ammo_type_to_use = (EAmmoType)get_int_ammo_type();
		}
		for (int i = 0; i < 2; i++) {
			Instantiate(ammobox_prefabs[(int)ammo_type_to_use], position, new Quaternion(50.0f, 20.0f, 10.0f, 0.0f));
		}
	}

	private void deploy_first_aid(Vector3 position)
	{
		Instantiate(first_aid_prefab, position, new Quaternion(100.0f, 10.0f, 20.0f, -40.0f));
	}

	private void deploy_water(Vector3 position)
	{
		Instantiate(water_bottle_prefab, position, new Quaternion(-10.0f, -20.0f, 10.0f, 10.0f));
	}

	private int get_int_ammo_type() {
		int int_ammo_type = Mathf.RoundToInt(Random.Range(-0.4f, 5.4f));
		while (int_ammo_type > ammobox_prefabs.Count - 1) {
			Debug.Log("ERR (DeployItems.cs): int_ammo_type > ammobox_prefabs.Count - 1");
			int_ammo_type = Mathf.RoundToInt(Random.Range(-0.4f, 5.4f));
		}
		return int_ammo_type;
	}
}
} //namespace Level
