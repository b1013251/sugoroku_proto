using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class res2 : MonoBehaviour {

	private Text res;
	
	// Use this for initialization
	void Start () {
		res=GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
		string txt="回答者2："+Database.res2; 
		if(Database.Correct[1]==1) res.text=txt+" 正解";
		else if (Database.Correct[1]==0) res.text=txt+" 不正解";
		else res.text=txt;
	}
}
