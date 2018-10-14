using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Player {
public class PlayerWeaponSlots : MonoBehaviour {

	public GameObject slot1;
	public GameObject slot2 = null;
	public Transform hand_position;
	public WeaponBehavior weapon_behavior;

	Transform main_camera_transform;
	bool holding_weapon = false;
	void Start () {
		main_camera_transform = transform.GetChild(1);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(slot1 != null && holding_weapon)
		{
			slot1.transform.localEulerAngles = new Vector3(-90,180,0);
		}
		
	}

	public bool can_pickup()
	{
		return slot1 == null && !holding_weapon;
	}
	public bool can_drop()
	{
		return holding_weapon && slot1 != null;
	}
	
	public WeaponBehavior get_script()
	{
		return weapon_behavior;
	}

	public void swap_slots()
	{
		if(slot2 != null)
			slot2.SetActive(true);
		
		GameObject item = slot1;
		slot1 = slot2;
		slot2 = item;

		if(slot2 != null)
			slot2.SetActive(false);
		
		if(slot1 == null){
			holding_weapon = false;
			weapon_behavior = null;
		}
		else{
			holding_weapon = true;
			weapon_behavior = slot1.GetComponent<WeaponBehavior>();
		}
			
			
		Debug.Log("Swap");
	}
/*
	void on_drag_collision(RaycastHit hit)
	{
		if(hit.collider.tag == "Drag"){
			slot1 = hit.collider.gameObject;
			holding = true;
		}
	}
*/
	public void pick_up(GameObject item)
	{
		item.GetComponent<Rigidbody>().isKinematic = true;
		weapon_behavior = item.GetComponent<WeaponBehavior>();
		item.transform.SetParent(main_camera_transform);
		item.transform.SetPositionAndRotation(hand_position.position, new Quaternion(0,0,0,0));
		item.transform.localEulerAngles = new Vector3(-90,180,0);
		item.GetComponent<BoxCollider>().enabled = false;
		slot1 = item;
		holding_weapon = true;
	}
	public void drop_weapon()
	{
		slot1.GetComponent<Rigidbody>().isKinematic = false;
		slot1.transform.SetParent(null);
		slot1.GetComponent<BoxCollider>().enabled = true;
		slot1 = null;
		holding_weapon = false;
	}

}

}
