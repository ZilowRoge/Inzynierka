using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Weapon;

namespace Inventory{
public class PlayerInventoryUI : MonoBehaviour {
	public Action<int> drop_item_callback;
	public Action<int> use_item_callback;
	public List<Text> items_amount_ui = new List<Text>();
	public GameObject inventory_ui;
	public UnityEngine.UI.Image highlight;
	private int selected_item;
	private float current_highlight_position;
	List<int> items_amount = new List<int>();
	public bool debug = false;

	public Text input_button;
	void Awake() {
		for (int i = 0; i < 7; i++) {
			items_amount.Add(0);
		}
		update_inventory();
		current_highlight_position = 14;//highlight.rectTransform.position.y;
	}

	private void Update()
	{
		if (debug) {
			if (Input.GetButton("XboxX")) input_button.text = "X";
			if (Input.GetButton("XboxY")) input_button.text = "Y";
			if (Input.GetButton("XboxA")) input_button.text = "A";
			if (Input.GetButton("XboxB")) input_button.text = "B";
			if (Input.GetButton("LeftBumper")) input_button.text = "LBumper";
			if (Input.GetButton("RightBumper")) input_button.text = "RBumper";
			float left_trigger = Input.GetAxis("LeftTrigger");
			float right_trigger = Input.GetAxis("RightTrigger");
			float cross_axis = Input.GetAxis("DPadVertical");
			if (left_trigger > 0) input_button.text = "LeftTrigger" + left_trigger.ToString();
			if (right_trigger > 0) input_button.text = "RightTrigger" + right_trigger.ToString();
			if (cross_axis != 0) input_button.text = "DPadV " + cross_axis.ToString();
		}
		//Debug.Log("Curren pos = " + current_highlight_position);
		
		//if (Input.GetButton("XboxX")) input_button.text = "X";
	}

	private void update_inventory()
	{
		int i = 0;
		foreach (int item_amount in items_amount) {
			//Debug.Log("Add to " + items_amount_ui[i].name);
			items_amount_ui[i++].text = item_amount.ToString();
		}
	}
	public void set_drop_callback(Action<int> action)
	{
		drop_item_callback += action;
	}

	public void set_use_callback(Action<int> action)
	{
		use_item_callback += action;
	}
	public void update_first_aid_amount(int amount)
	{
		items_amount[0] += amount;
		update_inventory();
	}

	public void update_watter_bottle(int amount)
	{
		items_amount[1] += amount;
		update_inventory();
	}

	public void update_food_ration(int amount)
	{
		items_amount[2] += amount;
		update_inventory();
	}

	public void update_ammo(EAmmoType type, int amount)
	{
		switch (type) {
			case EAmmoType.Calliber_9mm:
			items_amount[3] += amount;
			break;
			case EAmmoType.Calliber_5:
			items_amount[4] += amount;
			break;
			case EAmmoType.Calliber_762mm:
			items_amount[5] += amount;
			break;
			case EAmmoType.Calliber_556mm:
			items_amount[6] += amount;
			break;
			default:
			break;
		}
		update_inventory();
	}

	public bool is_active()
	{
		//Debug.Log("Is active = " + inventory_ui.activeSelf);
		return inventory_ui.activeSelf;
	}
	public void open_inventory()
	{
		inventory_ui.SetActive(true);
	}

	public void close_inventory()
	{
		inventory_ui.SetActive(false);
	}
	public void next_item()
	{
		if (selected_item < 6) {
			selected_item++;
			set_highlight_position(current_highlight_position - 3);
		}
	}

	public void previous_item()
	{
		if (selected_item > 0) {
			selected_item--;
			set_highlight_position(current_highlight_position + 3);
		}
	}

	public void drop_item()
	{
		if (items_amount[selected_item] > 0) {
			drop_item_callback(selected_item);
		}
	}

	public void use_item()
	{
		if (items_amount[selected_item] > 0) {
			use_item_callback(selected_item);
		}
	}

	public void on_load(int first_aid, int watter_bottle, int food_ration, int ammo9mm, int ammo5, int ammo556mm, int ammo762mm)
	{
		items_amount[0] = first_aid;
		items_amount[1] = watter_bottle;
		items_amount[2] = food_ration;
		items_amount[3] = ammo9mm;
		items_amount[4] = ammo5;
		items_amount[5] = ammo762mm;
		items_amount[6] = ammo556mm;
		update_inventory();
	}
	private void set_highlight_position(float pos_y)
	{
		highlight.GetComponent<RectTransform>().localPosition = new Vector3(0,pos_y,0);
		current_highlight_position = pos_y;
	}

/*/
	public void update_inventory(List<Items.Item> items)
	{
		Debug.Log("Update inventory, items count = " + items_in_backpack.Count);
		items_in_backpack = items;
		for(int i = selected_item, j = 0; i < items_in_backpack.Count && j < items_viewd.Count; i++, j++)
		{
			items_viewd[j].text = items_in_backpack[i].name;
		}
	}
	public void on_ui_open()
	{
		inventory_ui.SetActive(true);
	}

	public void on_ui_close()
	{
		inventory_ui.SetActive(false);
	}
	public void update_inventory(bool go_up = false)
	{
		int starting_point = (selected_item - items_viewd.Count + 1);
		if (!go_up) {
			starting_point = starting_point < 0 ? 0 : starting_point;
		} else {
			starting_point = selected_item;// jakos trzeba to poprawić, musze kogoś zapytać jak można to łatwo zrobić, może Adrianek wpadnie na jakiś pomysł
		}
		for(int i = starting_point, j = 0; i < inventory.items_in_backpack.Count && j < items_viewd.Count; i++, j++)
		{
			items_viewd[j].text = inventory.items_in_backpack[i].name;
		}
	}

	public void select_next_item()
	{
		Debug.Log("Next " + (inventory.items_in_backpack.Count - 1));
		if (inventory.items_in_backpack.Count - 1 > selected_item) {
			selected_item++;
			Debug.Log("Next item " + selected_item);
			if (selected_item < items_viewd.Count) {
				set_highlight_position(current_highlight_position - 3); //przeskok jest o 3 wiec zmieniam tylko o 3(px);
			}
			update_inventory();
		}
	}

	public void select_previous_item()
	{
		Debug.Log("Previous" + selected_item); 
		if (selected_item > 0) {//to jest do poprawy musze pomyśleć o warunku jak wracac na samą góre.
			set_highlight_position(current_highlight_position + 3); //przeskok jest o 3 wiec zmieniam tylko o 3(px);
			selected_item--;
			update_inventory(false);
		}
	}

	public void next_item()
	{
		
		selected_item++;
		set_highlight_position(current_highlight_position - 3);
		update_list();
	}

	private void update_list()
	{
		int starting_point = (selected_item - items_viewd.Count + 1);
		starting_point = starting_point < 0 ? 0 : starting_point;
		
		int range = (items_in_backpack.Count - selected_item) <= items_viewd.Count ? 
		            (items_in_backpack.Count - selected_item) : 
		            items_viewd.Count;
		Debug.Log("selected item " + selected_item + " starting point " + starting_point + " range " + range);
		var current_visible = items_in_backpack.GetRange(starting_point, range);
		int i = 0;
		foreach(Items.Item item in current_visible) {
			items_viewd[i++].text = item.name;
		}
	}

	private void set_highlight_position(float pos_y)
	{
		highlight.GetComponent<RectTransform>().localPosition = new Vector3(0,pos_y,0);
		current_highlight_position = pos_y;
	}
	*/
	
}
}

