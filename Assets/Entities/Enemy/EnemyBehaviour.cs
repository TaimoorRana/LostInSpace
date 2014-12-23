using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float health = 300f;
	public GameObject laserPrefab; 
	private float time;
	public float fireRate;
	public static int score = 0;
	public int scoreValue = 100;
	public UI ui;
	public AudioClip deadAudio;
	public AudioClip damageAudio;
	private PlayerController player;
	// Use this for initialization
	void Start () {
		ui = (UI)GameObject.Find("Score").GetComponent<UI>();
		player = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		time += Time.deltaTime * Random.value;
		if(time >= fireRate)
		{	
			if(player.isAlive){
				Shoot ();
			}
			time = 0;
		}
	}
	
	void Shoot(){
		
		GameObject laser = (GameObject)Instantiate(laserPrefab, transform.position, Quaternion.identity);
		laser.rigidbody2D.velocity = new Vector3(0, -10,0);
		laser.tag = "enemyLaser";
		
	}

	void OnTriggerEnter2D (Collider2D col) {
		// try to extract the Laser component of the object that collided
		Laser playerLaser = col.gameObject.GetComponent<Laser>();
		// if the the object is a laser AND it is tagged as playerLaser
		if(playerLaser!= null && playerLaser.tag == "playerLaser"){
			playerLaser.Hit();
			// take damage from the laser
			health -= playerLaser.getDamage();
			AudioSource.PlayClipAtPoint(damageAudio,transform.position);
			if(health <= 0){
				ui.addScore(scoreValue);
				AudioSource.PlayClipAtPoint(deadAudio,transform.position);
				Destroy(gameObject);
			}
		}
	}
	
}
