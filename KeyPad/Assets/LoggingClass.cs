using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LoggingClass
{

    public static string UserID { get; set; }
    public static string Technique { get; set; }
    public static string ExperimentPinNumber { get; set; }
    public static string TrialNumber { get; set; }
    public static string ActualPin { get; set; }

    /*Event logged can be any of these:
     * Event types:
     * - drag start
     * - drag end
     * - drag move
     * - pin character entered
     * - pin entry started
     * - pin entry finished
     * - vicon motion
     * - button press
     * */

    public static void AppendToLog(string eventLogged, string dataToWrite)
    {
        Debug.Log(Application.dataPath);
        
        string path = Application.persistentDataPath + "/userLogs/" + UserID + "_" + Technique + ".txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(System.DateTime.Now + ", " + UserID + ", " + Technique + ", " + ExperimentPinNumber + ", " + TrialNumber + ", " + eventLogged + ", " + ActualPin + ", " + dataToWrite);
        writer.Close();

    }


}
