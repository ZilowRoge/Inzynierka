using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Mobs;

namespace Edit {
[CustomEditor (typeof(MobController))]
public class MobControllerEditor : Editor {

	void OnSceneGUI()
	{
		MobController mob_controller = (MobController)target;
		Handles.color = Color.blue;
		if (mob_controller.patrol_script != null) {
			Handles.DrawLine(mob_controller.transform.position, mob_controller.patrol_script.get_destination());
		}

	}
}
} //namespace Editor

