using UnityEngine;
using System.Collections;

public class PointmanHandMapController : MonoBehaviour {

	public GameObject leftHand;
	public GameObject rightHand;

	Vector2 kinectWindowLeftBottom = new Vector2(-0.7f, 0.5f);
	Vector2 kinectWindowRightTop = new Vector2(0.7f, 1.8f);
	Vector2 worldWindowLeftBottom = new Vector2(-1.43f,-0.66f);
	Vector2 worldWindowRightTop = new Vector2(1.43f, 0.66f);

	float cameraSize;

	// Use this for initialization
	void Start () {
		cameraSize = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		cameraSize = Camera.main.orthographicSize;
	}


	public Vector2 GetHandMapMarkPosition(bool isLeftHand){
		// TODO: left OR right hand
		Vector2 result = new Vector2 ();
		if (isLeftHand) {
			result = leftHand.transform.localPosition;
		} 
		else {
			result = rightHand.transform.localPosition;
		}
		Vector2 kinectWindowRange = kinectWindowRightTop - kinectWindowLeftBottom;

		Vector2 worldWindowRange = cameraSize * (worldWindowRightTop - worldWindowLeftBottom);
		result.x = cameraSize * worldWindowLeftBottom.x + (result.x - kinectWindowLeftBottom.x) * (worldWindowRange.x / kinectWindowRange.x);
		result.y = cameraSize * worldWindowLeftBottom.y + (result.y - kinectWindowLeftBottom.y) * (worldWindowRange.y / kinectWindowRange.y);

		return result;
	}
}
