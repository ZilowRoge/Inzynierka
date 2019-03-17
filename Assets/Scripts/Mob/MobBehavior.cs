using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs {
public class MobBehavior : MonoBehaviour {
	protected Vector3 destination;
	public MobMovment movment_script;
	public float location_offset = 1.5f;

	public Transform target;

	void Start()
	{
		movment_script = GetComponent<MobMovment>();
	}

	protected Vector3 get_random_point(float movment_range)
	{
		return new Vector3(Random.Range(transform.position.x - movment_range,
		                                transform.position.x + movment_range),
		                   transform.position.y,
		                   Random.Range(transform.position.z - movment_range,
		                                transform.position.z + movment_range));
	}

	public void set_destination(Vector3 dest)
	{
		destination = dest;
		//movment_script.set_destionation(destination);
	}
}

} // namespace Mobs
