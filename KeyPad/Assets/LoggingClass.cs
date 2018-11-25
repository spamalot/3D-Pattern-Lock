using System.IO;
using UnityEngine;

public static class LoggingClass
{

    public static string UserID { get; set; }
    public static string Technique { get; set; }
    public static string ExperimentPinNumber { get; set; }
    public static string TrialNumber { get; set; }
    public static string ActualPin { get; set; }
    public static string EnteredPin { get; set; }

    public const string DRAG_START = "DRAG_START";
    public const string DRAG_MOVE = "DRAG_MOVE";
    public const string DRAG_END = "DRAG_END";
    public const string ENTERED_PIN_CHANGE = "ENTERED_PIN_CHANGE";
    public const string ENTRY_START = "ENTRY_START";
    public const string ENTRY_END = "ENTRY_END";
    public const string VICON = "VICON";
    public const string BUTTON_PRESS = "BUTTON_PRESS";


    private static string _ToRString(object x) {
        if (x == null) {
            return "NA";
        }
        return x.ToString();
    }

        // x, y, z, pincorrect, buttontext
    public static void AppendToLog(string eventType, object x, object y, object z, object buttonText, object pinCorrect)
    {
        Debug.Log(Application.dataPath);
        
        string path = Application.persistentDataPath + "/userLogs/" + UserID + "_" + Technique + ".txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(string.Join(",",
            new object[] {

                // Metadata
                System.DateTime.Now,
                System.DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                UserID,
                Technique,
                ExperimentPinNumber,
                TrialNumber,
                ActualPin,

                // Event Data
                eventType,
                EnteredPin,

                _ToRString(x),
                _ToRString(y),
                _ToRString(z),
                _ToRString(buttonText),
                _ToRString(pinCorrect),

            }));
        writer.Close();

    }


}
