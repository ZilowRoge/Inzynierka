using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mobs{
public class MobMovment : MonoBehaviour {
	public float speed;
	public float rotation_speed;

	[Range(0.5f, 5.0f)]
	public float position_offset = 1.5f;
	
	bool patrol_movment_active = false;
	bool idle_movment_active = false;
	bool chase_move_active = false;

	Transform target;
	void Start()
	{
		//rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		//TODO: Uncomment line below
		//rotate();
		if (patrol_movment_active || chase_move_active) {
			move_to_target();
		}

		if (idle_movment_active) {
			rotate_to_target();
		}
	}

	public bool is_patrol_move_active()
	{
		return patrol_movment_active;
	}

	public bool is_idle_active()
	{
		return idle_movment_active;
	}

	public bool is_chase_active()
	{
		return chase_move_active;
	}

	/*! \fn void activate_patrol_move()
	\brief activates patrol move it deactivates after reaching target
	*/
	public void activate_patrol_move()
	{
		patrol_movment_active = true;
	}

	public void deactivate_patrol_move()
	{
		patrol_movment_active = false;
		
	}

	/*! \fn void activate_patrol_move()
	\brief activates idle move it deactivates after certain time
	*/
	public void activate_idle_move()
	{
		idle_movment_active = true;
	}

	public void activate_chase_move()
	{
		chase_move_active = true;
	}

	/*! \fn void look_for(Transform target)
	\brief Turns to target and sets it
	\param target look for position
	*/
	public void look_for(Transform target)
	{
		//Debug.Log("Look_for: " + target.position);
		this.target = target;
	}

	/*! \fn void move_to_target()
	\brief moves transform for a certain step if reaches target deactivates patrol move
	*/
	public void move_to_target()
	{
		rotate_to_target();

		if(is_facing_target()) {
			transform.LookAt(target);
		}

		float step =  speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		patrol_movment_active = !is_target_reached();
		chase_move_active = !is_target_reached();
	}

	/*! \fn void move_to_target()
	\brief rotates transform  to face target position
	*/
	public void rotate_to_target()
	{
		Vector3 target_dir = target.position - transform.position;

		float step = rotation_speed * Time.deltaTime;

		Vector3 newDir = Vector3.RotateTowards(transform.forward, target_dir, step, 0.0f);
		Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);
		idle_movment_active = !is_facing_target();
	}

	/*! \fn void move_to_target()
	\returns true whent target and transform position ofset is less or equal to position_offset
	*/
	public bool is_target_reached()
	{
		return target != null && Vector3.Distance(transform.position, target.position) <= position_offset;
	}

	/*! \fn void is_facing_target()
	\returns true when the rotation between target forward and direction is less than offset
	*/
	public bool is_facing_target()
	{
		Vector3 target_dir = target.position - transform.position;
		//Debug.Log("Mob movment: target_direction: " + target_dir);
		//Debug.Log("Mob movment: forward: " + transform.forward);
		//Debug.Log("Mob movment: Angle between vectors" + Vector3.Angle(transform.forward, target_dir));

		return Vector3.Angle(transform.forward, target_dir) < 0.02f;
	}
}

} //namespace Mobs

