using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemIconsController : MonoBehaviour {

	public GameObject cupcake;
	public GameObject meat;
	public GameObject bone;
	public GameObject ball;
	public GameObject bunny;
	public GameObject frisbee;
	public ItemColliderController itemColliderScript;
	public DogController dogScript;
	public AudioSource sound;

	float generationPeriod = 4f;
	float generationDuration = 2.5f;

	bool generatingItem = false;
	float generationStartTime = -10f;
	bool itemThrown = false;
	bool isMissing = false;
	int thrownItemIndex = -1;
	bool isFoodAtLeft = false;

	int generatedFoodItemIndex = -1;
	int generatedToyItemIndex = -1;

	Vector3[] iconDefaultScales = new Vector3[6];
	Vector3 leftBottomIconDefaultPosition;
	Vector3 rightBottomIconDefaultPosition;

	// Use this for initialization
	void Start () {
		leftBottomIconDefaultPosition = new Vector3(-2.86f, -1.32f, 0);
		rightBottomIconDefaultPosition = new Vector3(2.86f, -1.32f, 0);

		foreach (Transform child in transform) {
			child.gameObject.SetActive(false);
		}

		// Store default scale
		for (int i = 0; i < 6; i++) {
			iconDefaultScales[i] = transform.GetChild(i).localScale;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (generatingItem) {
			if ((Time.time - generationStartTime > generationDuration) && !itemThrown) {
				// generating duration hit without throw
				isMissing = true;
				dogScript.MissEager();
				MissThisRound();
			}
			else if ((Time.time - generationStartTime <= generationDuration) && itemThrown) {
				// throw an item before duration expires
				StopGenerateItem();
				ResetOtherHand();
				itemThrown = false;
				//dogScript.ResetEager();
			}
		} 
		else {
			if (Time.time - generationStartTime > generationPeriod) {
				// generating period hit
				StartGenerateItem();
				dogScript.GenerateEager();
			}
		}
	}

	void StartGenerateItem(){
		generatingItem = true;
		generationStartTime = Time.time;
		// index: 0 = CupCake, 1 = Meat, 2 = Bone, 3 = Ball, 4 = Bunny, 5 = Frisbee
		generatedFoodItemIndex = Random.Range (0, 3);
		generatedToyItemIndex = Random.Range (3, 6);

		// Set icon local scales
		transform.GetChild (generatedFoodItemIndex).localScale = iconDefaultScales [generatedFoodItemIndex];
		transform.GetChild (generatedToyItemIndex).localScale = iconDefaultScales [generatedToyItemIndex];

		// Set icon positions
		int numFoodAtLeft = Random.Range (0, 2);
		if (numFoodAtLeft == 1) {
			isFoodAtLeft = true;
		} 
		else {
			isFoodAtLeft = false;
		}

		if (isFoodAtLeft) {
			transform.GetChild (generatedFoodItemIndex).localPosition = leftBottomIconDefaultPosition;
			transform.GetChild (generatedToyItemIndex).localPosition = rightBottomIconDefaultPosition;
			itemColliderScript.PassActiveItemIndex(generatedFoodItemIndex, generatedToyItemIndex);
		} 
		else {
			transform.GetChild (generatedFoodItemIndex).localPosition = rightBottomIconDefaultPosition;
			transform.GetChild (generatedToyItemIndex).localPosition = leftBottomIconDefaultPosition;
			itemColliderScript.PassActiveItemIndex(generatedToyItemIndex, generatedFoodItemIndex);
		}

		// Activating icons
		transform.GetChild(generatedFoodItemIndex).gameObject.SetActive(true);
		transform.GetChild(generatedToyItemIndex).gameObject.SetActive(true);

		sound.PlayOneShot (sound.clip);
		itemColliderScript.SetAllCollidersActive (true);
	}

	// Deactivate icons and colliders upon miss / throwing
	void StopGenerateItem(){
		generatingItem = false;

		// Set icon local scales
		transform.GetChild (generatedFoodItemIndex).localScale = iconDefaultScales [generatedFoodItemIndex];
		transform.GetChild (generatedToyItemIndex).localScale = iconDefaultScales [generatedToyItemIndex];

		// Activating icons
		transform.GetChild(generatedFoodItemIndex).gameObject.SetActive(false);
		transform.GetChild(generatedToyItemIndex).gameObject.SetActive(false);

		itemColliderScript.SetAllCollidersActive (false);
	}	

	public void DogBitePostman() {
		generationStartTime += 2f;
	}

	void MissThisRound(){
		StopGenerateItem ();
		itemColliderScript.ResetHandsWhenMiss ();
	}

	public void RecordThrownItem(int itemIndex){
		if (!itemThrown) {
			thrownItemIndex = itemIndex;
			itemThrown = true;
		}
	}

	public int PollThrownItemIndex() {
		if (isMissing) {
			isMissing = false;
			return -1;
		} 
		else if (itemThrown) {
			return thrownItemIndex;
		} 
		else {
			return 100;
		}
	}

	void ResetOtherHand(){
		if (isFoodAtLeft) {
			// left hand throw, reset right hand
			if (thrownItemIndex == generatedFoodItemIndex) {
				itemColliderScript.ResetOtherHandWhenThrow (false);
			}
			// right hand throw, reset left hand
			else if (thrownItemIndex == generatedToyItemIndex) {
				itemColliderScript.ResetOtherHandWhenThrow (true);
			}
		} 
		else {
			// left hand throw, reset right hand
			if (thrownItemIndex == generatedToyItemIndex) {
				itemColliderScript.ResetOtherHandWhenThrow (false);
			}
			// right hand throw, reset left hand
			else if (thrownItemIndex == generatedFoodItemIndex) {
				itemColliderScript.ResetOtherHandWhenThrow (true);
			}
		}
	}
}
