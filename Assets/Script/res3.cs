using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class res3 : MonoBehaviour {

	private Text res;
	
	// Use this for initialization
	void Start () {
		res=GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
		string txt="回答者3："+Database.res3; 
		if(Database.Correct[2]==1) res.text=txt+" 正解";
		else if (Database.Correct[2]==0) res.text=txt+" 不正解";
		else res.text=txt;
	}
}
