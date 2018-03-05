using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	
	public AudioClip crack;
	public Sprite[] hitSprites;
	public static int breakableCount = 0;
	private int timesHit;
	private LevelManager levelmanager;
	private bool isBreakable;
	public GameObject smoke;
	
	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		if(isBreakable){
			breakableCount ++;
		}
		levelmanager = GameObject.FindObjectOfType<LevelManager>();
		timesHit = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D (Collision2D collision){
	AudioSource.PlayClipAtPoint(crack, transform.position);
	if(isBreakable){
		HandleHits();
		}
	}
	
	void HandleHits() {
		timesHit++;
		int maxHits = hitSprites.Length + 1;
		if (timesHit >= maxHits){
			breakableCount --;
			levelmanager.BrickDestroyed();
			Destroy(gameObject);
			PuffSmoke();
		} else{
			LoadSprites();
		}
	}
	
	void PuffSmoke(){
		GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
		smokePuff.GetComponent<ParticleSystem>().startColor = this.GetComponent<SpriteRenderer>().color;
	}
	
	void LoadSprites(){
		int spriteIndex = timesHit -1;
		if(hitSprites[spriteIndex]){
		this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}else{
		Debug.LogError("Missing Sprite");
		}
	}
	
	//TODO Remove this method once we can actually win!
	void SimulateWin() {
		levelmanager.LoadNextLevel();
	}
}
