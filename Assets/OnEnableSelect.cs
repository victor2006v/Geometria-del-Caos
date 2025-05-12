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
    private void Update(){
        if (Input.GetMouseButtonDown(0)){
            EventSystem.current.SetSelectedGameObject(objectToSelect);
        }
    }
    
}
