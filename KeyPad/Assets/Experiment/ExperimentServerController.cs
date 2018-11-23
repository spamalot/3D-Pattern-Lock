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

    public int participantId;


    // chooses techniques
    // chooses "random" pin

    public List<string> PINPins;
    public List<string> PatternPins;
    public List<string> Pattern3DPins;

    //Pin Logic
    private int currentPinTested = 0;//0 for the first pin: 1 for the second pin
    private int currentTrailForPin = 0;//how many times the user has tried to enter the pin.
    private int digitEnteredSoFar = 0;
    private string currentlyEnteredStatePin = "";
    private int allowedNumTrails = 8;

    public GameObject techniqueServerControllerObject;
    public InputField inputPID;


    public void SetTechniquePIN() {
        SetTechnique(Technique.PIN);
    }

    public void SetTechniquePattern() {
        SetTechnique(Technique.Pattern);
    }

    public void SetTechniquePattern3D() {
        SetTechnique(Technique.Pattern3D);
    }

    private void SetTechnique(Technique newTechnique) {
        OnTechniqueChanged?.Invoke(newTechnique);
        LoggingClass.setLoggerUser(inputPID.text);
        LoggingClass.setLoggerTechnique(newTechnique.ToString());
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
            default: throw new System.InvalidOperationException();
        }
        SetCurrentPin(currentPinTested);
        SetCurrentPinTrail(currentTrailForPin);
        controller.gameObject.SetActive(true);
        techniqueServerPointer.Controller = controller;
    }

    public void ClientReady() {
       OnClientModeChanged?.Invoke(TechniqueClientController.ModeType.Start);
    }

    public void digitEntered(string enteredDigit)
    {
        digitEnteredSoFar++;
        currentlyEnteredStatePin = currentlyEnteredStatePin + enteredDigit;
        //Debug.Log("Digits entered so far: " + digitEnteredSoFar.ToString());
        if (digitEnteredSoFar == 4)
        {
            if(currentlyEnteredStatePin.Equals(PINPins[currentPinTested]))
            {
                OnClientModeChanged?.Invoke(TechniqueClientController.ModeType.ContinueCorrect);
                LoggingClass.appendToLog("Pin entry finished", "success");
            }
            else
            {
                OnClientModeChanged?.Invoke(TechniqueClientController.ModeType.ContinueIncorrect);
                LoggingClass.appendToLog("Pin entry finished", "fail");
            }

            currentlyEnteredStatePin = "";
            digitEnteredSoFar = 0;

            currentTrailForPin++;
            SetCurrentPinTrail(currentTrailForPin);

            //Check if we want this to be the last trail for the technique
            if(currentPinTested == 1 && currentTrailForPin == 5)
            {
                //CHANGE TECHQNIUE
                return;
            }

            if (currentTrailForPin == allowedNumTrails)//changing the current pin tested.
            {
                currentPinTested++;
                SetCurrentPin(currentPinTested);
                currentTrailForPin = 0;
            }



        }
    }

    public void ClientStart() {
        // fill in: sets client mode to entering
        OnClientModeChanged?.Invoke(TechniqueClientController.ModeType.Entering);
    }

    public void ClientContinue() {
        // fill in: sets client mode to entering
        OnClientModeChanged?.Invoke(TechniqueClientController.ModeType.Entering);
    }

    private void SetCurrentPin(int passedCurrentPinTested)
    {
        currentPinTested = passedCurrentPinTested;
        LoggingClass.setLoggerPinTestedNumber(currentTrailForPin.ToString());
        LoggingClass.setLoggerActualPin(PINPins[currentPinTested]);
        //Debug.Log("setLoggerPinTestedNumber: " + currentTrailForPin.ToString());
        //Debug.Log("setLoggerActualPin: " + PINPins[currentPinTested]);
    }

    private void SetCurrentPinTrail(int passedcurrentTrailForPin)
    {
        currentTrailForPin = passedcurrentTrailForPin;
        //Debug.Log("setLoggerPinTrailNumber: " + passedcurrentTrailForPin.ToString());
        LoggingClass.setLoggerPinTrailNumber(passedcurrentTrailForPin.ToString());
    }
}
