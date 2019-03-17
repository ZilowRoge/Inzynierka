using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs {
public class MobPatrol : MobBehavior {

	public bool destination_set = false;
	[Range(3.0f, 100.0f)]
	public float moving_range = 10.0f;
	public LayerMask obsticle_mask;

	public bool active_target = false;
	// Use this for initialization
	void Start () {
		movment_script = GetComponent<MobMovment>();
	}

	private void Update()
	{
		
	}
	private bool destination_reached()
	{
		return movment_script.is_target_reached();
	}

	public bool can_move_to_idle()
	{
		bool move_to_idle = destination_reached() && active_target;
		if (move_to_idle) {
			active_target = false;
		}
		return move_to_idle;
	}

	public void execute_state()
	{
		if (!active_target) {
			var new_position = get_random_point(moving_range);
			if (validate_point(new_position)) {
				//Debug.Log("Target assigned");
				target.position = new_position;
				active_target = true;
			}
		}

		if (!movment_script.is_patrol_move_active() && active_target) {
			movment_script.look_for(target);
			movment_script.activate_patrol_move();
		}
	}
	public Vector3 get_destination()
	{
		return target.position;
	}
	private bool validate_point(Vector3 point)
	{
		//Debug.DrawLine(transform.position, point, Color.red, 10.0f);
		return !Physics.Linecast(transform.position, point) &&
		       Vector3.Distance(transform.position, point) > moving_range/2;
	}

}
} //namespace speed

