using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sugoroku : MonoBehaviour {

	//Static Member
	public static GameObject[] grids;
	public static GameObject[] players;
	public static int restTurn = 21;

	//Member
	public GameObject turnLabel;
	public GameObject messageLabel;
	public GameObject messageBox;
	public GameObject dice;

	//ControllFlags
	public static int nowPlayer = 0;
	public static bool isDice = false;
	public static bool isTurnChange = false;
	public static bool isNextPlayer = false;


	// Use this for initialization
	void Start () {

		// TurnLabel Init
		turnLabel = GameObject.Find ("TurnLabel");
		turnLabel.SetActive (false);

		// Dice Init
		dice = GameObject.Find ("Dice");

		//MessageBox Init
		messageLabel = GameObject.Find ("Message");
		messageBox   = GameObject.Find ("MessageBox");
		messageLabel.SetActive (false);
		messageBox.SetActive (false);


		//Grid Init
		Transform[] gridsTransArray = GameObject.Find ("Grids").GetComponentsInChildren<Transform> ();
		grids = new GameObject[gridsTransArray.Length];
		foreach (var obj in gridsTransArray) {
			grids[obj.GetSiblingIndex()  ] = obj.gameObject;
		}

		//Player Init
		Transform[] playersTransArray = GameObject.Find ("Players").GetComponentsInChildren<Transform> ();
		players = new GameObject[playersTransArray.Length];
		foreach (var obj in playersTransArray) {
			players[obj.GetSiblingIndex()  ] = obj.gameObject;
		}

		//First Flags;
		nowPlayer = 0;
		isTurnChange = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isTurnChange) {
			isTurnChange = false;
			StartCoroutine ("coRoutineTurnChange");
		}

		if (isDice) {
			isDice = false;
		}

		if (isNextPlayer) {
			nowPlayer = (nowPlayer + 1) % players.Length ;
			isNextPlayer = false;
			StartCoroutine ("coRoutineNextPlayer");
		}

	}
	
	IEnumerator coRoutineTurnChange() {
		restTurn--;
		nowPlayer = -1; //

		turnLabel.GetComponent<Text> ().text = "残り " + restTurn.ToString() + " ターン";
		turnLabel.SetActive (true);

		yield return new WaitForSeconds(1);
	
		turnLabel.SetActive (false);

		isNextPlayer = true;
	}

	IEnumerator coRoutineNextPlayer() {
		
		turnLabel.GetComponent<Text> ().text = "プレイヤー " + (nowPlayer + 1).ToString() + " の番";
		turnLabel.SetActive (true);
		
		yield return new WaitForSeconds(1);
		
		turnLabel.SetActive (false);
		dice.SendMessage ("Roll", players[nowPlayer]);
	}
}

/*
 * 		//プレイヤーが一周回ったら次のターン

		*/