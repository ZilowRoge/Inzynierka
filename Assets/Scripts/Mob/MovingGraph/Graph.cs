using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mobs {
namespace MovingGraph {
public class Graph : MonoBehaviour {

	public List<GameObject> node_list;
	public GameObject find_nearest(Vector3 pos)
	{
		Debug.Log("Start");
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
}

} //namespace MovingGraph
} //namespace Mobs