using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class testScript : MonoBehaviour {

	private Text test;

	// Use this for initialization
	void Start () {
		test=GetComponent<Text>();
		Database.new_test();
	}
	
	// Update is called once per frame
	void Update () {
		test.text=Database.test;
	}
}
