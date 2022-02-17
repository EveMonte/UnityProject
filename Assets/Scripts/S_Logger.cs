using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class S_Logger
{
    public static void WriteLog(string errorMessage)
    {
        using (StreamWriter sw = new StreamWriter("Log.txt", true))
        {
            sw.WriteLine(errorMessage + "\n\n######################################################################################################\n\n");
            sw.Close();
        }
    }
}
