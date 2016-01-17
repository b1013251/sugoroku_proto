using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dice : MonoBehaviour {
	//members
	public int number;
	public Sprite[] diceImages;
	public GameObject sugoroku;
	public GameObject player;

	//flags
	public bool rolling;


	// Use this for initialization
	void Start () {
		sugoroku = GameObject.Find ("Sugoroku");
	}
	
	// Update is called once per frame
	void Update () {
		//change image
		if (rolling == true) {
			number = Random.Range(1,6);
			GetComponent<Image>().sprite = diceImages[number - 1];
		}

		//stop by space key
		if (rolling == true && Input.GetKeyDown (KeyCode.Space)) {
			rolling = false;
			player.SendMessage("Move",this.number);
		}
	}

	void Roll(GameObject player) {
		rolling = true;
		this.player = player;
	}
}
