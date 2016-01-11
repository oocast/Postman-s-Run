using UnityEngine;
using System.Collections;

public class PostmanAnimationController : MonoBehaviour {
	public Animator anim;
	float animSpeed = 2f;

	// Use this for initialization
	void Start () {
		anim.speed = animSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		anim.speed = animSpeed;
	}
}
