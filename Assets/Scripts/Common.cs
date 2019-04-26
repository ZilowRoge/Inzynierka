using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;
public class Common : MonoBehaviour {
	public GameObject ammobox5;
	public GameObject ammobox9mm;
	public GameObject ammobox556mm;
	public GameObject ammobox762mm;
	public static Dictionary<EAmmoType, string> ammo_type_to_string = new Dictionary<EAmmoType, string>();
	public static Dictionary<EAmmoType, string> ammo_type_to_path = new Dictionary<EAmmoType, string>();
	public static int max_ammo = 30;
	public static float hunger_drop = 0.33f;
	public static float thirst_drop = 0.33f;
	public static float fast_thirst_drop = 0.5f;

	public static float water_bottle_heal = 10.0f;

	public static float first_aid_heal = 10.0f;

	public static float hunger_heal = 30.0f;

	// Use this for initialization
	void Awake () {
		initialize_ammo_type_to_string();
		initialize_ammo_type_to_path();
	}

	static void initialize_ammo_type_to_string()
	{
		ammo_type_to_string[EAmmoType.Calliber_9mm] = "Ammobox 9mm";
		ammo_type_to_string[EAmmoType.Calliber_762mm] = "Ammobox 7.62mm";
		ammo_type_to_string[EAmmoType.Calliber_556mm] = "Ammobox 5.56mm";
		ammo_type_to_string[EAmmoType.Calliber_5] = "Ammobox .50";
		//ammo_type_to_string[EAmmoType.Calliber_45] = "Ammobox .45";
	}

	static void initialize_ammo_type_to_path()
	{
		ammo_type_to_path[EAmmoType.Calliber_9mm] = "Assets/Prefabs/Ammo/Ammobox9mm.prefab";
		ammo_type_to_path[EAmmoType.Calliber_762mm] = "Assets/Prefabs/Ammo/Ammobox762mm.prefab";
		ammo_type_to_path[EAmmoType.Calliber_556mm] = "Assets/Prefabs/Ammo/Ammobox556mm.prefab";
		ammo_type_to_path[EAmmoType.Calliber_5] = "Assets/Prefabs/Ammo/Ammobox50.prefab";
		//ammo_type_to_path[EAmmoType.Calliber_45] = "Assets/Prefabs/Ammo/Ammobo45.prefab";
	}

	public GameObject get_object_from_type(Weapon.EAmmoType type){
	//	Debug.Log(path);
		GameObject prefab = null;
		switch (type)
		{
			case Weapon.EAmmoType.Calliber_5:
			prefab = ammobox5;
			Debug.Log("Ammobox1: " + prefab);
			break;
			case Weapon.EAmmoType.Calliber_9mm:
			prefab = ammobox9mm;
			break;
			case Weapon.EAmmoType.Calliber_762mm:
			prefab = ammobox762mm;
			break;
			case Weapon.EAmmoType.Calliber_556mm:
			prefab = ammobox556mm;
			break;
			default:
			break;
		}
		return prefab; 
	}
}
