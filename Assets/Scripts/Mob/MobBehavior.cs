using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs {
public class MobBehavior : MonoBehaviour {

	protected Vector3 destination;
	public MobMovment movment_script;
	public float location_offset = 1.5f;

	void Start()
	{
		if (movment_script == null) {
			movment_script = GetComponent<MobMovment>();
		}
	}

	protected Vector3 get_random_point(float min_range_x, float max_range_x, float min_range_z = -20, float max_range_z = 20)
	{
		Vector3 point = new Vector3(Random.Range(min_range_x, max_range_x), transform.position.y, Random.Range(min_range_z, max_range_z));
		//Debug.Log(point);
		return point;
	}

	protected bool near_point(Vector3 destination)
	{
		//Debug.Log(Vector3.Distance(transform.position, destination));
		return Vector3.Distance(transform.position, destination) <= location_offset;
	}

	public void set_destination(Vector3 dest)
	{
		destination = dest;
		movment_script.set_destionation(destination);
	}
}

} // namespace Mobs
