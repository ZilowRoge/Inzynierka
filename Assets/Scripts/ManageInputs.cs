using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Weapon;
using Player;
using Inventory;

public class ManageInputs : MonoBehaviour {
	public PlayerMovment player_movment;
	public PlayerManager player_manager;

	public PlayerInventoryUI player_inventory;
	public DataControl.DataHandler data_handler;
	public ItemFactory item_factory;
	public GameObject mobs_parent;
	public GameObject gurdian_prefab;
	public GameObject normal_prefab;

	private bool inventory_opened = false;
	private float ui_key_down_timer = 0;

	private bool realoaded = false;
	
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
		float horizontal = Input.GetAxis("Horizontal");//Input.GetAxis("Horizontal");     //joystick horizontal
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
		if (Input.GetKey(KeyCode.Space) || Input.GetButton("XboxA")) {
			player_movment.jump();
		}
	}

	void manage_player_slots(){
		if (!player_inventory.is_active() && (Input.GetButton("XboxX") || Input.GetKeyDown("e"))) {
			player_manager.try_pickup();
		}

		if (Input.GetButtonDown("LeftBumper") || Input.GetKeyDown("z")) {
			player_manager.swap_slots();
		}

		if (!player_inventory.is_active() && (Input.GetButton("XboxY") || Input.GetKeyDown("x"))) {
			player_manager.drop_weapon();
		}
	}

	void manage_weapons_input()
	{

		if (/*Input.GetKeyUp("v") || Input.GetAxis("RightTrigger") <= 0 ||*/ Input.GetMouseButtonUp(1)) {
			Debug.Log("Button up");
			player_manager.fired();
		}

		if (/*Input.GetKeyDown("v") || Input.GetAxis("RightTrigger") > 0 || */Input.GetMouseButtonDown(1)) {
			player_manager.fire();
		}

		if ((Input.GetAxis("LeftTrigger") == 1 && !realoaded) || Input.GetKeyDown("r")) {
			player_manager.reload();
			realoaded = true;
		}
		if (Input.GetAxis("LeftTrigger") < 0.3 && realoaded) {
			realoaded = false;
		}
		if (Input.GetButtonDown("XboxB") || Input.GetKeyDown("q")) {
			player_manager.swap_modes();
		}
	}

	void manage_ui_input()
	{
		if (ui_key_down_timer < 0.5) {
			ui_key_down_timer += Time.deltaTime;
		}
		if ((Input.GetButtonDown("XboxMenu") || Input.GetKeyUp("i")) && inventory_opened && ui_key_down_timer >= 0.5) {
			player_inventory.close_inventory();
			inventory_opened = false;
			ui_key_down_timer = 0;
		}

		if ((Input.GetButtonDown("XboxMenu") || Input.GetKeyUp("i")) && !inventory_opened && ui_key_down_timer >= 0.5) {
			player_inventory.open_inventory();
			inventory_opened = true;
			ui_key_down_timer = 0;
		}

		if (Input.GetKeyDown("[") || Input.GetAxis("DPadVertical") > 0) {
			player_inventory.next_item();
		} else if (Input.GetKeyDown("]") || Input.GetAxis("DPadVertical") < 0) {
			player_inventory.previous_item();
		}

		if (player_inventory.is_active() && (Input.GetButtonDown("XboxY")) || Input.GetKeyDown("x")) {
			Debug.Log("Drop item");
			player_inventory.drop_item();
		}

		if (player_inventory.is_active() && (Input.GetButton("XboxX") || Input.GetKeyDown("e"))) {
			Debug.Log("Use item");
			player_inventory.use_item();
		}

		if (Input.GetKeyDown("1")) save_game();
		if (Input.GetKeyDown("2")) load_game();
	}

	public void save_game()
	{
		player_manager.on_save();
		data_handler.save_items();
		data_handler.save_mobs();
	}

	public void load_game()
	{
		player_manager.on_load();
		place_items(data_handler.load_items());
		place_mobs(data_handler.load_mobs());
	}

	private void place_items(DataControl.ItemData item_data)
	{
		Debug.Log("Load items: " + item_data.item_list.Count);
		foreach (DataControl.Item item in item_data.item_list)
		{
			Debug.Log("Loaded item: " + item.item_name);
			switch(item.item_name)
			{
				case "WaterBottle":
				item_factory.create_watter_bottle(new Vector3(item.pos_x, item.pos_y, item.pos_z));
				break;
				case "FirstAid":
				item_factory.create_first_aid(new Vector3(item.pos_x, item.pos_y, item.pos_z));
				break;
				case "FoodRations":
				item_factory.create_food_ration(new Vector3(item.pos_x, item.pos_y, item.pos_z));
				break;
				case "Ammobox9mm":
				item_factory.create_ammo(new Vector3(item.pos_x, item.pos_y, item.pos_z), EAmmoType.Calliber_9mm, 30);
				break;
				case "Ammobox50":
				item_factory.create_ammo(new Vector3(item.pos_x, item.pos_y, item.pos_z), EAmmoType.Calliber_5, 30);
				break;
				case "Ammobox556mm":
				item_factory.create_ammo(new Vector3(item.pos_x, item.pos_y, item.pos_z), EAmmoType.Calliber_556mm, 30);
				break;
				case "Ammobox762mm":
				item_factory.create_ammo(new Vector3(item.pos_x, item.pos_y, item.pos_z), EAmmoType.Calliber_762mm, 30);
				break;
				default:
				item_factory.create_weapon(item.item_name, new Vector3(item.pos_x, item.pos_y+2, item.pos_z));
				break;
			}
		}
	}

	private void place_mobs(DataControl.MobsData mob_data)
	{
		foreach (DataControl.Mob mob in mob_data.mob_list)
		{
			switch(mob.mob_name)
			{
				case "GuardianMob":
				Instantiate(gurdian_prefab, new Vector3(mob.pos_x, mob.pos_y, mob.pos_z), Quaternion.Euler(0,0,0), mobs_parent.transform);
				break;
				case "NormalMob":
				Instantiate(normal_prefab, new Vector3(mob.pos_x, mob.pos_y, mob.pos_z), Quaternion.Euler(0,0,0), mobs_parent.transform);
				break;
			}
		}
	}
}
