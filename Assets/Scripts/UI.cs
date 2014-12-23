using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
	public static int score;
	private Text text;
	
	// Use this for initialization
	void Start () {
		score = 0;
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	static void Reset(){
		score = 0;
	}
	
	public void addScore(int points){
		score += points;
		text.text = score.ToString();
	}
}
