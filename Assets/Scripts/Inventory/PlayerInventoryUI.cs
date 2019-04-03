using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Weapon;

namespace Inventory{
public class PlayerInventoryUI : MonoBehaviour {
    
	public List<Text> items_amount_ui = new List<Text>();
	public GameObject inventory_ui;
	public UnityEngine.UI.Image highlight;
	private int selected_item;
	private float current_highlight_position;
	List<int> items_amount = new List<int>();
	void Awake() {
		for (int i = 0; i < 7; i++) {
			items_amount.Add(0);
		}
		update_inventory();
	}

	private void update_inventory()
	{
		int i = 0;
		foreach (int item_amount in items_amount) {
			items_amount_ui[i++].text = item_amount.ToString();
		}
	}

	public void update_first_aid_amount()
	{
		items_amount[0]++;
		update_inventory();
	}

	public void update_watter_bottle()
	{
		items_amount[1]++;
		update_inventory();
	}

	public void update_food_ration()
	{
		items_amount[2]++;
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
	public void open_inventory()
	{
		inventory_ui.SetActive(true);
	}

	public void close_inventory()
	{
		inventory_ui.SetActive(false);
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

