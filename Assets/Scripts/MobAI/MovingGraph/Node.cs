using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobAI {
namespace MovingGraph {
public class Node : MonoBehaviour {
	public List<GameObject> neighbour_nodes;
	public List<float> distance_between_nodes = new List<float>();
	
	// Use this for initialization
	void Start () {
		foreach (GameObject obj in neighbour_nodes) {
			float distance = Vector3.Distance(obj.transform.position, this.transform.position);
			distance_between_nodes.Add(distance);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

} //namespace MovingGraph
} //namespace MobAI

