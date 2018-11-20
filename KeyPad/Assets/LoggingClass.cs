using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LoggingClass
{

    public static string userId;
    public static string technique;

    public static void setLoggerUser(string user)
    {
        userId = user;
    }

    public static void setLoggerTechnique(string techniqueStarted)
    {
        technique = techniqueStarted;
    }

    public static void appendToLog(string eventLogged,string dataToWrite)
    {
        string path = "Assets/userLogs/" + userId + "_" + technique + ".txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(System.DateTime.Now + ": " + eventLogged + " " + dataToWrite);
        writer.Close();

    }


}
