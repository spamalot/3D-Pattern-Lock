using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class UserInput : PinTechnique {

    public GameObject[] buttons;
    public Text txtInput;

    Gyroscope m_Gyro;
    private string passwordEntered;
    
    // Use this for initialization
    void Start () {
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
        numsSoFar.Clear();
    }

    void Awake ()
    {


    }

    // Update is called once per frame
    void Update () {
        /*
        if (Input.touchCount > 0) {
            //Touch myTouch = Input.touches[0];
            Touch myTouch = Input.GetTouch(0);
            if (myTouch.phase == TouchPhase.Began)
            {
                Debug.Log("PIN ENTRY " + System.DateTime.Now + " : Begun " + myTouch.position + "\n");
            }else if(myTouch.phase == TouchPhase.Ended)
            {
                Debug.Log("PIN ENTRY " + System.DateTime.Now + " :  End " + myTouch.position + "\n");
            }
            else
            {
                Debug.Log("PIN ENTRY " + System.DateTime.Now + " : " + myTouch.position);
            }
        }
        */

       // Debug.Log("Gyro rotation rate " + m_Gyro.rotationRate);
       // Debug.Log("Gyro attitude" + m_Gyro.attitude);
       // Debug.Log("Gyro enabled : " + m_Gyro.enabled);



    }

    void End() {
        Debug.Log(passwordEntered);
        passwordEntered = "";
        txtInput.text = "";
        Commit();
    }
   
    public void OnClickBtn(int buttPressed)
    {
        txtInput.text = txtInput.text + "*";
        numsSoFar.Add(buttPressed);
        passwordEntered = passwordEntered + buttPressed.ToString();
        if(passwordEntered.Length == 4)
        {
            End();
        }
    }

 
}
