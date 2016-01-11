using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public DogController dogScript;
	public ItemIconsController iconScript;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			dogScript.DistractDog (10);
		} 
		else {
			//dogScript.stop = false;
			int thrownItemIndex = iconScript.PollThrownItemIndex();
			if (thrownItemIndex > 10) {
				// nothing happens
			}
			else if (thrownItemIndex == -1){
				// Called from itemIconController
				//dogScript.MissEager();
			}
			else {
				dogScript.DistractDog(thrownItemIndex);
			}
		}
		//Debug.Log (GetMouseTargetObject ());
	}

	GameObject GetMouseTargetObject() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit raycastHit;

		if (Physics.Raycast (ray, out raycastHit)) {
			return raycastHit.collider.gameObject;
		}
		return null;
	}
}
