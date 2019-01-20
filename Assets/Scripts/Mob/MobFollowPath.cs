using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mobs.MovingGraph;
namespace Mobs{
public class MobFollowPath : MobBehavior {

	public Graph graph;
	public List<Node> path = new List<Node>();
	public bool path_found = false;
	public bool destination_set = false;
	public int node_index = 0;
	// Use this for initialization
	void Start () {
		if (movment_script == null) {
			movment_script = GetComponent<MobMovment>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.T)){
			Debug.Log("KeyPressed");
			StartCoroutine("find_path", new Vector3(-23.9f, 0, 67.6f));
		}
	}

	public void execute_state()
	{
		if (!destination_set || destination_reached()) {
			Debug.Log("Set destination " + path.Count);
			movment_script.stop();
			destination = path[node_index].transform.position;
			node_index++;
			destination_set = true;
			movment_script.reset_direction();
		} else {
			movment_script.set_destionation(destination);
			movment_script.move();
		}
	}

	public bool destination_reached()
	{
		return near_point(destination);
	}
	public bool is_path_found()
	{
		return path_found;
	}
	public IEnumerator find_path(Vector3 ending_pos)
	{
		Node starting_node = graph.find_nearest(transform.position).GetComponent<Node>();
		Node ending_node = graph.find_nearest(ending_pos).GetComponent<Node>();
		//float euqlidian_distance = Vector3.Distance(transform.position, ending_pos);
		Debug.Log("End node = " + ending_node.gameObject.transform.position);
		List<Node> open_list = new List<Node>();
		List<Node> closed_list = new List<Node>();
		open_list.Add(starting_node);

		while (open_list.Count > 0) {
			int current_index = 0;
			Node current_node = open_list[current_index];

			int index = 0;
			foreach (Node n in open_list) {
				if (n.f < current_node.f) {
					current_node = n;
					current_index = index;
				}
				index++;
			}

			open_list.RemoveAt(current_index);
			closed_list.Add(current_node);
			//Debug.Log("Current node = " + current_node.gameObject.transform.position);

			if (Vector3.Distance(current_node.transform.position,
			                     ending_node.transform.position) == 0) {
				path = closed_list;
				foreach (Node elem in path) {
					Debug.Log(elem.gameObject.name + ":" + elem.gameObject.transform.position);
				}
				path_found = true;
				break;
			}

			index = 0;
			List<Node> children = current_node.get_neighbours();
			foreach (Node child in children) {
				if (!closed_list.Contains(child)) {
					child.g = current_node.g + 1;//current_node.distance_between_nodes[index];
					child.h = Vector3.Distance(ending_node.gameObject.transform.position,
					                           child.gameObject.transform.transform.position);
					child.f = child.g + child.h;
				//	Debug.Log("Distance betwen " + child.gameObject.name + " and " + ending_node.gameObject.name + " = " + child.h);
					if (!open_list.Contains(child)) {
						open_list.Add(child);
					}

				}
			}
			/*foreach (Node elem in closed_list) {
				Debug.Log(elem.gameObject.name + ":" + elem.gameObject.transform.position);
			}*/
			yield return null; 
		}
	}
}
} //namespace Mobs