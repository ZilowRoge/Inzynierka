using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Inventory{
namespace Items{

public class AmmoBox : Item{
	public EAmmoType ammo_type;
	public int ammo_amount = Common.max_ammo;


	void Update(){
		if(ammo_amount == 0)
		{
			Destroy(gameObject);
		}
	}

	public void on_pickup()
	{
		gameObject.GetComponent<MeshRenderer>().enabled = false;
		gameObject.GetComponent<BoxCollider>().enabled = false;
	}
}

}
}