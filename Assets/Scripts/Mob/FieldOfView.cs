using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mobs {
public class FieldOfView : MonoBehaviour {
	public float view_range;

	[Range(0,360)]
	public float view_angle;

	public LayerMask target_mask;
	public LayerMask obstacle_mask;

	public Transform target;

	void Start() {
		StartCoroutine ("find_targets_with_delay", .2f);
	}


	IEnumerator find_targets_with_delay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			find_targets ();
		}
	}

	public void find_targets()
	{
		target = null;
		Collider[] targets_in_view = Physics.OverlapSphere(transform.position, view_range, target_mask);

		
		for (int i = 0; i < targets_in_view.Length; i++) {
			Transform target = targets_in_view [i].transform;
			Vector3 direction_to_target = (target.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, direction_to_target) < view_angle / 2) {
				float distance_to_target = Vector3.Distance (transform.position, target.position);

				if (!Physics.Raycast (transform.position, direction_to_target, distance_to_target, obstacle_mask)) {
					//Handle if target was seen
					//Debug.Log("Target found: " + target.position);
					this.target = target;
				}
			}
		}
	}
	public Vector3 direction_from_angle(float angle, bool angle_is_global)
	{
		if (!angle_is_global) {
			angle += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
	}
	
}
} //namespace Mobs

