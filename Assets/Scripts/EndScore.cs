using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScore : MonoBehaviour {
	private Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.text = "Score: " + UI.score;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
