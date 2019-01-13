using UnityEngine;
using System.Collections;
using UnityEditor;
using Mobs;

namespace Edit {
[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor {

	void OnSceneGUI() {
		FieldOfView fow = (FieldOfView)target;
		Handles.color = Color.white;
		Handles.DrawWireArc (fow.transform.position, Vector3.up, Vector3.forward, 360, fow.view_range);
		Vector3 viewAngleA = fow.direction_from_angle(-fow.view_angle / 2, false);
		Vector3 viewAngleB = fow.direction_from_angle(fow.view_angle / 2, false);

		Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleA * fow.view_range);
		Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleB * fow.view_range);

		Handles.color = Color.red;
		if (fow.target != null) {
			Handles.DrawLine (fow.transform.position, fow.target.position);
		}
	}
}
} //namespace Editor