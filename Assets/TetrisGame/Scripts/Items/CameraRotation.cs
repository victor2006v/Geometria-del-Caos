using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : Item{
    private float time;
    public CameraRotation() : base(){ 
    
    }
    public CameraRotation(string itemName, string description) : base(itemName, description) {
        this.time = 0f;
    }
}
