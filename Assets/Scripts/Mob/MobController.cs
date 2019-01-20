using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mobs {
public class MobController : MonoBehaviour {
	public enum MobBehaviorState {
		EPATROL,
		EIDLE,
		ECHASE,
		EFOLLOWPATH
	}
	public MobBehaviorState state = MobBehaviorState.EPATROL;
	public MobPatrol patrol_script;
	public MobIdle idle_script;
	public MobChase chase_script;
	public MobFollowPath follow_path_script;
	public Animator animator;
	public GameObject attack_target;
	public FieldOfView mob_view;

	// Use this for initialization
	void Start () {
		patrol_script = GetComponent<MobPatrol>();
		idle_script = GetComponent<MobIdle>();
		chase_script = GetComponent<MobChase>();
		follow_path_script = GetComponent<MobFollowPath>();
		mob_view = GetComponent<FieldOfView>();
		animator = GetComponent<Animator>();
		animator.speed = 0.75f;/*attack speed */
		StartCoroutine ("wake_mob", .2f);
	}

	void Update() 
	{
		if(player_spotted())
		{
			patrol_script.set_destination(mob_view.target.position);
			idle_script.set_destination(mob_view.target.position);
			chase_script.set_destination(mob_view.target.position);
		}
		try_attack();

	}

	

	IEnumerator wake_mob(float delay)
	{
		while(true) {
			//Debug.Log("wake_mob");
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
			case MobBehaviorState.EFOLLOWPATH:
				follow_path();
				break;
			default:
				break;
		}
	}
	void change_state(MobBehaviorState state)
	{
		//Debug.Log("Change state to: " + state.ToString());
		this.state = state;
	}
	void patrol ()
	{
		if (follow_path_script.is_path_found()) {
			change_state(MobBehaviorState.EFOLLOWPATH);
		} else {
			if (player_spotted()) {
				change_state(MobBehaviorState.ECHASE);
			}
			if (patrol_script.destination_reached("MobController") &&
			    patrol_script.destination_set) {
				//Debug.Log("Set Idle state");
				change_state(MobBehaviorState.EIDLE);
				patrol_script.destination_set = false;
			} else {
				//Debug.Log("Execute patrol state");
				patrol_script.execute_state();
			}
		}
	}

	void idle()
	{
		if (follow_path_script.is_path_found()) {
			change_state(MobBehaviorState.EFOLLOWPATH);
		} else {
			if (player_spotted()) {
				change_state(MobBehaviorState.ECHASE);
			}
			if (idle_script.should_end_idle && idle_script.timer_less_than_zero()) {
				//Debug.Log("Should end idle");
				idle_script.should_end_idle = false;
				change_state(MobBehaviorState.EPATROL);
			} else {
				//Debug.Log("Execute state IDLE");
				idle_script.execute_state();
			}
		}
	}

	void follow_path()
	{
		if (player_spotted()) {
			change_state(MobBehaviorState.ECHASE);
		} else {
			follow_path_script.execute_state();
		}
	}

	void chase()
	{
		if (chase_script.destination_reached()) {
			change_state(MobBehaviorState.EPATROL);
		}
		chase_script.execute_state();
	}
	
	void try_attack()
	{
		targets_in_attack_range();
		if (attack_target != null)
		{
			animator.SetBool("Attack", true);
		} else {
			animator.SetBool("Attack", false);
		}
	}

	private bool player_spotted()
	{
		return mob_view.target != null;
	}
	private void targets_in_attack_range()
	{
		attack_target = null;
		Collider[] targets = Physics.OverlapSphere(transform.position, 1.5f/*attack range*/, mob_view.target_mask);
		foreach(Collider col in targets)
		{
			if (col.gameObject.tag == "Player") {
				attack_target = col.gameObject;
				break;
			}
		}
	}

}

} //namespace Mobs