using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;
	// Use this for initialization
	void Start () {
		if(instance != null && instance !=this){
			Destroy(gameObject);
			print ("music destroyed");
		}else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.Play();
			music.loop = true;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnLevelWasLoaded(int level){
		music.Stop();
		if(level == 0){
			music.clip = startClip;
		}
		if(level == 1){
			music.clip = gameClip;
		}
		if(level == 2){
			music.clip = endClip;
		}
		music.Play();
		music.loop = true;
	
	}
}
