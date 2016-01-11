using UnityEngine;
using System.Collections;

public class DogController : MonoBehaviour {
	float speed = 1.3f;
	const float defaultSpeed = 1.3f;
	public bool speedChange = false;
	public bool distracted = false;
	public Animator anim;

	float speedChangeTime = 1f;
	float speedChangeStartTime;
	float animSpeed = 3f;
	public const float biteStopTime = 2f;

	int eatState = Animator.StringToHash("Eat");
	int playState = Animator.StringToHash("Play");
	int runState = Animator.StringToHash("Run");
	AnimatorStateInfo animState;

	// 0 = food, 1 = toy
	int eager = -1;
	// # of items in each category
	const int categorySize = 3;

	// Use this for initialization
	void Start () {
		anim.speed = animSpeed;
		ResetThoughts ();
	}
	
	// Update is called once per frame
	void Update () {
		animState = anim.GetCurrentAnimatorStateInfo (0);
		anim.speed = animSpeed;
		if (!speedChange) {
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0f, 0f));
		} 
		else {
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0f, 0f));
			SpeedChangeCountDown();
		}
	}

	public void DistractDog(int source){
		// keyboard backdoor
		if (source == 10) {
			speedChange = true;
			speedChangeStartTime = Time.time;
			speed = 0f;
			animSpeed = 3f;

			if (animState.shortNameHash == runState) {
				anim.SetTrigger ("EatStart");
				anim.ResetTrigger ("RunStart");
			}
		}
		// Has eager and item thrown
		else if (eager != -1 && source != -1) {
			speedChange = true;
			speedChangeStartTime = Time.time;

			// eager fulfilled
			if (source / categorySize == eager) {
				speed = 0f;
				animSpeed = 3f;

				if (animState.shortNameHash == runState) {
					// Eager is food
					if (eager == 0) {
						anim.SetTrigger ("EatStart");
					}
					// eager is toy
					else if (eager == 1) {
						anim.SetTrigger("PlayStart");
					}
					anim.ResetTrigger ("RunStart");
				}
				ResetEager();
				transform.FindChild("Like").gameObject.SetActive(true);
			}
			// eager mismatch
			else {
				speed = 1.5f;
				animSpeed = 4.5f;
				ResetEager();
				transform.FindChild("Angry Wrong").gameObject.SetActive(true);
			}
		}
	}

	public void BitePostman() {
		speedChange = true;
		speedChangeStartTime = Time.time;
		speedChangeTime = biteStopTime;
		animSpeed = 3f;
		speed = 0f;

		transform.FindChild("Angry").gameObject.SetActive(false);
		transform.FindChild("Angry Wrong").gameObject.SetActive(false);

		anim.SetTrigger ("PlayStart");
		anim.ResetTrigger ("RunStart");
		//ResetEager ();
	}

	void ResetThoughts() {
		// Reset dog thoughts
		transform.FindChild("Angry").gameObject.SetActive(false);
		transform.FindChild ("Angry Wrong").gameObject.SetActive (false);
		transform.FindChild("Like").gameObject.SetActive(false);
		transform.FindChild("Eager Food").gameObject.SetActive(false);
		transform.FindChild("Eager Toy").gameObject.SetActive(false);

		//TODO: for debug only
		transform.FindChild("Wrong Mark").gameObject.SetActive(false);
	}

	void SpeedChangeCountDown(){
		if (Time.time - speedChangeStartTime > speedChangeTime) {
			speedChange = false;
			speed = defaultSpeed;
			GetComponent<Renderer> ().material.color = new Color (1f,1f,1f);

			if (animState.shortNameHash == eatState) {
				anim.SetTrigger("RunStart");
				anim.ResetTrigger("EatStart");
			}
			else if (animState.shortNameHash == playState) {
				anim.SetTrigger("RunStart");
				anim.ResetTrigger("PlayStart");
			}

			speedChangeTime = 1f;
			animSpeed = 3f;

			transform.FindChild("Angry").gameObject.SetActive(false);
			transform.FindChild("Like").gameObject.SetActive(false);
			transform.FindChild("Angry Wrong").gameObject.SetActive(false);
		}
	}

	public void GenerateEager(){
		eager = Random.Range (0, 2);
		if (eager == 0) {
			transform.FindChild ("Eager Food").gameObject.SetActive (true);
		} 
		else {
			transform.FindChild ("Eager Toy").gameObject.SetActive(true);
		}
	}

	public void ResetEager() {
		eager = -1;
		transform.FindChild("Eager Food").gameObject.SetActive(false);
		transform.FindChild("Eager Toy").gameObject.SetActive(false);
	}

	public void MissEager() {
		// Have eager
		if (eager != -1) {
			ResetEager();
			speedChange = true;
			speedChangeStartTime = Time.time;
			speed = 2f;
			animSpeed = 6f;

			if (animState.shortNameHash == eatState) {
				anim.SetTrigger("RunStart");
				anim.ResetTrigger("EatStart");
			}
			else if (animState.shortNameHash == playState) {
				anim.SetTrigger("RunStart");
				anim.ResetTrigger("PlayStart");
			}

			transform.FindChild("Angry").gameObject.SetActive(true);
		}
	}

}
