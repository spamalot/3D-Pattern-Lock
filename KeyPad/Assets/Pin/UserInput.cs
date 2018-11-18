using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class UserInput : TechniqueClientController {

    /*
     
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public Text txtInput;
  
    List<Button> buttons = new List<Button>();

    Gyroscope m_Gyro;
    private string passwordEntered;
    
    protected override void Start () {
        base.Start();

        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;

        float startX = -24;
        float startY = -316;
        float spacingX = 132;
        float spacingY = 92;
        numsSoFar.Clear();

        buttons.Add(CreateButton(new Vector2(startX + spacingX * 2, startY - spacingY * 4)));
        for (int i = 0; i < 3; i++) {
            startY = startY - spacingY;
            startX = -24;
            for (int j = 0; j < 3; j++){
                startX = startX + spacingX;
                buttons.Add(CreateButton(new Vector2(startX, startY)));
            }
        }
      
        for (int i = 0; i < 10; i++) {
            var x = i; // curry i (C# doesn't support this nicely any other way)
            buttons[i].onClick.AddListener(() => OnClickBtn(x));
        }


       

    }

    void Update () {
    
        //if (Input.touchCount > 0) {
        //    //Touch myTouch = Input.touches[0];
        //    Touch myTouch = Input.GetTouch(0);
        //    if (myTouch.phase == TouchPhase.Began)
        //    {
        //        Debug.Log("PIN ENTRY " + System.DateTime.Now + " : Begun " + myTouch.position + "\n");
        //    }else if(myTouch.phase == TouchPhase.Ended)
        //    {
        //        Debug.Log("PIN ENTRY " + System.DateTime.Now + " :  End " + myTouch.position + "\n");
        //    }
        //    else
        //    {
        //        Debug.Log("PIN ENTRY " + System.DateTime.Now + " : " + myTouch.position);
        //    }
        //}
  

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
        button.transform.SetParent(buttonContainer);
        button.GetComponent<RectTransform>().anchoredPosition = pos;
        return button.GetComponent<Button>();
    }

    */
}
