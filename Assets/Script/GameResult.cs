using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameResult : MonoBehaviour {

	public static string[] playersName;
	public static int[] playersScore;
	public static int[] playersCorrect;
	public static int playerCount;

	public static int totalTurn;

	// Use this for initialization
	void Start () {
		// read data
		playersName = PlayerPrefsX.GetStringArray ("playersName");
		playersScore = PlayerPrefsX.GetIntArray ("playersScore");
		playersCorrect = PlayerPrefsX.GetIntArray ("playersCorrect");
		playerCount = PlayerPrefs.GetInt ("playerCount");
		totalTurn = PlayerPrefs.GetInt ("totalTurn");



		// set player names and scores
		setStrings (playersName, GameObject.Find ("playerName"));
		setStrings (toStringArray (playersScore), GameObject.Find ("playerScore"));
		
		// set percentage of correct anser
		setStrings (toStringArray (calcPercentage ()), GameObject.Find ("correctAnswerPercent"));
	}

	/* Set strings to scene
	 * @param array of string
	 */
	private void setStrings( string[] playerArray, GameObject labelObject){
		// get Label Object reflected strings
		Text[] texts = labelObject.GetComponentsInChildren<Text> ();
		// reflect strings
		for (int i=0; i<playerCount; i++) {
			texts [i].text = playerArray [i];
			Debug.Log(texts[i]);
		}
	}

	/* Convert int[] to string[]
	 * @param intArray[] array of integer
	 * @return array of string
	 */
	private string[] toStringArray(int[] intArray){
		string[] strArray = new string[intArray.Length];
		for (int i=0; i<intArray.Length; i++) {
			strArray [i] = intArray [i].ToString ();
		}
		return strArray;
	}

	/* Convert float[] to string[]
	 * @param floatArray[] array of float
	 * @return array of string
	 */
	private string[] toStringArray(float[] floatArray){
		string[] strArray = new string[floatArray.Length];
		for (int i=0; i<floatArray.Length; i++) {
			strArray [i] = floatArray[i].ToString("P1"); // over 0.09%
		}
		return strArray;
	}

	/*  Calculate percentage of correct answer 
	 * @return array of (paercentage of correct answer )
	 */
	private float[] calcPercentage(){
		float[] percentages = new float[playerCount];
		for (int i=0; i<playerCount; i++) {
			percentages[i] = playersCorrect[i] / (float)totalTurn * playerCount;
		}
		return percentages;
	}
}
