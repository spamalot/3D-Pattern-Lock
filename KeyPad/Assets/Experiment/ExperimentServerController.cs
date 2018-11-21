using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ExperimentServerController : ExperimentController {

    public TechniqueServerController PINPinTechnique;
    public TechniqueServerController PatternPinTechnique;
    public TechniqueServerController Pattern3DPinTechnique;

    public TechniqueServerPointer techniqueServerPointer;

    public event Action<Technique> OnTechniqueChanged;

    public int participantId;
    

    // chooses techniques
    // chooses "random" pin

    public List<string> PINPins;
    public List<string> PatternPins;
    public List<string> Pattern3DPins;

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


    public void ApplyServerTechnique(Technique newTechnique) {
        technique = newTechnique;

        foreach (var x in new TechniqueServerController[] { PINPinTechnique, PatternPinTechnique, Pattern3DPinTechnique }) {
            x.gameObject.SetActive(false);
        }
        TechniqueServerController controller;
        switch (technique) {
            case Technique.PIN: controller = PINPinTechnique; break;
            case Technique.Pattern: controller = PatternPinTechnique; break;
            case Technique.Pattern3D: controller = Pattern3DPinTechnique; break;
            default: throw new System.InvalidOperationException();
        }
        controller.gameObject.SetActive(true);
        techniqueServerPointer.Controller = controller;
    }

}
