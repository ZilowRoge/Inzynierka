using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Target{
public class TargetBehavior : MonoBehaviour {

	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void on_hit()
	{
		animator.SetTrigger("down_trigger");
	}

	public void on_stand()
	{
		animator.SetTrigger("up_trigger");
	}
}

} //namespace Target
