using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public bool isAlive;
	
	private float health = 500;
	// speed of the player
	public float speed;
	public GameObject laserPrefab;
	// screen bounderies
	private float xMin, xMax ;
	
	//x position of the player
	private float xPosition;
	
	// laser speed
	private float laserSpeed = 5.0f ;
	
	public AudioClip damageAudio;
	public AudioClip deadAudio;
	private LevelManager levelManager;
	
	// Use keyboard input to move the ship left and right
	void keyboardInput (){
		if(Input.GetKey(KeyCode.LeftArrow)){
			/*
			Substract SPEED from the player current Position and limit this new position within the
			screen bounderies
			*/
			xPosition = Mathf.Clamp(transform.position.x-(speed*Time.deltaTime),xMin,xMax);
			
			// Keep the current y and z position value and update the x value only
			transform.position = new Vector3(xPosition,transform.position.y,transform.position.z);
		}else if(Input.GetKey(KeyCode.RightArrow)){
			/*
			Add SPEED from the player current Position and limit this new position within the
			screen bounderies
			*/
			xPosition = Mathf.Clamp(transform.position.x+(speed*Time.deltaTime),xMin,xMax);
			
			// Keep the current y and z position value and update the x value only
			transform.position = new Vector3(xPosition,transform.position.y,transform.position.z);
			
		}
	}
	
	void fire(){
		// create a laser object
		GameObject laser = (GameObject) Instantiate(laserPrefab,transform.position,Quaternion.identity);
		// set laser velocity and direction
		laser.rigidbody2D.velocity = new Vector3(0,laserSpeed,0);
		// tag will be used by the enemy class to check if the laser that hit them was a player's laser
		laser.tag = "playerLaser";
		
	}
	
	// Method runs when the player collides with the enemy's laser
	void OnTriggerEnter2D(Collider2D col){
		// try to extract the Laser component of the object that collided
		Laser laser = col.gameObject.GetComponent<Laser>();
		
		// if the the object is a laser AND it is tagged as enemyLaser
		if(laser != null && laser.tag == "enemyLaser"){
			// take damage from the laser
			health -= laser.getDamage();
			// play damage audio
			AudioSource.PlayClipAtPoint(damageAudio,transform.position);
			
			if(health <= 0){
				isAlive = false;		
				// play explosion audio	
				AudioSource.PlayClipAtPoint(deadAudio,transform.position);
				Destroy(gameObject);
				EndScreen();
			}
		}
		
		
	}
	
	void EndScreen(){
		levelManager.LoadLevel("End Screen");
	}
	
	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		
		isAlive = true;
		// camera is used to get the exact screen bounderies of the game.
		Camera camera = Camera.main;
		
		// Find out how far is the camera from the player
		float distance = camera.transform.position.z - transform.position.z;
		
		xMax = camera.ViewportToWorldPoint(new Vector3(1,0,distance)).x;
		xMin = camera.ViewportToWorldPoint(new Vector3(0,1,distance)).x;
	}
	
	// Update is called once per frame
	void Update () {
		// get 
		keyboardInput();
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("fire",0.0001f,0.2f);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("fire");
		}
	}
}
