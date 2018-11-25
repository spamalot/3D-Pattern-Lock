using System;
using UnityEngine;

public class ExperimentClientController : ExperimentController {

    public event Action OnClientReady;

    public TechniqueClientController PINPinTechnique;
    public TechniqueClientController PatternPinTechnique;
    public TechniqueClientController Pattern3DPinTechnique;

    public TechniqueClientPointer techniqueClientPointer;

    public void ApplyClientTechnique(Technique newTechnique) {
        technique = newTechnique;
        foreach (var x in new TechniqueClientController[] { PINPinTechnique, PatternPinTechnique, Pattern3DPinTechnique }) {
            x.gameObject.SetActive(false);
        }
        TechniqueClientController controller;
        switch (technique) {
            case Technique.PIN: controller = PINPinTechnique; break;
            case Technique.Pattern: controller = PatternPinTechnique;  break;
            case Technique.Pattern3D: controller = Pattern3DPinTechnique; break;
            default: throw new System.InvalidOperationException();
        }
        controller.gameObject.SetActive(true);
        techniqueClientPointer.Controller = controller;
        OnClientReady?.Invoke();
#if UNITY_ANDROID
        Handheld.Vibrate(); // to add permissions to manifest
#endif
    }

}
