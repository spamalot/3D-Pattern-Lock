using System;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    public event Action<string> OnButtonPress;
    
    public void ButtonPressed(string text) {
        OnButtonPress?.Invoke(text);
    }

}
