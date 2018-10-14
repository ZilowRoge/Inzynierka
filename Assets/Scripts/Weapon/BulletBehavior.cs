using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Target;

namespace Weapon{
public class BulletBehavior : MonoBehaviour {

	public int max_collision_count = 1;
	public int current_collision_count = 0;

	float live_time = 0;

	float max_live_time = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(max_live_time > live_time)
		{
			live_time += Time.deltaTime;
		}
		if(live_time >= max_live_time){
			live_time = 0;
			Destroy(this.gameObject);
		
		}
	}

	void OnCollisionEnter(Collision other)
	{
		current_collision_count++;
		if(other.collider.tag == "Target")
		{
			other.transform.parent.gameObject.GetComponent<TargetBehavior>().on_hit();
		}
		if(current_collision_count >= max_collision_count)
		{
			GameObject.Destroy(this.gameObject);
		}
		
	}
}

} //namespace Weapon
