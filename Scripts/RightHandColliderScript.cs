using UnityEngine;
using System.Collections;

public class RightHandColliderScript : MonoBehaviour {
	public DogController dogScript;
	public ItemIconsController iconScript;
	// Subject to change in the run time
	Transform activeIconTransform = null;
	
	// activeIconIndex: 0 = CupCake, 1 = Meat, 2 = Bone, 3 = Ball, 4 = Bunny, 5 = Frisbee
	int activeItemIndex = -1;
	bool holdingItem = false;
	
	const float iconGain = 1.5f;
	
	float throwStartZPosition;
	const float throwZPositionThreshold = 0.1f;
	// Use this for initialization
	void Start () {
		holdingItem = false;
		activeItemIndex = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.name == "RightBottomCollider") {
			activeIconTransform.localScale *= iconGain;
			if (!holdingItem) {
				throwStartZPosition = transform.localPosition.z;
				holdingItem = true;
			}
		} 
		else if (other.name == "RightTopCollider") {
			if (holdingItem && (transform.localPosition.z - throwStartZPosition > throwZPositionThreshold)) {
				// valid throwing
				holdingItem = false;
				iconScript.RecordThrownItem(activeItemIndex);
			}
		}
		
	}
	
	void OnTriggerExit(Collider other) {
		if (other.name == "RightBottomCollider") {
			activeIconTransform.localScale /= iconGain;
		}
	}
	
	public void SetActiveItemIndex(int index){
		activeItemIndex = index;
		activeIconTransform = iconScript.transform.GetChild (index);
	}
	
	public void ResetThisHand(){
		holdingItem = false;
		activeItemIndex = -1;
		activeIconTransform = null;
	}
}