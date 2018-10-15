

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;

namespace Player {
public class PlayerUI : MonoBehaviour {
	public PlayerStats player_statisctic;
	public List<Text> list_of_texts = new List<Text>();

	public List<Slider> status_bars = new List<Slider>();


	// Use this for initializations
	void Start () {
		player_statisctic = gameObject.GetComponent<PlayerStats>();
		
		status_bars[0].value = player_statisctic.curren_health_points;
		status_bars[1].value = player_statisctic.current_hunger;
		status_bars[2].value = player_statisctic.current_thirst;
	}
	
	// Update is called once per frame
	void Update () {
		/*for(int i = 0; i < 3; i++)
		{
			list_of_texts[i].text = inventory.get_ammo_amount(i) + "";
		}*/

		status_bars[0].value = player_statisctic.curren_health_points;
		status_bars[1].value = player_statisctic.current_hunger;
		status_bars[2].value = player_statisctic.current_thirst;
	}
}
} //namespace Player
