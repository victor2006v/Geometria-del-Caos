using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class ActivateInputSystem : MonoBehaviour
{
    [SerializeField]
    private InputSystemUIInputModule input;

    void Activate()
    {
        input.enabled = true;
    }
}
