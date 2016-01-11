using UnityEngine;
using System.Collections;

public class PostmanController : MonoBehaviour {
	float speed = 1f;
	public GameObject dog;

	// Use this for initialization
	void Start () {
		dog = GameObject.FindWithTag ("Dog");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (speed * Time.deltaTime, 0f, 0f));
		//Camera.main.orthographicSize = 3f;
		float dogDistance = transform.position.x - dog.transform.position.x;
		//Debug.Log (dogDistance);
		Camera.main.GetComponent<CameraController>().ZoomCamera (dogDistance);
	}
	
}
