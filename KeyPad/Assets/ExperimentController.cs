using System.Collections.Generic;
using UnityEngine;

public class ExperimentController : MonoBehaviour {

    public enum Technique { PIN, Pattern, Pattern3D };


    public int participantId;
    public Technique technique;

    // chooses techniques
    // chooses "random" pin

    public List<string> PINPins;
    public List<string> PatternPins;
    public List<string> Pattern3DPins;


}
