using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Statistics : MonoBehaviour
{
    public int counterClicks;
    private float timer = 0f;
    private float timeInterval = 2f; // Interval to print clicks
    /*To Initialize the clicks*/
    private void Awake()
    {
        counterClicks = 0;
    }

    private void imprimirClicks() {
        Debug.Log("Clicks: " + counterClicks);
    }
    // Update is called once per frame
    private void Update(){
    }

}
