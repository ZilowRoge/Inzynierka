using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
namespace Inventory{
public class PlayerInventoryUI : MonoBehaviour {
    
	public List<Text> items_viewd = new List<Text>();
	public GameObject inventory_ui;
	public PlayerInventory inventory;
	public UnityEngine.UI.Image highlight;

	private Text item;
	private int selected_item;
	private float current_highlight_position;
    // Use this for initialization
    void Start () {
		for(int i = 0; i < items_viewd.Count; i++)
		{
			items_viewd[i].text = "";
		}
		highlight.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void on_ui_open()
	{
		int items_in_inventory_count = inventory.items_in_backpack.Count;
		inventory_ui.SetActive(true);

		if (items_in_inventory_count > 0) {
			highlight.enabled = true;
			set_highlight_position(17); //nie wiem czy stałej tu nie dać, bedzie sie zmieniało o 3
			selected_item = 0;
		}

		update_inventory();
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

	private void set_highlight_position(float pos_y)
	{
		highlight.GetComponent<RectTransform>().localPosition = new Vector3(0,pos_y,0);
		current_highlight_position = pos_y;
	}
	
	
}
}

