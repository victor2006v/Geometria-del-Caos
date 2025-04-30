
using UnityEngine;

public class SingleTon : MonoBehaviour
{
    public static SingleTon instance { get; private set; }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } 
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
