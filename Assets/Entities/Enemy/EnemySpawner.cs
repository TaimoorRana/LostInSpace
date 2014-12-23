using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	// assign the enemy via unity
	public GameObject enemyPrefab;
	private float speed = 3.0f;
	// x position of the object
	private float xPosition;
	// bounderies in which this object can move
	private float xMin, xMax;
	
	private int direction = -1;
	
	// Use this for initialization
	void Start () {
		// find the bounderies where enemies can move
		xPosition = 0;
		direction = -1;
		transform.position = new Vector3(0,0,0);
		// for every child , make an enemy gameobject
		makeEnemies();
		setBounderies();
	}
	
	void makeEnemies(){
		foreach(Transform child in transform){
			GameObject enemy = (GameObject)Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
			enemy.transform.parent = child;
		}
	}
	

	
	
	
	// find the bounderies where enemies can move
	void setBounderies(){
	
		float mostRightEnemy = 0;
		float mostLeftEnemy = 0;
		
		// find the furthest right and left enemy
		foreach (Transform child in transform){
			if(mostRightEnemy < child.transform.position.x)
			{
				mostRightEnemy = child.transform.position.x;
			}
			
			if(mostLeftEnemy > child.transform.position.x)
			{
				mostLeftEnemy = child.transform.position.x;
			}
			
		}	
		
		Camera camera = Camera.main;
		float distance = (camera.transform.position - transform.position).z;

		xMin = camera.ViewportToWorldPoint(new Vector3(0,0,distance)).x + Mathf.Abs(mostLeftEnemy);
		xMax = camera.ViewportToWorldPoint(new Vector3(1,0,distance)).x - Mathf.Abs(mostRightEnemy);
	}
	
	// move the enemies with the bounderies
	void moveEnemyFormation(){
		// if enemy formation is going left
		if(direction == -1){
		
			// calcualte the new position with respect to the speed and bound to the xMin and xMax
			xPosition = Mathf.Clamp((transform.position.x+speed*Time.deltaTime),xMin,xMax);
			
			transform.position = new Vector3(xPosition,transform.position.y,transform.position.z);
			
			if(xPosition >= xMax){
				direction = 1;
			}
		}
		if(direction == 1){
			// calcualte the new position with respect to the speed and bound to the xMin and xMax
			xPosition = Mathf.Clamp((transform.position.x-speed*Time.deltaTime),xMin,xMax);
			
			transform.position = new Vector3(xPosition,transform.position.y,transform.position.z);
			
			if(xPosition <= xMin){
				direction = -1;
			}
		}
		
	}
	
	// verify if all enemies are destroyed.
	bool areEnemiesDead (){
		foreach(Transform position in transform){
			if(position.childCount > 0)
			{
				return false;
			}
		}
		return true;
	}
	
	// Update is called once per frame
	void Update () {
		moveEnemyFormation();
		if(areEnemiesDead()){
			Start ();
		}	
	}
}
