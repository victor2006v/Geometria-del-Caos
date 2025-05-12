using UnityEngine;
using UnityEngine.SceneManagement;

public class AccionesMenu : MonoBehaviour{
    [SerializeField] GameObject KeyboardPanel;
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject GamepadPanel;

    [SerializeField] private Statistics statistics;
    private void Start()
    {
        SettingsPanel.SetActive(true);
    }
    public void Back(){ 
        SceneManager.LoadScene(0);
        this.statistics.counterClicks++;
    }
    public void Keyboard() {

        KeyboardPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        GamepadPanel.SetActive(false);
        this.statistics.counterClicks++;
    }
    public void BackSettings()
    {
        SettingsPanel.SetActive(true);
        KeyboardPanel.SetActive(false);
        GamepadPanel.SetActive(false);
        this.statistics.counterClicks++;
    }
    public void Gamepad() {
        GamepadPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        KeyboardPanel.SetActive(false);
        this.statistics.counterClicks++;
    }
}
