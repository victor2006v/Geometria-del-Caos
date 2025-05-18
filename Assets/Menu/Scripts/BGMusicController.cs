using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicController : MonoBehaviour
{
    public static BGMusicController instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
