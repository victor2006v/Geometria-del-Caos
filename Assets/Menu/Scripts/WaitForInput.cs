using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class WaitForInput : MonoBehaviour
{
    [SerializeField]
    private InputSystemUIInputModule input;

    private void OnEnable()
    {
        input.enabled = false;
    }

    private void OnDisable()
    {
        Invoke("EnableInput", 0.1f);
    }

    private void EnableInput()
    {
        input.enabled = true;
    }
}
