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
			var test_point = transform.InverseTransformPoint(new_point);
			Debug.Log(new_point);
			Debug.DrawRay(transform.position, new_point, Color.black, 10.0f);
			Debug.DrawRay(transform.position, get_destination(), Color.red, 10.0f);
			if (can_use_point(test_point)) {
				destination = new_point;
				destination_set = true;
				movment_script.reset_direction();
				movment_script.set_destionation(destination);
			}
		}
	
		movment_script.move();
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
}
} //namespace speed

