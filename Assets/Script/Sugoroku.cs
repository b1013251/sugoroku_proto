using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sugoroku : MonoBehaviour {

	//Static Member
	public static GameObject[] grids;
	public static GameObject[] players;
	public static int[] nowGrids = {0,0,0,0};
	public static int restTurn = 2;

	//Member
	public static GameObject turnLabel;
	public static GameObject messageLabel;
	public static GameObject messageBox;
	public static GameObject dice;

	//ControllFlags
	public static int nowPlayer = 0;
	public static bool isDice = false;
	public static bool isTurnChange = false;
	public static bool isNextPlayer = false;
	public static bool isToMoveQuestion = false;

	//Other
	private static bool duplicated = false;
	private int playersCount;
	

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
		Player[] playersArray = GameObject.Find ("Players").GetComponentsInChildren<Player> ();

		players = new GameObject[playersTransArray.Length];

		foreach (var obj in playersTransArray) {
			players [obj.GetSiblingIndex ()] = obj.gameObject;
		}
	
		for(int i = 0; i < players.Length - 1 ; i++) {
			players[i].transform.position = grids[nowGrids[i]].transform.position;
		}

		//Player Count Init（プレイヤー数を調整）
		this.playersCount = PlayerPrefs.GetInt("playerCount");
		if (!duplicated) nowPlayer = playersCount - 1; 
		for (int i = 3; i >= playersCount; i--) {
			Debug.Log ("delete" + i.ToString());
			players[i].SetActive(false);
		}

		//残りターン数を適用
		if(!duplicated) restTurn = PlayerPrefs.GetInt ("totalTurn") + 1;

		// ページ遷移されてきたらターンを動かす
		MoveNextTurn ();
		duplicated = true; //duplicateは一度だけ呼ばれる

		Debug.Log ("TTTTTTTTTTTTTTT");
		Debug.Log (isTurnChange);
		Debug.Log (isNextPlayer);
		Debug.Log (isToMoveQuestion);

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

		if (isToMoveQuestion) {
			// 出題シーンに遷移
			Application.LoadLevel("DBExample");
			isToMoveQuestion = false;
		}

		if (isNextPlayer) {
			nowPlayer = (nowPlayer + 1) % players.Length ;
			isNextPlayer = false;
			StartCoroutine ("coRoutineNextPlayer");
		}

		//ゲーム終了！
		if (restTurn <= 0) {

			//PlayerPrefs.SetInt("playerCount",playersCount);
			PlayerPrefsX.SetIntArray ("playersScore"  ,PointUpdater.playersScore);
			PlayerPrefsX.SetIntArray ("playersCorrect",PointUpdater.playersCorrect);

			StartCoroutine ("coRoutineGameOver");
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

	IEnumerator coRoutineGameOver() {
		turnLabel.GetComponent<Text> ().text = "終了";
		turnLabel.SetActive (true);
		
		yield return new WaitForSeconds(2);
		
		turnLabel.SetActive (false);
		
		Application.LoadLevel("ResultScene");
	}

	// ターン遷移
	private void MoveNextTurn() { 
		//最後のプレイヤであれば次のターン
		
		if (Sugoroku.nowPlayer == playersCount -1) {
			Sugoroku.isTurnChange = true;
		} else {
			Sugoroku.isNextPlayer = true;
		}
	}
}

/*
 * 		//プレイヤーが一周回ったら次のターン

		*/