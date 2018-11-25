using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentServerController : ExperimentController {

    public const string START = "Start";
    public const string CONTINUE = "Continue";

    public TechniqueServerController PINPinTechnique;
    public TechniqueServerController PatternPinTechnique;
    public TechniqueServerController Pattern3DPinTechnique;

    public TechniqueServerPointer techniqueServerPointer;

    public event Action<Technique> OnTechniqueChanged;
    public event Action<TechniqueClientController.ModeType> OnClientModeChanged;
    public event Action<bool> OnClientFeedbackEnabledChanged;
    public event Action<TechniqueClientController.NotificationType> OnClientRoundNotification;

    private int participantId;

    // chooses techniques
    // chooses "random" pin

    public List<string> PINPins;
    public List<string> PatternPins;
    public List<string> Pattern3DPins;

    //Pin Logic
    private int currentPinTested = 0; //0 for the first pin: 1 for the second pin
    private int currentTrialForPin = 0; //how many times the user has tried to enter the pin.

    private const int TOTAL_TRIALS = 8;
    private const int PRACTICE_TRIALS = 3;
    private const int PINS_PER_PARTICIPANT = 2;
    // extra var for number of practice rounds

    public InputField inputPID;

    private TechniqueServerController _controller;


    private string CurrentCorrectPinString {
        get {
            List<string> relevantList = null;
            switch (technique) {
                case Technique.PIN: relevantList = PINPins; break;
                case Technique.Pattern: relevantList = PatternPins; break;
                case Technique.Pattern3D: relevantList = Pattern3DPins; break;
                default: throw new InvalidOperationException();
            }
            return relevantList[(participantId % 1000) * PINS_PER_PARTICIPANT + currentPinTested];
        }
    }

    /// <summary>
    /// Called by button press
    /// </summary>
    public void SetTechniquePIN() {
        SetTechnique(Technique.PIN);
    }

    /// <summary>
    /// Called by button press
    /// </summary>
    public void SetTechniquePattern() {
        SetTechnique(Technique.Pattern);
    }

    /// <summary>
    /// Called by button press
    /// </summary>
    public void SetTechniquePattern3D() {
        SetTechnique(Technique.Pattern3D);
    }

    private void SetTechnique(Technique newTechnique) {
        OnTechniqueChanged?.Invoke(newTechnique);
        participantId = int.Parse(inputPID.text);
        LoggingClass.UserID = inputPID.text;
        LoggingClass.Technique = newTechnique.ToString();
    }

    public void ApplyServerTechnique(Technique newTechnique)
    {
        technique = newTechnique;

        foreach (var x in new TechniqueServerController[] { PINPinTechnique, PatternPinTechnique, Pattern3DPinTechnique })
        {
            x.gameObject.SetActive(false);
        }
        TechniqueServerController controller;
        switch (technique)
        {
            case Technique.PIN: controller = PINPinTechnique; break;
            case Technique.Pattern: controller = PatternPinTechnique; break;
            case Technique.Pattern3D: controller = Pattern3DPinTechnique; break;
            default: throw new InvalidOperationException();
        }
        SetCurrentPin(currentPinTested);
        SetCurrentPinTrial(currentTrialForPin);
        controller.gameObject.SetActive(true);
        techniqueServerPointer.Controller = controller;

        // For instrumentation
        _controller = controller;
        _controller.OnEnteredNumbersChanged += x => DigitEntered();
    }


    private void ChangeClientMode(TechniqueClientController.ModeType modeType) {
        _controller.Enabled = modeType == TechniqueClientController.ModeType.Entering;
        OnClientModeChanged?.Invoke(modeType);
    }

    public void ClientReady() {
       ChangeClientMode(TechniqueClientController.ModeType.Start);
        OnClientRoundNotification?.Invoke(TechniqueClientController.NotificationType.Round1Practice);
        OnClientFeedbackEnabledChanged?.Invoke(true);
    }

    private void DigitEntered()
    {
        var enteredPinString = string.Join(",", _controller.EnteredNumbers);
        SetLoggerCurrentEnteredPin(enteredPinString);
        LoggingClass.AppendToLog(LoggingClass.ENTERED_PIN_CHANGE, null, null, null, null, null);
        if (_controller.EnteredNumberCount == 4) {

            // TODO only in the case that feedback is enabled do we show correct or incorrect
            if (enteredPinString == CurrentCorrectPinString) {
                if (currentTrialForPin >= PRACTICE_TRIALS) {
                    ChangeClientMode(TechniqueClientController.ModeType.ContinueNoFeedback);
                } else {
                    ChangeClientMode(TechniqueClientController.ModeType.ContinueCorrect);
                }
                LoggingClass.AppendToLog(LoggingClass.ENTRY_END, null, null, null, null, true);
            } else {
                if (currentTrialForPin >= PRACTICE_TRIALS) {
                    ChangeClientMode(TechniqueClientController.ModeType.ContinueNoFeedback);
                } else {
                    ChangeClientMode(TechniqueClientController.ModeType.ContinueIncorrect);
                }
                LoggingClass.AppendToLog(LoggingClass.ENTRY_END, null, null, null, null, false);
            }

            // TODO use somewhere: (in the case that feedback is disabled, regardless of correctness)
            // ChangeClientMode(TechniqueClientController.ModeType.ContinueNoFeedback);

            _controller.ResetEnteredNumbers();

            currentTrialForPin+=1;
            SetCurrentPinTrial(currentTrialForPin);

            if(currentTrialForPin >= PRACTICE_TRIALS){
                if (currentPinTested == 0) {
                    OnClientRoundNotification?.Invoke(TechniqueClientController.NotificationType.Round1Normal);
                } else {
                    OnClientRoundNotification?.Invoke(TechniqueClientController.NotificationType.Round2Normal);
                }
                OnClientFeedbackEnabledChanged?.Invoke(false);
            }

            // TODO when past trial rounds do this:
            // OnClientFeedbackEnabledChanged?.Invoke(false);

            if (currentTrialForPin == TOTAL_TRIALS) { //changing the current pin tested.
                currentPinTested+=1;
                SetCurrentPin(currentPinTested);
                currentTrialForPin = 0;
                if (currentPinTested == (PINS_PER_PARTICIPANT - 1)){
                    OnClientFeedbackEnabledChanged?.Invoke(true);
                    OnClientRoundNotification?.Invoke(TechniqueClientController.NotificationType.Round2Practice);
                }
                if (currentPinTested == PINS_PER_PARTICIPANT)
                {
                    OnClientRoundNotification?.Invoke(TechniqueClientController.NotificationType.Finished);
                }
            }

            /*
            //Check if we want this to be the last trial for the technique
            if (currentPinTested == (PINS_PER_PARTICIPANT - 1) && currentTrialForPin == 8)
            {
                // TODO: make sure this is called when pin changes
                OnClientFeedbackEnabledChanged?.Invoke(true);
                OnClientRoundNotification?.Invoke(TechniqueClientController.NotificationType.Round2);
                return;
            }
            */
        }
    }

    public void ClientStart() {
        LoggingClass.AppendToLog(LoggingClass.ENTRY_START, null, null, null, null, null);
        ChangeClientMode(TechniqueClientController.ModeType.Entering);
    }

    public void ClientContinue() {
        if (currentPinTested == PINS_PER_PARTICIPANT) {
            return;
        }
        LoggingClass.AppendToLog(LoggingClass.ENTRY_START, null, null, null, null, null);
        ChangeClientMode(TechniqueClientController.ModeType.Entering);
    }

    private void SetCurrentPin(int passedCurrentPinTested)
    {
        currentPinTested = passedCurrentPinTested;
        LoggingClass.ExperimentPinNumber = currentTrialForPin.ToString();
        LoggingClass.ActualPin = CurrentCorrectPinString.Replace(',', '-');
        //Debug.Log("setLoggerPinTestedNumber: " + currentTrailForPin.ToString());
        //Debug.Log("setLoggerActualPin: " + PINPins[currentPinTested]);
    }

    private void SetCurrentPinTrial(int passedcurrentTrialForPin)
    {
        currentTrialForPin = passedcurrentTrialForPin;
        //Debug.Log("setLoggerPinTrailNumber: " + passedcurrentTrailForPin.ToString());
        LoggingClass.TrialNumber = passedcurrentTrialForPin.ToString();
    }

    private void SetLoggerCurrentEnteredPin(string pinString) {
        LoggingClass.EnteredPin = pinString.Replace(',', '-');
    }
}
