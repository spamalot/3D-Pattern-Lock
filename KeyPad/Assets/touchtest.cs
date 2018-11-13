using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchtest : MonoBehaviour {

    public float leftOffset = 114;
    public float topOffset = 458;
    public float spacing = 127;

    public Camera c;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var a = c.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(a + " "+Input.GetMouseButton(1));
	}
}
