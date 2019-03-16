using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs {
public class MobPatrol : MobBehavior {

	public bool destination_set = false;
	[Range(3.0f, 100.0f)]
	public float moving_range = 3.0f;
	// Use this for initialization
	void Start () {
		if (movment_script == null) {
			movment_script = GetComponent<MobMovment>();
		}
	}

	public void execute_state()
	{
		if (!destination_set || destination_reached()) {
			//Debug.Log("Set destination");
			movment_script.stop();
			destination = get_new_destination();//losowe punkty powinny być w pobliżu gracza, i bedą sprawdzane ray castem czy nie jest wolna droga do nich.
			destination_set = true;
			movment_script.reset_direction();
		} else {
			movment_script.set_destionation(destination);
			movment_script.move();
		}
	}
	public bool destination_reached(string str = "MobPatrol")
	{
		Debug.Log(str + " destination reached: " + near_point(destination));
		return near_point(destination);
	}

	public Vector3 get_destination()
	{
		return destination;
	}

	private Vector3 get_new_destination()
	{
		float max_range_x = transform.position.x + moving_range;
		float min_range_x = transform.position.x + moving_range;
		float max_range_z = transform.position.z + moving_range;
		float min_range_z = transform.position.z + moving_range;

		return new Vector3(Random.Range(min_range_x, max_range_x), transform.position.y, Random.Range(min_range_z, max_range_z));
	}
}
} //namespace speed

