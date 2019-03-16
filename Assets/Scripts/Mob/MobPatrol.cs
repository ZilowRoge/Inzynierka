using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs {
public class MobPatrol : MobBehavior {

	public bool destination_set = false;
	[Range(3.0f, 100.0f)]
	public float moving_range = 10.0f;
	public LayerMask obstacle_mask;
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
			var new_point = get_random_point(transform.position.x - moving_range,
			                               transform.position.x + moving_range,
			                               transform.position.z - moving_range,
			                               transform.position.z + moving_range);
			//Debug.Log(new_point);
			if (can_use_point(new_point)) {
				destination = new_point;    //losowe punkty powinny być w pobliżu gracza, i bedą sprawdzane ray castem czy nie jest wolna droga do nich.
				destination_set = true;
				movment_script.reset_direction();
			}
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
	private bool can_use_point(Vector3 point)
	{
		float distance_to_target = Vector3.Distance (transform.position, point);
		Vector3 direction_to_target = (point - transform.position).normalized;
		bool test = Physics.Raycast (transform.position, direction_to_target, distance_to_target, obstacle_mask);
		Debug.Log("Raycast hit obsticle: " + test);
		return !test;
	}
	public Vector3 get_random_point(float min_range_x, float max_range_x, float min_range_z = -20, float max_range_z = 20)
	{
		Vector3 random_point = new Vector3(Random.Range(min_range_x, max_range_x), transform.position.y, Random.Range(min_range_z, max_range_z));
		
		
		
		
		return random_point;
	}
}
} //namespace speed

