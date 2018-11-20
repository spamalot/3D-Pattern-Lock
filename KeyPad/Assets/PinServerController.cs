using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinServerController : TechniqueServerController
{
    public override void OnButtonPress(string text) {
        enteredNumbers.Add(Int32.Parse(text));
        if (enteredNumbers.Count == 4){
            enteredNumbers.Clear();
        }
        InvokeOnEnteredNumbersChanged(enteredNumbers.ToArray());
    }
}
