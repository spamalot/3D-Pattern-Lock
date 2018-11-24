using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LoggingClass
{

    public static string userId;
    public static string technique;
    public static string pinTestedNumber;
    public static string pinTrailNumber;
    public static string actualPin;

    public static void setLoggerUser(string user)
    {
        userId = user;
    }

    public static void setLoggerTechnique(string techniqueStarted)
    {
        technique = techniqueStarted;
    }

    public static void setLoggerPinTestedNumber (string pinNumber)
    {
        pinTestedNumber = pinNumber;
    }

    public static void setLoggerPinTrailNumber (string pinTrailNum)
    {
        pinTrailNumber = pinTrailNum;
    }

    public static void setLoggerActualPin (string actualPinNumber)
    {
        actualPin = actualPinNumber;
    }

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

    public static void appendToLog(string eventLogged,string dataToWrite)
    {
        Debug.Log(Application.dataPath);
        
        string path = Application.persistentDataPath + "/userLogs/" + userId + "_" + technique + ".txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(System.DateTime.Now + ", " + userId + ", " + technique + ", " + pinTestedNumber + ", " + pinTrailNumber + ", " + eventLogged + ", " + actualPin + ", " + dataToWrite);
        writer.Close();

    }


}
