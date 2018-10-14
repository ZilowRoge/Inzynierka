using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inventory {
namespace Items {
public class Item : MonoBehaviour {

	public string name;
	public float weight;
	public GameObject model;

	public void on_pickup()
	{
		gameObject.GetComponent<MeshRenderer>().enabled = false; //butelke trzeba bedzie zrobić jednego mesh'a (najlepiej w blenderze)
		gameObject.GetComponent<Collider>().enabled = false;
	}

}
}//namespace Items
}//namespace Inventory

