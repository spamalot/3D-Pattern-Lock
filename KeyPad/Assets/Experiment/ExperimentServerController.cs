using System;
using System.Collections.Generic;
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
            return relevantList[participantId * PINS_PER_PARTICIPANT + currentPinTested];
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
        OnClientFeedbackEnabledChanged?.Invoke(true);
    }

    private void DigitEntered()
    {
        if (_controller.EnteredNumberCount == 4) {

            // TODO only in the case that feedback is enabled do we show correct or incorrect
            if (string.Join(",", _controller.EnteredNumbers) == CurrentCorrectPinString) {
                ChangeClientMode(TechniqueClientController.ModeType.ContinueCorrect);
                LoggingClass.AppendToLog("Pin entry finished", "success");
            } else {
                ChangeClientMode(TechniqueClientController.ModeType.ContinueIncorrect);
                LoggingClass.AppendToLog("Pin entry finished", "fail");
            }

            // TODO use somewhere: (in the case that feedback is disabled, regardless of correctness)
            // ChangeClientMode(TechniqueClientController.ModeType.ContinueNoFeedback);

            _controller.ResetEnteredNumbers();

            currentTrialForPin++;
            SetCurrentPinTrial(currentTrialForPin);

            //Check if we want this to be the last trial for the technique
            if (currentPinTested == (PINS_PER_PARTICIPANT - 1) && currentTrialForPin == 8) {
                // TODO: make sure this is called when pin changes
                OnClientFeedbackEnabledChanged?.Invoke(true);
                return;
            }

            // TODO when past trial rounds do this:
            // OnClientFeedbackEnabledChanged?.Invoke(false);

            if (currentTrialForPin == TOTAL_TRIALS) { //changing the current pin tested.
                currentPinTested++;
                SetCurrentPin(currentPinTested);
                currentTrialForPin = 0;
            }

        }
    }

    public void ClientStart() {
        // fill in: sets client mode to entering
        ChangeClientMode(TechniqueClientController.ModeType.Entering);
    }

    public void ClientContinue() {
        // fill in: sets client mode to entering
        // TODO if we're done, do nothing
        ChangeClientMode(TechniqueClientController.ModeType.Entering);
    }

    private void SetCurrentPin(int passedCurrentPinTested)
    {
        currentPinTested = passedCurrentPinTested;
        LoggingClass.ExperimentPinNumber = currentTrialForPin.ToString();
        LoggingClass.ActualPin = PINPins[currentPinTested];
        //Debug.Log("setLoggerPinTestedNumber: " + currentTrailForPin.ToString());
        //Debug.Log("setLoggerActualPin: " + PINPins[currentPinTested]);
    }

    private void SetCurrentPinTrial(int passedcurrentTrialForPin)
    {
        currentTrialForPin = passedcurrentTrialForPin;
        //Debug.Log("setLoggerPinTrailNumber: " + passedcurrentTrailForPin.ToString());
        LoggingClass.TrialNumber = passedcurrentTrialForPin.ToString();
    }
}
