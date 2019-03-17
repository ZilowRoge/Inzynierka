using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mobs {
public class MobStats : MonoBehaviour {

	[Range(200, 2000)]
	public float health;
	[Range(2.0f, 100.0f)]
	public float damage;
	[Range(0.1f, 5.0f)]
	public float attack_speed;
}
}//namespace Mobs