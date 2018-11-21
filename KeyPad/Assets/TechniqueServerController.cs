using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TechniqueServerController : MonoBehaviour {

    public enum Depth { Inactive, Close, Middle, Far };

    public virtual void OnBeginDrag() { }
    public virtual void OnEndDrag() { }
    public virtual void OnDrag(Vector2 pos) { }
    public virtual void OnButtonPress(string text) { }

    public List<int> enteredNumbers = new List<int>();

    public event Action<int[]> OnEnteredNumbersChanged;
    public event Action<Vector2> OnCursorPositionChanged;
    public event Action<Depth> OnCursorDepthChanged;

    protected void InvokeOnEnteredNumbersChanged(int[] numbers) {
        OnEnteredNumbersChanged?.Invoke(numbers);
    }

    protected void InvokeOnCursorPositionChanged(Vector2 position) {
        OnCursorPositionChanged?.Invoke(position);
    }

    protected void InvokeOnCursorDepthChanged(Depth depth) {
        OnCursorDepthChanged?.Invoke(depth);
    }

}
