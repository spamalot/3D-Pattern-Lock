using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public int numToSquare = 42;

	// Use this for initialization
	void Start () {
        GetComponentInChildren<UnityEngine.UI.Text>().text = (numToSquare*numToSquare)+"";
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<RectTransform>().position = new Vector3(transform.position.x + 1.01f, transform.position.y);
	}
}
