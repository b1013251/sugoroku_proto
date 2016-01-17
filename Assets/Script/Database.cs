using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Database : MonoBehaviour {
	public static string ct,test="問題",a="選択肢1",i="選択肢2",u="選択肢3",e="選択肢4",answer="",res1="",res2="",res3="",res4="";
	public static int  playerCount=4, ansflg=0 ,j;
	public static int[] resflg={0,0,0,0}, Correct={-1,-1,-1,-1}; 
	// Use this for initialization
	void Start () {
		//変数初期化
		for(int i=0;i<4;i++) resflg[i]=0;
		ansflg=0;Correct[0]=-1; Correct[1]=-1; Correct[2]=-1; Correct[3]=-1;
		res1="";res2="";res3="";res4="";
		if(PlayerPrefs.HasKey("playerCount")) playerCount = PlayerPrefs.GetInt("playerCount");//プレイヤー人数取得
		// Select 
		SqliteDatabase sqlDB = new SqliteDatabase("question");
		string selectQuery = "select * from FE";
		DataTable dataTable = sqlDB.ExecuteQuery(selectQuery);	
		Debug.Log ("テスト");
		string question = "";
		foreach(DataRow dr in dataTable.Rows){
			Debug.Log ("ID:" + dr["ID"].ToString());
			question = (string)dr["Question"];
			Debug.Log ("Question:" + question);	
			Debug.Log ("選択肢ア:" + (string)dr["Choice_a"]);	
			Debug.Log ("選択肢イ:" + (string)dr["Choice_i"]);	
			Debug.Log ("選択肢ウ:" + (string)dr["Choice_u"]);	
			Debug.Log ("選択肢エ:" + (string)dr["Choice_e"]);	
			Debug.Log ("正解:" + (string)dr["Answer"]);	
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(ansflg==0) response();

		else if(Input.GetKeyUp(KeyCode.Return)) {
			Pointing();//点数付け処理
			Debug.Log("うぇえええええええええええええええええええええええええええ");
			Application.LoadLevel("DBexample");//画面遷移
		}
	}

	public static void new_test(){
		res1="";
		Correct[0] =-1;
		SqliteDatabase sqlDB = new SqliteDatabase("question");
		//コラム数を取得
		string query = "select count (*) as ct from FE";
		DataTable dT =sqlDB.ExecuteQuery(query);
		foreach(DataRow dr in dT.Rows)		ct=dr["ct"].ToString();
		//ランダムに１問抽出
		int random = Random.Range ((int)1, int.Parse(ct)+1);
		string rq = "select * from FE where ID = " + random.ToString();
		dT =sqlDB.ExecuteQuery(rq);
		foreach(DataRow dr in dT.Rows){
			test="問題：　"+(string)dr["Question"];
			a="ア：　"+(string)dr["Choice_a"];
			i="イ：　"+(string)dr["Choice_i"];
			u="ウ：　"+(string)dr["Choice_u"];
			e="エ：　"+(string)dr["Choice_e"];
			answer=(string)dr["Answer"];
		}
	}

	public static void 	response(){
		//キーボードの文字列判定
		if (Input.GetKey(KeyCode.Alpha1)) {res1="ア"; resflg[0]=1;}
		else if (Input.GetKey(KeyCode.Alpha2)) {res1="イ";resflg[0]=1;}
		else if (Input.GetKey(KeyCode.Alpha3)) {res1="ウ";resflg[0]=1;}
		else if (Input.GetKey(KeyCode.Alpha4)) {res1="エ";resflg[0]=1;}
		if(playerCount>=2){
			if (Input.GetKey(KeyCode.Q)) {res2="ア";resflg[1]=1;}
			else if (Input.GetKey(KeyCode.W)){ res2="イ";resflg[1]=1;}
			else if (Input.GetKey(KeyCode.E)) {res2="ウ";resflg[1]=1;}
			else if (Input.GetKey(KeyCode.R)) {res2="エ";resflg[1]=1;}
			if(playerCount>=3){
				if (Input.GetKey(KeyCode.A)) {res3="ア";resflg[2]=1;}
				else if (Input.GetKey(KeyCode.S)) {res3="イ";resflg[2]=1;}
				else if (Input.GetKey(KeyCode.D)) {res3="ウ";resflg[2]=1;}
				else if (Input.GetKey(KeyCode.F)) {res3="エ";resflg[2]=1;}
				if(playerCount>=4){
					if (Input.GetKey(KeyCode.Z)) {res4="ア";resflg[3]=1;}
					else if (Input.GetKey(KeyCode.X)) {res4="イ";resflg[3]=1;}
					else if (Input.GetKey(KeyCode.C)){ res4="ウ";resflg[3]=1;}
					else if (Input.GetKey(KeyCode.V)) {res4="エ";resflg[3]=1;}
				}
			}
		}
		for(j=0;j<playerCount;j++){
			if(resflg[j]!=1) break; 
		}
		if(j==playerCount){
			ansflg=1;
			//正解判定
			if(res1=="") Correct[0]=-1;
			else if(answer==res1) Correct[0]=1;
			else Correct[0] =0;

			if(res2=="") Correct[1]=-1;
			else if(answer==res2) Correct[1]=1;
			else Correct[1] =0;

			if(res3=="") Correct[2]=-1;
			else if(answer==res3) Correct[2]=1;
			else Correct[2] =0;

			if(res4=="") Correct[3]=-1;
			else if(answer==res4) Correct[3]=1;
			else Correct[3] =0;
		}
	}

	public static void 	Pointing(){ //点数付け処理
		int[] playersScore ={0,0,0,0};
		playersScore = PlayerPrefsX.GetIntArray("playersScore");
		int currentPlayer=PlayerPrefs.GetInt("currentPlayer");
		for(int i=0;i<playerCount;i++){
			if(Correct[i]==1) playersScore[i]++;
			if(Correct[i]== currentPlayer) playersScore[i]+=3;
		}
		PlayerPrefsX.SetIntArray("playersScore", playersScore);	
		//if(PlayerPrefs.HasKey()) ;
		//if(Correct==1 )
		/*for(int i=0;i<playerCount;i++){
			if(Correct[i]==1) PlayerPrefsX.SetIntArray("playersScore", Correct);	
		}*/
	}
}