using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class AnimatorController : MonoBehaviour
{
    [SerializeField]
    private InputSystemUIInputModule input;

    [SerializeField]
    private MenuInicio menu;

    void ActivateInputSystem()
    {
        input.enabled = true;
    }
}
