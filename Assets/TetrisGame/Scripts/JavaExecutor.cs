using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class JavaExecutor : MonoBehaviour
{
    void Start()
    {
        RunJavaProgram();
    }

    void RunJavaProgram()
    {
        Process javaProcess = new Process();
        javaProcess.StartInfo.FileName = "java";
        javaProcess.StartInfo.Arguments = "-cp \"C:/Users/Argo/Desktop/TetrisBBDD/lib/mysql-connector-j-9.3.0.jar;C:/Users/Argo/Desktop/TetrisBBDD/src\" tetrisbbdd.TetrisBBDD";
        javaProcess.StartInfo.RedirectStandardOutput = true;
        javaProcess.StartInfo.RedirectStandardError = true;
        javaProcess.StartInfo.UseShellExecute = false;
        javaProcess.StartInfo.CreateNoWindow = true;

        javaProcess.Start();
        string output = javaProcess.StandardOutput.ReadToEnd();
        string error = javaProcess.StandardError.ReadToEnd();
        javaProcess.WaitForExit();

        UnityEngine.Debug.Log("Java Output: " + output);
        if (!string.IsNullOrEmpty(error))
        {
            UnityEngine.Debug.LogError("Java Error: " + error); 
        }
    }
}
