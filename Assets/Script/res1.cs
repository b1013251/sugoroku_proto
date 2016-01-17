using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class res1 : MonoBehaviour {

	private Text res;
	
	// Use this for initialization
	void Start () {
		res=GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
		string txt="回答者1："+Database.res1; 
		if(Database.Correct[0]==1) res.text=txt+" 正解";
		else if (Database.Correct[0]==0) res.text=txt+" 不正解";
		else res.text=txt;
	}
}
