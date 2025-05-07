using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnEnableSelect : MonoBehaviour
{
    [SerializeField] GameObject objectToSelect;
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(objectToSelect);
    }
}
