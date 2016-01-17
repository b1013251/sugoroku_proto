using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class res4 : MonoBehaviour {

	private Text res;
	
	// Use this for initialization
	void Start () {
		res=GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
		string txt="回答者4："+Database.res4; 
		if(Database.Correct[3]==1) res.text=txt+" 正解";
		else if (Database.Correct[3]==0) res.text=txt+" 不正解";
		else res.text=txt;
	}
}
