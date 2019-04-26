using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player{
public class PlayerStats : MonoBehaviour {
	public float max_health_points;
	public float curren_health_points;
	public float max_hunger;
	public float current_hunger;
	public float max_thirst;
	public float current_thirst;

	public float max_weight = 20;

	public float max_speed = 20;

	float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (current_hunger > -100 ) {
			current_hunger -= Common.hunger_drop * Time.deltaTime;
		} else {
			curren_health_points -= 0.5f;
		}
		if (current_thirst > -100) {
			current_thirst -= Common.fast_thirst_drop * Time.deltaTime;
			max_speed = 20;
		} else {
			max_speed = 10;
		}
	}
/*Jezeli pragnienie spada x na sec to ile spada w czasie t
 x - 1s
 y - t
 y = x * t */

	public void eat(float amount)
	{
		current_hunger += amount;
		if (current_hunger > 0) {
			current_hunger = 0;
		}
	}

	public void dring(float amount)
	{
		current_thirst += amount;
		if (current_thirst > 0) {
			current_thirst = 0;
		}
	}

	public void heal(float amount)
	{
		curren_health_points += amount;
	}

}
} //namespace Player
