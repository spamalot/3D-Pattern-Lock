﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TechniqueClientController : MonoBehaviour {

    [Header("General")]
    public Image backgroundObject;

    [Header("Technique")]

    public Sprite background;

    public List<int> EnteredNumbers { get; set; } = new List<int>();
    public Vector2 CursorPosition { get; set; }
    public TechniqueServerController.Depth CursorDepth { get; set; }

    public Button startButton;
    public Button continueButton;
    public Text correctPinText;
    public Text noticationText;

    public enum ModeType { Start, Entering, ContinueNoFeedback, ContinueCorrect, ContinueIncorrect };
    public ModeType Mode { get; set; }

    public enum NotificationType { Round1, Round2, Finished };
    public NotificationType notification { get; set; }

    public ButtonController buttonController;

    protected virtual void OnEnable() {
        backgroundObject.sprite = background;
    }

    protected virtual void Start()
    {
        startButton.onClick.AddListener(() => buttonController.ButtonPressed(ExperimentServerController.START));
        continueButton.onClick.AddListener(() => buttonController.ButtonPressed(ExperimentServerController.CONTINUE));
    }

    public void ChangeMode(ModeType modeType) {
        // hide everything
        startButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        correctPinText.gameObject.SetActive(false);

        if (modeType == ModeType.Start) {
            // show start
            startButton.gameObject.SetActive(true);
        } else if (modeType == ModeType.Entering) {
            // do nothing?
        } else if (modeType == ModeType.ContinueNoFeedback || modeType == ModeType.ContinueCorrect
                   || modeType == ModeType.ContinueIncorrect) {
            // show continue button
            continueButton.gameObject.SetActive(true);
            correctPinText.gameObject.SetActive(true);
            switch (modeType) {
                case ModeType.ContinueNoFeedback: correctPinText.text = ""; break;
                case ModeType.ContinueCorrect: correctPinText.text = "Correct PIN"; break;
                case ModeType.ContinueIncorrect: correctPinText.text = "Incorrect PIN"; break;
            }
        } else {
            Debug.Assert(false);
        }
    }

    public void UpdateNotificationText(NotificationType notification){
        noticationText.text = notification.ToString();
    }

    public abstract void ChangeFeedbackEnabled(bool enabled_);

}
