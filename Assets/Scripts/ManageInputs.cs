using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Weapon;
using Player;

public class ManageInputs : MonoBehaviour {

	public PlayerMovment player_movment;
	public PlayerManager player_manager;

	private bool inventory_opened = false;
	private float ui_key_down_timer = 0;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		manage_player_inputs();
		manage_weapons_input();
		manage_ui_input();
	}

	void manage_player_inputs()
	{
		manage_player_movment();
		manage_player_slots();
	}

	void manage_player_movment()
	{
		float horizontal = Input.GetAxis("Horizontal");     //joystick horizontal
		float vertical = Input.GetAxis("Vertical");      //joystick vertical
		if (horizontal > 0.0f) {
			player_movment.move_left();
		}
		if (horizontal < 0.0f) {
			player_movment.move_right();
		}
		if (vertical > 0.0f) {
			player_movment.move_forward();
		}
		if (vertical < 0.0f) {
			player_movment.move_backward();
		}
	}

	void manage_player_slots(){
		if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown("e")) {
			player_manager.try_pickup();
		}

		if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown("z")) {
			player_manager.swap_slots();
		}

		if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown("x")) {
			player_manager.drop_weapon();
		}
	}

	void manage_weapons_input()
	{
		if (Input.GetKey("v") || Input.GetAxis("LeftTrigger") > 0 || Input.GetMouseButtonDown(1)) {
			player_manager.fire();
		}

		if (Input.GetKeyUp("v") || Input.GetAxis("LeftTrigger") < 0 || Input.GetMouseButtonUp(1)) {
			player_manager.fired();
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown("r")) {
			player_manager.reload();
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown("q")) {
			player_manager.swap_modes();
		}
	}

	void manage_ui_input()
	{
		if (ui_key_down_timer < 0.5) {
			ui_key_down_timer += Time.deltaTime;
		}
		if (Input.GetKeyUp("i") && inventory_opened && ui_key_down_timer >= 0.5) {
			player_manager.close_inventory();
			inventory_opened = false;
			ui_key_down_timer = 0;
		}

		if (Input.GetKeyUp("i") && !inventory_opened && ui_key_down_timer >= 0.5) {
			player_manager.open_inventory();
			inventory_opened = true;
			ui_key_down_timer = 0;
		}

		if (Input.GetKeyDown(".")) {
			player_manager.next_item();
		} else if (Input.GetKeyDown(",")) {
			player_manager.previous_item();
		}
	}
}
