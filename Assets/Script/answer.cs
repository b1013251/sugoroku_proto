using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class answer : MonoBehaviour {
	
	private Text choice;
	
	// Use this for initialization
	void Start () {
		choice=GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Database.res1=="" || Database.ansflg!=1) choice.text="";
		else choice.text="正解："+Database.answer;
	}
}
