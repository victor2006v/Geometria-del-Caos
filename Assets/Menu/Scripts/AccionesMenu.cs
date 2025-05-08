using UnityEngine;
using UnityEngine.SceneManagement;

public class AccionesMenu : MonoBehaviour{
    [SerializeField] GameObject KeyboardCanvas;
    [SerializeField] GameObject SettingsCanvas;
    [SerializeField] GameObject GamepadCanvas;

    [SerializeField] private Statistics statistics;
    public void Back(){ 
        SceneManager.LoadScene(0);
        this.statistics.counterClicks++;
    }
    public void Keyboard() {

        KeyboardCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
        GamepadCanvas.SetActive(false);
        this.statistics.counterClicks++;
    }
    public void BackSettings()
    {
        SettingsCanvas.SetActive(true);
        KeyboardCanvas.SetActive(false);
        GamepadCanvas.SetActive(false);
        this.statistics.counterClicks++;
    }
    public void Gamepad() {
        GamepadCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
        KeyboardCanvas.SetActive(false);
        this.statistics.counterClicks++;
    }
}
