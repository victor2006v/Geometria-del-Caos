using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBackground : MonoBehaviour
{
    /**
     * This piece of code is for not destroying 
     * the GameObject, which contains the music
     * if we load another Scene
     */
    private void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
}
