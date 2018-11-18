using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class PinTechnique : MonoBehaviour {

    [Header("General")]
    public UnityEngine.UI.Image backgroundObject;

    [Header("Technique")]

    public Sprite background;

    public List<int> numsSoFar = new List<int>();

    public List<int> rightPin = new List<int> { 1, 2, 3, 5 };

    protected virtual void Start() {
        backgroundObject.sprite = background;
    }

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
