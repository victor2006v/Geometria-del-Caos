using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMutliplayer : Item{
    private float time;

    public PointMutliplayer() : base(){ 
        
    }
    public PointMutliplayer(string itemName, string description) : base(itemName, description) {
        this.time = 0f;
    }
    
}
