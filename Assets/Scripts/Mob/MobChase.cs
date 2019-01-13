using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs{
public class MobChase : MobBehavior {
	// Use this for initialization
	public FieldOfView mob_view;
	public GameObject attack_target;
	void Start () {
		mob_view = GetComponent<FieldOfView>();
		if (movment_script == null) {
			movment_script = GetComponent<MobMovment>();
		}
	}

	public bool reached_target()
	{
		return near_point(destination);
	}
	public void execute_state()
	{
		if (!reached_target()) {
			movment_script.set_destionation(destination);
			movment_script.move();
		}
	}

	public bool destination_reached(string str = "MobChase")
	{
	//	Debug.Log(str + "destination reached: " + near_point(destination));
		return near_point(destination);
	}

}
} //namespace Mobs
