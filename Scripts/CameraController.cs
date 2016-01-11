using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	const float cameraSizeDistanceRatio = 0.5f;
	const float defaultOrthographicSize = 3f;
	Vector3 defaultCameraLocalPosition;
	public GameObject itemIcons;
	//public GameObject SteakIcon;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		float initOrthographicSize = Camera.main.orthographicSize;
		defaultCameraLocalPosition = Camera.main.transform.localPosition;
		//defaultCameraLocalPosition.x *= defaultOrthographicSize / initOrthographicSize;
		//defaultCameraLocalPosition.y *= defaultOrthographicSize / initOrthographicSize;
		Camera.main.orthographicSize = defaultOrthographicSize;

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ZoomCamera(float dogDistance){
		if (dogDistance > defaultOrthographicSize / cameraSizeDistanceRatio) {
			float newOrthographicSize = cameraSizeDistanceRatio * dogDistance;
			Vector3 newItemIconScale = new Vector3(newOrthographicSize, newOrthographicSize, 2f);
			Vector3 cameraLocalPosition = Camera.main.transform.localPosition;
			cameraLocalPosition.x *= newOrthographicSize / Camera.main.orthographicSize;
			cameraLocalPosition.y *= newOrthographicSize / Camera.main.orthographicSize;

			Camera.main.orthographicSize = newOrthographicSize;
			Camera.main.transform.localPosition = cameraLocalPosition;
			itemIcons.transform.localScale = newItemIconScale;
		} 
		else {
			Camera.main.orthographicSize = defaultOrthographicSize;
			Camera.main.transform.localPosition = defaultCameraLocalPosition;
			itemIcons.transform.localScale = defaultOrthographicSize * Vector3.one;
		}
	}
}
