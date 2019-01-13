using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs {
public class MobPatrol : MobBehavior {

	public bool destination_set = false;

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
			destination = get_random_point(-86, 167, 96, 145);//losowe punkty powinny być w pobliżu gracza, i bedą sprawdzane ray castem czy nie jest wolna droga do nich.
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
}
} //namespace speed

