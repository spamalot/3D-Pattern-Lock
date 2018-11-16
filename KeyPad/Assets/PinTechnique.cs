using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class PinTechnique : MonoBehaviour {

    public List<int> numsSoFar = new List<int>();

    List<int> rightPin = new List<int> { 1, 2, 3, 5 };

    // some way to know right pin

    // some comparison of pins

    // some way to commit entered pin

    protected void Commit() {
        Debug.Log("TODO");
        if(rightPin.SequenceEqual(numsSoFar)){
            Debug.Log("correct PIN");
        }
        numsSoFar.Clear();
    }

}
