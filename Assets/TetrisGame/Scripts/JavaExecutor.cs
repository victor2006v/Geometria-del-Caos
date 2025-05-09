using System.Diagnostics;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class JavaExecutor : MonoBehaviour
{
    private Thread javaThread;
    private Process javaProcess;
    private bool isJavaRunning = false;

    void Start()
    {
        if (!isJavaRunning)
        {
            javaThread = new Thread(RunJavaProgram);
            javaThread.Start();
        }
        else
        {
            UnityEngine.Debug.LogWarning("El proceso java ya está en ejecución.");
        }
    }

    void RunJavaProgram()
    {
        try
        {
            isJavaRunning = true;

            if (javaProcess != null && !javaProcess.HasExited)
            {
                UnityEngine.Debug.LogWarning("El proceso Java ya está en ejecución.");
                return;
            }

            javaProcess = new Process();
            javaProcess.StartInfo.FileName = "java";

            string jarPath = "C:\\Users\\Argo\\Desktop\\Geometria-del-Caos\\Assets\\TetrisBBDD\\lib\\mysql-connector-j-9.3.0\\mysql-connector-j-9.3.0.jar";
            string srcPath = "C:\\Users\\Argo\\Desktop\\Geometria-del-Caos\\Assets\\TetrisBBDD\\src";

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
        finally
        {
            isJavaRunning = false;
        }

        void OnApplicationQuit()
        {
            if (javaProcess != null && !javaProcess.HasExited)
            {
                javaProcess.Kill();
                javaProcess.WaitForExit();
            }
            if (javaThread != null && javaThread.IsAlive)
            {
                javaThread.Join();
            }
        }
    }
}