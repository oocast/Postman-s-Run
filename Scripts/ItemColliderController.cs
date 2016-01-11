using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemColliderController : MonoBehaviour {
	
	// TODO: accessing hands here might not be good
	public LeftHandColliderScript leftHandScript;
	public RightHandColliderScript rightHandScript;

	// Use this for initialization
	void Start () {
		SetAllCollidersActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// All 4 colliders are now combined
	public void SetAllCollidersActive(bool state){
		foreach (Transform child in transform) {
			child.gameObject.SetActive(state);
		}
	}

	public void PassActiveItemIndex(int leftItemIndex, int rightItemIndex){
		leftHandScript.SetActiveItemIndex(leftItemIndex);
		rightHandScript.SetActiveItemIndex(rightItemIndex);
	}

	public void ResetHandsWhenMiss() {
		leftHandScript.ResetThisHand();
		rightHandScript.ResetThisHand();
	}

	public void ResetOtherHandWhenThrow(bool isLeftHand) {
		if (isLeftHand) {
			leftHandScript.ResetThisHand ();
		} 
		else {
			rightHandScript.ResetThisHand();
		}
	}
}
