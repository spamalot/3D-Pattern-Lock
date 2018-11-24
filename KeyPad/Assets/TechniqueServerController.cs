using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TechniqueServerController : MonoBehaviour {

    public enum Depth { Inactive, Close, Middle, Far };

    public virtual void OnBeginDrag() { }
    public virtual void OnEndDrag() { }
    public virtual void OnDrag(Vector2 pos) { }
    public virtual void OnButtonPress(string text) { }

    public bool Enabled { get; set; } = true;
    public List<int> EnteredNumbers { get; protected set; } = new List<int>();
    public int EnteredNumberCount { get { return EnteredNumbers.Count; } }

    public event Action<int[]> OnEnteredNumbersChanged;
    public event Action<Vector2> OnCursorPositionChanged;
    public event Action<Depth> OnCursorDepthChanged;

    public void ResetEnteredNumbers() {
        EnteredNumbers.Clear();
    }

    protected void InvokeOnEnteredNumbersChanged() {
        OnEnteredNumbersChanged?.Invoke(EnteredNumbers.ToArray());
    }

    protected void InvokeOnCursorPositionChanged(Vector2 position) {
        OnCursorPositionChanged?.Invoke(position);
    }

    protected void InvokeOnCursorDepthChanged(Depth depth) {
        OnCursorDepthChanged?.Invoke(depth);
    }

}
