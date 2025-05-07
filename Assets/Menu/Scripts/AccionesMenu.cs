using UnityEngine;
using UnityEngine.SceneManagement;

public class AccionesMenu : MonoBehaviour{
    [SerializeField] GameObject KeyboardCanvas;
    [SerializeField] GameObject SettingsCanvas;
    [SerializeField] GameObject GamepadCanvas;
    public void Back(){ 
        SceneManager.LoadScene(0);
    }
    public void Keyboard() {

        KeyboardCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
        GamepadCanvas.SetActive(false);
    }
    public void BackSettings()
    {
        SettingsCanvas.SetActive(true);
        KeyboardCanvas.SetActive(false);
        GamepadCanvas.SetActive(false);
    }
    public void Gamepad() {
        GamepadCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
        KeyboardCanvas.SetActive(false);
    }
}
