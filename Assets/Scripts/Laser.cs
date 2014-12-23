using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	
	public float damage = 100f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float getDamage(){
		return damage;
	}
	
	public void Hit(){
		Destroy(gameObject);
	}
	

}
