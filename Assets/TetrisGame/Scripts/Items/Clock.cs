using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Clock : Item{
    private float time;
    
    public Clock() : base() { 
    
    }
    public Clock(string itemName, string description) : base(itemName, description){
        this.time = 0f;
    }


}
