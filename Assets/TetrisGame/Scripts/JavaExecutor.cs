using System;
using System.Diagnostics;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using System.IO;

public class JavaExecutor : MonoBehaviour
{
    private Thread javaThread;
    private Process javaProcess;

    void Start()
    {
        javaThread = new Thread(RunJavaProgram);
        javaThread.Start();
    }

    void RunJavaProgram()
    {
        try
        {
            string jrePath = Path.Combine(Application.streamingAssetsPath, "JRE", "bin", "java.exe");
            if (!File.Exists(jrePath))
            {
                UnityEngine.Debug.LogError("No se encontró la JRE embebida en: " + jrePath);
                return;
            }

            javaProcess = new Process();
            javaProcess.StartInfo.FileName = "java";

            string jarPath = Path.Combine(Application.streamingAssetsPath, "TetrisBBDD", "lib", "mysql-connector-j-9.3.0", "mysql-connector-j-9.3.0.jar");
            string srcPath = Path.Combine(Application.streamingAssetsPath, "TetrisBBDD", "src");

            javaProcess.StartInfo.Arguments = $"-cp \"{srcPath};{jarPath}\" tetrisbbdd.TetrisBBDD";
            javaProcess.StartInfo.RedirectStandardOutput = true;
            javaProcess.StartInfo.RedirectStandardError = true;
            javaProcess.StartInfo.UseShellExecute = false;
            javaProcess.StartInfo.CreateNoWindow = true;

            javaProcess.Start();
            string output = javaProcess.StandardOutput.ReadToEnd();
            string error = javaProcess.StandardError.ReadToEnd();
            javaProcess.WaitForExit();

            UnityEngine.Debug.Log("Java output: " + output);
            UnityEngine.Debug.Log("Java error: " + error);
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("Java Execution Error: " + ex.Message);
        }
    }

    void OnApplicationQuit()
    {
        KillExistingJavaProcess();
    }

    private void KillExistingJavaProcess()
    {
        Process[] javaProcesses = Process.GetProcessesByName("java");
        foreach (Process process in javaProcesses)
        {
            try
            {
                process.Kill();
                process.WaitForExit();
                UnityEngine.Debug.Log("Proceso Java existente cerrado");
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError("Error cerrando el proceso de java existente: " + ex.Message);
            }
        }
    }
}