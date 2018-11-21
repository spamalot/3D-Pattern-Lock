﻿using System.Collections.Generic;
using UnityEngine;

public abstract class TechniqueClientController : MonoBehaviour {

    [Header("General")]
    public UnityEngine.UI.Image backgroundObject;

    [Header("Technique")]

    public Sprite background;

    public List<int> EnteredNumbers { get; set; } = new List<int>();
    public Vector2 CursorPosition { get; set; }
    public TechniqueServerController.Depth CursorDepth { get; set; }

    protected virtual void OnEnable() {
        backgroundObject.sprite = background;
    }

}
