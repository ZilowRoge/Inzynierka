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

	public void execute_state()
	{
		/* TODO REWRITE THIS
		if (!reached_target()) {
			movment_script.set_destionation(destination);
			movment_script.move();
		}*/
	}

}
} //namespace Mobs
