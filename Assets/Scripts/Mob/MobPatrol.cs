using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs {
public class MobPatrol : MobBehavior {

	public bool destination_set = false;
	public LayerMask layer_mask;
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
			destination = get_random_point(transform.position.x - 10, transform.position.x + 10,
			                               transform.position.z - 10, transform.position.z + 10);
			if (can_move_to_point(destination)) {
				//Debug.Log("Can move to point");
				destination_set = true;
			}/* else {
				Debug.Log("Cannot move to point");
			}*/
			movment_script.reset_direction();
		} else {
			movment_script.set_destionation(destination);
			movment_script.move();
		}
	}

	public bool destination_reached(string str = "MobPatrol")
	{
		//Debug.Log(str + " destination reached: " + near_point(destination));
		return near_point(destination);
	}

	public Vector3 get_destination()
	{
		return destination;
	}

	private bool can_move_to_point(Vector3 point)
	{
		RaycastHit hit;
		Vector3 direction = point - transform.position;
		float distance = Vector3.Distance(transform.position, point);
		return !(Physics.Raycast(transform.position, direction, out hit, distance, layer_mask) && hit.collider.tag == "Building");
	}
}
} //namespace speed


