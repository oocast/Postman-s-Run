using UnityEngine;
using System.Collections;

public class WinLoseControl : MonoBehaviour {
	public DogController dogScript;
	public ItemIconsController iconScript;

	int biteCount;
	const int biteMax = 100; //TODO: for Aliceroom Testing

	// Use this for initialization
	void Start () {
		biteCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (biteCount >= biteMax) {
			Application.LoadLevel(0);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Dog") {
			//Application.LoadLevel (0);
			dogScript.BitePostman();
			iconScript.DogBitePostman();
			biteCount++;
		} 
		else if (other.name == "WinCollider") {
			Application.LoadLevel (0);
		}
	}	
}
