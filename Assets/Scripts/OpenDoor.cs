using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {
	GameObject mob1;
	GameObject mob2;
	GameObject mob3;
	GameObject mob4;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if(mob1 == null && mob2 == null && mob3 == null && mob4 == null && transform.position.x < 500.0f)
			open_door();
	}

	void open_door()
	{
		transform.position = new Vector3(transform.position.x + 5 * Time.deltaTime, transform.position.y, transform.position.z);
	}
}
