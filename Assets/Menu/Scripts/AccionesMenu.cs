using UnityEngine;
using UnityEngine.SceneManagement;

public class AccionesMenu : MonoBehaviour{
    [SerializeField] GameObject KeyboardPanel;
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject GamepadPanel;

    

    private void Start(){
        SettingsPanel.SetActive(true);
    }
    public void Back(){ 
        SceneManager.LoadScene(0);;
    }
    public void Keyboard() {
        MenuManager.instance.CountClicks();
        KeyboardPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        GamepadPanel.SetActive(false);

    }
    public void BackSettings(){
        MenuManager.instance.CountClicks();
        SettingsPanel.SetActive(true);
        KeyboardPanel.SetActive(false);
        GamepadPanel.SetActive(false);
    }
    public void Gamepad() {
        MenuManager.instance.CountClicks();
        GamepadPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        KeyboardPanel.SetActive(false);
    }
}
