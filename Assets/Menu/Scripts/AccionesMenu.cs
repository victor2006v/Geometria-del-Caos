using UnityEngine;
using UnityEngine.SceneManagement;

public class AccionesMenu : MonoBehaviour{
    [SerializeField] GameObject KeyboardCanvas;
    [SerializeField] GameObject SettingsCanvas;
    public void Back(){ 
        SceneManager.LoadScene(0);
    }
    public void Keyboard() {

        KeyboardCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
    }
    public void BackSettings()
    {
        SettingsCanvas.SetActive(true);
        KeyboardCanvas.SetActive(false);
    }
}
