using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs {
public class MobIdle : MobBehavior {
	public float idle_timer = 0.0f;
	public bool should_end_idle = false;
	void Start()
	{
		if (movment_script == null) {
			movment_script = GetComponent<MobMovment>();
		}
	}
	public bool timer_less_than_zero()
	{
		return idle_timer <= 0;
	}
	public void execute_state()
	{
		if (timer_less_than_zero()) {
			idle_timer = Random.Range(0.5f, 2);
			should_end_idle = true;
		} else {
			if (!movment_script.should_rotate()) {
				//maybe add here rotation speed setter
				destination = get_random_point(-86, 167, 96, 145);
				movment_script.set_point_to_rotate(destination);
			}
			idle_timer -= Time.deltaTime;
		}
	}
}
} // namespace Mobs