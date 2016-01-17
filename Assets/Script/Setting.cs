using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Setting : MonoBehaviour {
	
	public static int  playerCount;
	public static string [] playersName;

	public static int totalTurn;

	/* pressed start button */
	public void StartButtonPush() {
		// get playerCount
		playerCount = GetSelectedNumber (GameObject.Find("1player").GetComponent<Toggle>().group);

		// get totalTurn
		totalTurn = GetSelectedNumber (GameObject.Find("10turn").GetComponent<Toggle>().group);

		// get player's names
		playersName = GetPlayersName (playerCount);
	//	Debug.Log ("playersName Length : " + playersName.Length.ToString());

		// save data
		PlayerPrefsX.SetStringArray ("playersName", playersName);
		PlayerPrefs.SetInt ("playerCount", playerCount);
		PlayerPrefs.SetInt ("totalTurn", totalTurn);

		// move playing game scene
		Application.LoadLevel ("Main"); // Edit
	}
	 
	/* Get int value from Toggle Group 
	 * @param "toggleGroup" playerCountToggleGroup or totalTurnToggleGroup
	 * @return int value selected playerCount or totalTurn button
	 */
	public int GetSelectedNumber(ToggleGroup toggleGroup){
		string playerCountLabel = toggleGroup.ActiveToggles()
			.First().GetComponentsInChildren<Text>()
				.First(t => t.name == "Label").text;
		Debug.Log ( "selected number : " + playerCountLabel);
		return int.Parse (playerCountLabel);
	}


	/* Get all player's name from Input Fields
	 * @param 
	 * @return array of player's name
	 */
	 public string[] GetPlayersName(int playerCount){
		// get player1InputField ~ player4InputField
		Component[] inputFields = GameObject.Find ("playerNameInpuFieldPanel").GetComponentsInChildren<InputField> ();
		string[] names =  new string[playerCount];
		for (int i=0; i<playerCount; i++) {
			names[i] = inputFields[i].GetComponent<InputField>().text;
			Debug.Log("player" + (i +1).ToString() + " : " + names[i]);
		}

		return names;
	}





}



