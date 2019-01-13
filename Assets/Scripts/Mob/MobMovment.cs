using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mobs{
public class MobMovment : MonoBehaviour {
	public float speed;
	public float rotation_speed;
	public float max_speed;
	public Rigidbody rigidbody;
	public Vector3 direction;
	public Vector3 destination;
	public float angle;

	private float angle_offset = 51;
	private bool is_script_active = false;
	private bool direction_found = false;
	public void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	public void Update()
	{
		rotate();
	}
	public void move()
	{
		if (!direction_found) {
			find_direction();
		}
		if (!should_rotate()) {
			direction_found = true;
			rigidbody.AddForce(direction.normalized * speed);
			set_max_speed(max_speed);
		}
	}

	public void rotate()
	{
		if(should_rotate()) {
			if(angle > 0) {
				transform.Rotate(0, rotation_speed, 0);
			} else if (angle < 0) {
				transform.Rotate(0, -rotation_speed, 0);
			}
			//Debug.Log("Movment script active");
		}
	}

	public bool should_rotate()
	{
		angle = Vector3.SignedAngle(new Vector3(transform.forward.x, transform.position.y, transform.forward.z), direction, Vector3.up);
		
		return angle >= angle_offset || angle <= -angle_offset;
	}

	public void stop()
	{
		rigidbody.velocity = Vector3.zero;
	}

	public void set_active(bool active)
	{
		is_script_active = active;
	}

	public void set_destionation(Vector3 dest)
	{
		destination = dest;
		find_direction();
	}

	public void set_point_to_rotate(Vector3 destination)
	{
		direction = destination - new Vector3(transform.position.x, transform.position.y, transform.position.z);
		angle = Vector3.SignedAngle(new Vector3(transform.forward.x, transform.position.y, transform.forward.z), direction, Vector3.up);
	}

	public void reset_direction()
	{
		direction_found = false;
	}

	private void set_max_speed(float max_speed)
	{
		if(rigidbody.velocity.magnitude > max_speed) {
			rigidbody.velocity = rigidbody.velocity.normalized * max_speed;
		}
	}

	private void find_direction()
	{
		direction = destination - new Vector3(transform.position.x, transform.position.y, transform.position.z);
		angle = Vector3.SignedAngle(new Vector3(transform.forward.x, transform.position.y, transform.forward.z), direction, Vector3.up);
		Debug.Log("Angle: " + angle);
	}
}

} //namespace Mobs

