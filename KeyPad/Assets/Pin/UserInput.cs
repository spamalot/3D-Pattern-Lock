using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class UserInput : PinTechnique {

    public GameObject buttonPrefab;
    public GameObject backgroundParent;
    public Text txtInput;
  
    List<Button> buttons = new List<Button>();

    Gyroscope m_Gyro;
    private string passwordEntered;
    
    // Use this for initialization
    void Start () {
        float startX = -817;
        float startY = 95;
        float spacingX = 132;
        float spacingY = 97;
    m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
        numsSoFar.Clear();
        buttons.Add(CreateButton(new Vector2(startX+spacingX*2, startY-spacingY*4)));
        for (int i = 0; i < 3; i++){
            startY = startY - spacingY;
            startX = -817;
            for (int j = 0; j < 3; j++){
                startX = startX + spacingX;
                buttons.Add(CreateButton(new Vector2(startX, startY)));
            }
        }
      
        for (int i = 0; i < 10; i++){
            int x = i;
            buttons[i].onClick.AddListener(() => OnClickBtn(x));
        }


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
   
     void OnClickBtn(int buttPressed)
    {
        Debug.Log("num: " + buttPressed);
        txtInput.text = txtInput.text + "*";
        numsSoFar.Add(buttPressed);
        passwordEntered = passwordEntered + buttPressed.ToString();
        if(passwordEntered.Length == 4)
        {
            End();
        }
    }

    public Button CreateButton(Vector2 pos)
    {
        GameObject button = Instantiate(buttonPrefab);
        button.transform.SetParent(backgroundParent.transform, false);
        button.GetComponent<RectTransform>().anchoredPosition = pos;
        return button.GetComponent<Button>();
    }


}
