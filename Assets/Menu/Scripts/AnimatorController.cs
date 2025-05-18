using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class AnimatorController : MonoBehaviour
{
    [SerializeField]
    private InputSystemUIInputModule input;

    public void DisableInputSystem()
    {
        input.enabled = false;
    }

    public void ActivateInputSystem()
    {
        input.enabled = true;
    }
}
