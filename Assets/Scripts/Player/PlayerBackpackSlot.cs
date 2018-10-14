using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

namespace Player {
public class PlayerBackpackSlot : MonoBehaviour {

	public GameObject backpack_slot;
	
	public BackpackBehavior backpack_behavior;

	public Transform backpack_position;

	public Transform player_body;

	bool backpack_equiped = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool can_pickup()
	{
		return backpack_slot == null && !backpack_equiped;
	}

	bool can_drop()
	{
		return backpack_slot != null && backpack_equiped;
	}

	public void drop()
	{
		if(can_drop())
		{
			//drop_weapon();
		}
	}
	public BackpackBehavior get_script()
	{
		return backpack_behavior;
	}

	public void pick_up(GameObject item)
	{
		item.GetComponent<Rigidbody>().isKinematic = true;
		backpack_behavior = item.GetComponent<BackpackBehavior>();
		item.transform.SetParent(player_body);
		item.transform.SetPositionAndRotation(backpack_position.position, new Quaternion(0,0,0,0));
		item.transform.localEulerAngles = new Vector3(0,90,0);
		item.GetComponent<BoxCollider>().enabled = false;
		backpack_slot = item;
		backpack_equiped = true;
	}
	void drop_backpack()
	{
		backpack_slot.GetComponent<Rigidbody>().isKinematic = false;
		backpack_slot.transform.SetParent(null);
		backpack_slot.GetComponent<BoxCollider>().enabled = true;
		backpack_slot = null;
		backpack_equiped = false;
	}
}

} //namespace Player
