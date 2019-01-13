using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player{
public class PlayerMovment : MonoBehaviour {

	public GameObject player_object;

	public GameObject player_body;

	public Rigidbody player_rigidbody;

	public float force_multiplayer = 400;

	public float jump_force = 3200;

	public float max_speed = 20;

	public float max_sneak_speed = 6;

	public float current_speed;

	public bool can_jump = true;

	public bool sneak = false;

	private void Start()
	{
		current_speed = max_speed;
		player_rigidbody = this.GetComponent<Rigidbody>();
	}
	
	private void Update()
	{
		limit_player_speed();
		set_body_rotation();
	}

	private void set_body_rotation()
	{
		Quaternion rotation = Camera.main.transform.rotation;
		player_body.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
	}

	private void limit_player_speed()
	{
		if (player_rigidbody.velocity.magnitude > max_speed) {
			player_rigidbody.velocity = player_rigidbody.velocity.normalized * max_speed;
		}
	}

	public void move_forward()
	{
		player_rigidbody.AddForce(player_body.transform.forward * force_multiplayer * Time.deltaTime);
	}

	public void move_backward()
	{
		player_rigidbody.AddForce(-player_body.transform.forward * force_multiplayer * Time.deltaTime);
	}

	public void move_left()
	{
		player_rigidbody.AddForce(player_body.transform.right * force_multiplayer * Time.deltaTime);
	}

	public void move_right()
	{
		player_rigidbody.AddForce(-player_body.transform.right * force_multiplayer * Time.deltaTime);
	}

	public void change_sneak()
	{
		sneak = !sneak;
		if (sneak) {
			current_speed = max_sneak_speed;
		} else {
			current_speed = max_speed;
		}
	}
	
	public void jump()
	{
		if (can_jump) {
			Debug.Log("Jumped");
			player_rigidbody.AddForce(Vector3.up * jump_force * Time.deltaTime);
			can_jump = false;
		}
	}

	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		if (!can_jump && other.collider.gameObject.tag.Equals("Ground")) {
			Debug.Log("Touched the ground");
			can_jump = true;
		}
	}
}

} //namespace Player

