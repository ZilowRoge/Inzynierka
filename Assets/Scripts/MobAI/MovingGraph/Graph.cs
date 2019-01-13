using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

	public List<GameObject> node_list;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private GameObject find_nearest(Vector3 pos)
	{
		GameObject nearest = node_list[0];
		float current_nearest_distance = Vector3.Distance(pos, nearest.transform.position);
		for(int i = 1; i < node_list.Count; i++)
		{
			float nearest_distance = Vector3.Distance(pos, node_list[i].transform.position);
			if (current_nearest_distance > nearest_distance) {
				nearest = node_list[i];
				current_nearest_distance = nearest_distance;
			}
		}

		return nearest;
	}

	public void find_path(Vector3 starting_pos, Vector3 ending_pos)
	{
		GameObject starting_node = find_nearest(starting_pos);
		GameObject ending_node = find_nearest(ending_pos);
		float euqlidian_distance = Vector3.Distance(starting_pos, ending_pos);

		List<GameObject> open_list = new List<GameObject>();
		List<GameObject> closed_list = new List<GameObject>();

		open_list.Add(starting_node);

		while (open_list.Count > 0) {
			
		}
	}


}
