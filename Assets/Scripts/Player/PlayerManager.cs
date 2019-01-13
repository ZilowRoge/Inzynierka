﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;
using Inventory;
using Inventory.Items;

namespace Player {

public class PlayerManager : MonoBehaviour {
	public PlayerWeaponSlots weapon_slots;

	public PlayerBackpackSlot backpack_slot;
	public PlayerInventory player_inventory;
	public WeaponBehavior weapon_script;
	public PlayerInventoryUI inventory_ui;



	bool fire_pressed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (fire_pressed) {
			weapon_script.fire();
		}
	}

	//Weapon scripts
	public void fire()
	{
		fire_pressed = true && weapon_script != null;
	}

	public void fired()
	{
		if (fire_pressed) {
			weapon_script.fired();
			fire_pressed = false;
		}
	}

	public void swap_modes()
	{
		if (weapon_script.can_change_mode()) {
			weapon_script.switch_mode();
		}
	}

	public void try_pickup()
	{
		raycast();
	}

	public void drop_weapon()
	{
		if (weapon_slots.can_drop()) {
			weapon_slots.drop_weapon();
			weapon_script = null;
		}
	}

	public void swap_slots()
	{
		weapon_slots.swap_slots();
		weapon_script = weapon_slots.get_script();
	}

	public void reload()
	{
		int ammo_type = weapon_script.get_ammo_type();
		int ammo_amount = player_inventory.get_ammo_amount(ammo_type);
		int ammo_used = weapon_script.reload(ammo_amount);
		player_inventory.set_ammo_amount(ammo_type, ammo_amount - ammo_used);
	}

	//Backpack scripts


	private void raycast()
	{
		RaycastHit hit;
		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.black, 10.0f);
		if (Physics.Raycast(ray, out hit, 10.0f)){
			on_raycast_collision(hit);
		}
	}

	public void on_raycast_collision(RaycastHit hit)
	{
		if (hit.collider.tag == "Weapon" && weapon_slots.can_pickup()) {
			weapon_slots.pick_up(hit.collider.gameObject);
			weapon_script = weapon_slots.get_script();
		} else if (hit.collider.tag == "Backpack" && backpack_slot.can_pickup()) {
			backpack_slot.pick_up(hit.collider.gameObject);
		} else if (hit.collider.tag == "Ammo") {
			AmmoBox ammobox = hit.collider.gameObject.GetComponent<AmmoBox>();
			ammobox.on_pickup();
			player_inventory.pick_up(ammobox); //w inventory musze wiedzieci ile mam ammo i usuwac puste pudelka (to juz chyba zrobione)
		} else if (hit.collider.tag == "Item") {
			Debug.Log("item hit");
			Item item = hit.collider.gameObject.GetComponent<Item>();
			item.on_pickup();
			player_inventory.pick_up(item);
		}
	}

	//UI scripts

	public void open_inventory()
	{
		Debug.Log("Open UI");
		inventory_ui.on_ui_open();
	}

	public void close_inventory()
	{
		Debug.Log("Close UI");	
		inventory_ui.on_ui_close();
	}

	public void next_item()
	{
		inventory_ui.select_next_item();
	}

	public void previous_item()
	{
		inventory_ui.select_previous_item();
	}
}

}