using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour {

	public float leftOffset = 114;
	public float topOffset = 458;
	public float spacing = 127;

	public Camera c;

	void Start () {
		
	}
	
	void Update () {
		Debug.Log(Input.GetMouseButton(0));
	}
}
