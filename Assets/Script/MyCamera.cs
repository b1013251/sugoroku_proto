using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour {

	//Member
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player1");
	}
	
	// Update is called once per frame
	void Update () {

		if (Sugoroku.nowPlayer != -1) {
			player = Sugoroku.players [Sugoroku.nowPlayer];
		} else {
			player = Sugoroku.players[0];
		}

		Vector3 pos = player.transform.position;
		transform.position = new Vector3 (pos.x, pos.y, -10);
	}
}
