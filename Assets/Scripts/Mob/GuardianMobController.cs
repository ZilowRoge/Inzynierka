using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs {
public class GuardianMobController : MonoBehaviour {
	public enum MobBehaviorState {
		EPATROL,
		EIDLE,
		ECHASE,
	}
	public MobBehaviorState state = MobBehaviorState.EPATROL;

	public GuardianMobPatrol patrol_script;
	public MobIdle idle_script;
	public GuardianMobChase chase_script;
	public Animator animator;
	public MobStats mob_stats;

	public FieldOfView mob_view;
	// Use this for initialization
	void Awake () {
		mob_stats = GetComponent<MobStats>();
		patrol_script = GetComponent<GuardianMobPatrol>();
		idle_script = GetComponent<MobIdle>();
		chase_script = GetComponent<GuardianMobChase>();
		mob_view = GetComponent<FieldOfView>();
		animator = GetComponent<Animator>();
		animator.speed = 0.75f;/*attack speed */
		StartCoroutine ("wake_mob", .2f);
	}

	void Update() 
	{
		try_attack();

		if (mob_stats.health <= 0) {
			Destroy(this.gameObject);
		}
	}

	IEnumerator wake_mob(float delay)
	{
		while(true) {
		//	Debug.Log("wake_mob");
			yield return new WaitForSeconds (delay);
			mob_state_machine();
		}
	}

	void mob_state_machine()
	{
		//patrol_script.set_movment_script_active(false);
		switch(state)
		{
			case MobBehaviorState.EPATROL:
				patrol();
				break;
			case MobBehaviorState.ECHASE:
				chase();
				break;
			case MobBehaviorState.EIDLE:
				idle();
				break;
			default:
				break;
		}
	}
	void change_state(MobBehaviorState state)
	{
		//Debug.Log("Mob Controller: Change state to: " + state.ToString());
		this.state = state;
	}
	void patrol ()
	{
		//Debug.Log("Patrol");
		if (player_spotted()) {
			change_state(MobBehaviorState.ECHASE);
			patrol_script.stop_execution();
		}
		if (patrol_script.can_change_to_idle()) {
			change_state(MobBehaviorState.EIDLE);
		} else if(!player_spotted()) {
			patrol_script.execute_state();
		}
	}

	void idle()
	{
		idle_script.execute_state();
		if (player_spotted()) {
			change_state(MobBehaviorState.ECHASE);
		}
		if (idle_script.timer_less_than_zero()) {
			//Debug.Log("Should end idle");
			change_state(MobBehaviorState.EPATROL);
		}
	}

	void chase()
	{
		if (chase_script.can_change_to_patrol() && !player_spotted()) {
			change_state(MobBehaviorState.EPATROL);
		} else {
			chase_script.execute_state();
		}
	}
	
	void try_attack()
	{
		if (is_target_in_range())
		{
			Debug.Log("Start animation");
			animator.SetBool("Attack", true);
		} else {
			animator.SetBool("Attack", false);
		}
	}

	private bool player_spotted()
	{
		return mob_view.target != null;
	}
	private bool is_target_in_range()
	{
		//Debug.Log("Distance : " + Vector3.Distance(mob_view.target.position, transform.position));
		return mob_view.target != null && Vector3.Distance(mob_view.target.position, transform.position) <= 2.5f;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Bullet") {
			mob_stats.health -= other.collider.gameObject.GetComponent<Weapon.BulletBehavior>().get_damage();
			Debug.Log("Health left: " + mob_stats.health);
		}
	}
}

} //namespace Mobs