using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PasswordUI : MonoBehaviour
{
    public static PasswordUI Instance;

    public GameObject keypadPanel;
    public TextMeshProUGUI inputDisplay;   
    public TextMeshProUGUI errorText;      

    public string correctPassword = "4711"; 

    private string currentInput = "";
    private DoorController targetDoor;

    void Awake()
    {
        Instance = this;
        keypadPanel.SetActive(false);
    }

    public void OpenKeypad(DoorController door)
    {
        targetDoor = door;
        currentInput = "";
        inputDisplay.text = "";
        errorText.gameObject.SetActive(false);
        keypadPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseKeypad()
    {
        keypadPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PressNumber(string number)
    {
        if (currentInput.Length >= 4) return;
        currentInput += number;
        inputDisplay.text = new string('*', currentInput.Length); // แสดงเป็น ****
    }

    public void PressConfirm()
    {
        if (currentInput == correctPassword)
        {
            targetDoor.Unlock();
            CloseKeypad();
        }
        else
        {
            errorText.gameObject.SetActive(true);
            currentInput = "";
            inputDisplay.text = "";
        }
    }

    public void PressClear()
    {
        currentInput = "";
        inputDisplay.text = "";
        errorText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (keypadPanel.activeSelf && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseKeypad();
            
        }
    }
}