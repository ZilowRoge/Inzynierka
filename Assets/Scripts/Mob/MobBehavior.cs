using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs {
public class MobBehavior : MonoBehaviour {
	protected Vector3 destination;
	public MobMovment movment_script;
	public float location_offset = 1.5f;

	public Transform target = null;
	public Vector3 central_point;
	public float radius;

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

	protected Vector3 get_random_point(Vector3 point, float radius)
	{
		float random = Random.Range(0.0f, 1.0f);
		float a = random * 2 * 3.14f;
		float r = radius * Mathf.Sqrt(random);
		float x = r * Mathf.Cos(a) + point.x;
		float z = r * Mathf.Sin(a) + point.z;
		//Debug.Log("random: " + random + " a: " + " r: " + r + " x: " + x + " z: " + z);
		return new Vector3(x, 1, z);
	}

	public void set_destination(Vector3 dest)
	{
		destination = dest;
		//movment_script.set_destionation(destination);
	}
}

} // namespace Mobs
