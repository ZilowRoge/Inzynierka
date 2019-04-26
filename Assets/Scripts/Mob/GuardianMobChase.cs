using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mobs{
public class GuardianMobChase : MobBehavior {

	public FieldOfView mob_view;
	void Start () {
		mob_view = GetComponent<FieldOfView>();
		movment_script = GetComponent<MobMovment>();
	}
	void Update()
	{
		if (movment_script.is_chase_active()) {
			target = mob_view.target;
		}
	}
	public void execute_state()
	{
		if (!movment_script.is_chase_active() && mob_view.target != null) {
			target = mob_view.target;
			movment_script.look_for(target);
			movment_script.activate_chase_move();
		}
	}
	public bool can_change_to_patrol()
	{
		return target == null ||
		       Vector3.Distance(transform.position, target.position) > mob_view.view_range ||
		       Vector3.Distance(central_point, target.position) > radius;
	}
}
}
