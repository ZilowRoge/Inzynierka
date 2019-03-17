using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs {
public class MobIdle : MobBehavior {
	public float idle_timer = 0.0f;

	[Range(3.0f, 100.0f)]
	public float point_range = 20.0f;

	public bool rotation_point_set = false;
	void Start()
	{
		movment_script = GetComponent<MobMovment>();
	}

	void Update()
	{
		if (!timer_less_than_zero()) {
			idle_timer -= Time.deltaTime;
		}
	}
	public bool timer_less_than_zero()
	{
		return idle_timer <= 0;
	}
	public void execute_state()
	{
		if (timer_less_than_zero() && !rotation_point_set) {
			idle_timer = Random.Range(2.0f, 2.75f);
		}
		if (!rotation_point_set) {
			Vector3 new_point = get_random_point(point_range);
			if (validate_point(new_point)) {
				target.position = new_point;
				rotation_point_set = true;
			}
		}

		if (!movment_script.is_idle_active()) {
			movment_script.look_for(target);
			movment_script.activate_idle_move();
		} else if (movment_script.is_facing_target()) {
			rotation_point_set = false;
		}
	}

	private bool validate_point(Vector3 point)
	{
		Vector3 direction = transform.position - point;
		return Vector3.Angle(transform.forward, direction) > 60 &&
		       Vector3.Distance(transform.position, point) > point_range*0.75;
	}
}
} // namespace Mobs