using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PasswordUI : MonoBehaviour
{
    public static PasswordUI Instance;

    public GameObject keypadPanel;
    public TextMeshProUGUI inputDisplay;   // แสดงตัวเลขที่กด
    public TextMeshProUGUI errorText;      // "รหัสผิด"

    public string correctPassword = "4711"; // รหัสที่ถูกต้อง

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

    // ปุ่มตัวเลข 0-9 กดเรียกฟังก์ชันนี้
    public void PressNumber(string number)
    {
        if (currentInput.Length >= 4) return;
        currentInput += number;
        inputDisplay.text = new string('*', currentInput.Length); // แสดงเป็น ****
    }

    // ปุ่ม Confirm
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

    // ปุ่ม Clear
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