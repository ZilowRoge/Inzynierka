using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs {
namespace MovingGraph {
public class Node : MonoBehaviour {
	public List<GameObject> neighbour_nodes;
	public List<float> distance_between_nodes = new List<float>();

	public Node parent;

	public float g;
	public float h;
	public float f;
	
	// Use this for initialization
	void Start () {
		foreach (GameObject obj in neighbour_nodes) {
			float distance = Vector3.Distance(obj.transform.position, this.transform.position);
			distance_between_nodes.Add(distance);
		}
	}
	
	public List<Node> get_neighbours()
	{
		List<Node> nodes = new List<Node>();
		foreach (GameObject obj in neighbour_nodes) {
			nodes.Add(obj.GetComponent<Node>());
		}
		return nodes;
	}
}

} //namespace MovingGraph
} //namespace Mobs

