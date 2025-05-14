using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour{
    private string itemName;
    private string description;

    [SerializeField] private SpriteRenderer spriteRenderer;
    
    public Item() { 
    }
    public Item(string itemName, string description) {
        this.itemName = itemName;
        this.description = description;
    }
    public void OnDestroy(){
        
    }
    

    public void setName(string itemName){
        this.itemName = itemName;
    }
    public string getName(string description) {
        return this.name;
    }

    public void setDescription(string description) {
        this.description = description;
    }
    public string getDescription() { 
        return this.description;
    }


    public string toString() { 
        return "Name: " + this.itemName + ", Description: " + this.description;
    }
    
}
