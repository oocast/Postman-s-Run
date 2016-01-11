using UnityEngine;
using System.Collections;

public class HandMarkController : MonoBehaviour {
	Vector3 leftHandMarkPosition;
	Vector3 rightHandMarkPosition;
	public PointmanHandMapController positionDataSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateLeftHandMark ();
		UpdateRightHandMark ();
	}

	void UpdateLeftHandMark(){
		leftHandMarkPosition = transform.FindChild ("LeftHandMark").transform.localPosition;

		Vector2 newLeftHandMarkPositionXY = positionDataSource.GetHandMapMarkPosition (true);
		leftHandMarkPosition.x = newLeftHandMarkPositionXY.x;
		leftHandMarkPosition.y = newLeftHandMarkPositionXY.y;
		transform.FindChild ("LeftHandMark").transform.localPosition = leftHandMarkPosition;
	}

	void UpdateRightHandMark(){
		rightHandMarkPosition = transform.FindChild ("RightHandMark").transform.localPosition;
		
		Vector2 newRightHandMarkPositionXY = positionDataSource.GetHandMapMarkPosition (false);
		rightHandMarkPosition.x = newRightHandMarkPositionXY.x;
		rightHandMarkPosition.y = newRightHandMarkPositionXY.y;
		transform.FindChild ("RightHandMark").transform.localPosition = rightHandMarkPosition;
	}
}
