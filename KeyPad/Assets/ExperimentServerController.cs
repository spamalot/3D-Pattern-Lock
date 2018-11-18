using System;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentServerController : ExperimentController {

    public event Action<Technique> OnTechniqueChanged;

    public int participantId;
    

    // chooses techniques
    // chooses "random" pin

    public List<string> PINPins;
    public List<string> PatternPins;
    public List<string> Pattern3DPins;

    public GameObject techniqueServerControllerObject;


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
    }

    public void ApplyServerTechnique(Technique newTechnique) {
        technique = newTechnique;
        switch (newTechnique) {
            case Technique.PIN: techniqueServerControllerObject.AddComponent<PinServerController>(); break;
            case Technique.Pattern: techniqueServerControllerObject.AddComponent<PatternServerController>(); break;
            case Technique.Pattern3D: Debug.LogError("NotImplemented"); break;
        }
    }

}
