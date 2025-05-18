using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class AccionesMenu : MonoBehaviour{
    [SerializeField]
    public Animator settingsAnimation, keyboardAnimation, gamepadAnimation;

    [SerializeField]
    private InputSystemUIInputModule input;

    [SerializeField]
    AudioClip okSFX, selectSFX, cancelSFX;

    [SerializeField] private GameObject firstToSelect;
    [SerializeField] private GameObject keyboardToSelect;
    [SerializeField] private GameObject gamepadToSelect;

    [SerializeField]
    private InputActionAsset inputMapping;
    private InputAction cancel;

    private bool settings, keyboard, gamepad, done, done2, done3;

    private void Awake()
    {
        inputMapping.Enable();
        cancel = inputMapping.FindActionMap("UI").FindAction("Cancel");
        settings = true;
        keyboard = false;
        gamepad = false;
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstToSelect);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            done = false;
            done2 = false;
            done3 = false;
        }

        if (settings && !keyboard && !gamepad && !done)
        {
            EventSystem.current.SetSelectedGameObject(firstToSelect);
            done = true;
        }
        else if (!settings && keyboard && !gamepad && !done2)
        {
            EventSystem.current.SetSelectedGameObject(keyboardToSelect);
            done2 = true;
        }
        else if (!settings && !keyboard && gamepad && !done3)
        {
            EventSystem.current.SetSelectedGameObject(gamepadToSelect);
            done3 = true;
        }

        if (!settings && !keyboard && gamepad)
        {
            if (cancel.triggered)
            {
                BackSettingsGamepad();
            }
        }
        else if (!settings && keyboard && !gamepad)
        {
            if (cancel.triggered)
            {
                BackSettingsKeyboard();
            }
        }
        else if (settings && !keyboard && !gamepad)
        {
            if (cancel.triggered)
            {
                Back();
            }
        }
    }

    public void Back(){
        MenuManager.instance.CountClicks();
        SoundFXManager.instance.PlaySoundFXClip(cancelSFX, transform, 1f, false);
        SceneManager.LoadScene(0);;
    }
    public void Keyboard() {
        input.enabled = false;
        settings = false;
        keyboard = true;
        done2 = false;
        MenuManager.instance.CountClicks();
        SoundFXManager.instance.PlaySoundFXClip(okSFX, transform, 0.2f, false);
        settingsAnimation.SetTrigger("moveRightTrigger");
        keyboardAnimation.SetTrigger("moveUpTrigger");
    }
    public void BackSettingsKeyboard(){
        input.enabled = false;
        keyboard = false;
        settings = true;
        done = false;
        MenuManager.instance.CountClicks();
        SoundFXManager.instance.PlaySoundFXClip(cancelSFX, transform, 1f, false);
        settingsAnimation.SetTrigger("moveLeftTrigger");
        keyboardAnimation.SetTrigger("moveDownTrigger");
    }
    public void BackSettingsGamepad()
    {
        input.enabled = false;
        gamepad = false;
        settings = true;
        done = false;
        MenuManager.instance.CountClicks();
        SoundFXManager.instance.PlaySoundFXClip(cancelSFX, transform, 1f, false);
        settingsAnimation.SetTrigger("moveLeftTrigger");
        gamepadAnimation.SetTrigger("moveDownTrigger");
    }

    public void Gamepad() {
        input.enabled = false;
        settings = false;
        gamepad = true;
        done3 = false;
        MenuManager.instance.CountClicks();
        SoundFXManager.instance.PlaySoundFXClip(okSFX, transform, 0.2f, false);
        settingsAnimation.SetTrigger("moveRightTrigger");
        gamepadAnimation.SetTrigger("moveUpTrigger");
    }
}
