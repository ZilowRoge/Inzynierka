using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Target {
public class ManageTargets : MonoBehaviour {

	int fisrt_iterator = 0;
	int second_iterator = 0;
	List<List<TargetBehavior>> targets_scripts;
	// Use this for initialization
	void Start () {
		targets_scripts = new List<List<TargetBehavior>>(transform.childCount);
		for(int i = 0; i < transform.childCount; i++){
			Transform child = transform.GetChild(i);
			targets_scripts.Add(new List<TargetBehavior>(child.childCount));
			for(int j = 0; j < child.childCount; j++){
				targets_scripts[i].Add(child.GetChild(j).GetComponent<TargetBehavior>());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void stand_all()
	{
		for(int i = 0; i < targets_scripts.Count; i++)
			for(int j = 0; j < targets_scripts[i].Count; j++)
				targets_scripts[i][j].on_stand();
	}
}

} //namespace Target
