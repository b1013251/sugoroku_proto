using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointUpdater : MonoBehaviour {
	
	public static int[] playersScore = {0,0,0,0};
	public static int[] playersCorrect = {0,0,0,0};
	
	private GameObject t1;
	private GameObject t2;
	private GameObject t3;
	private GameObject t4;

	// Use this for initialization
	void Start () {
		t1 = GameObject.Find ("Player1Text");
		t2 = GameObject.Find ("Player2Text");
		t3 = GameObject.Find ("Player3Text");
		t4 = GameObject.Find ("Player4Text");
	}
	
	// Update is called once per frame
	void Update () {
		t1.GetComponent<Text> ().text = "1P " + playersScore [0].ToString () + " Points";
		t2.GetComponent<Text> ().text = "2P " + playersScore [1].ToString () + " Points";
		t3.GetComponent<Text> ().text = "3P " + playersScore [2].ToString () + " Points";
		t4.GetComponent<Text> ().text = "4P " + playersScore [3].ToString () + " Points";
	}
}
