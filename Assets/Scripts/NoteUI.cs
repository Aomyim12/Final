using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteUI : MonoBehaviour
{
    public static NoteUI Instance;

    public GameObject notePanel;         // Panel อ่านโน้ต
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
    public GameObject prompt;            // Text "กด E เพื่ออ่าน"
    public Button closeButton;

    void Awake()
    {
        Instance = this;
        notePanel.SetActive(false);
        prompt.SetActive(false);
        closeButton.onClick.AddListener(CloseNote);
    }

    void Update()
    {
        // กด E หรือ ESC เพื่อปิด
        if (notePanel.activeSelf && Input.GetKeyDown(KeyCode.E))
            CloseNote();
    }

    public void OpenNote(string title, string content)
    {
        titleText.text = title;
        contentText.text = content;
        notePanel.SetActive(true);
        prompt.SetActive(false);
    }

    public void CloseNote()
    {
        notePanel.SetActive(false);
    }

    public void ShowPrompt(bool show)
    {
        if (!notePanel.activeSelf)
            prompt.SetActive(show);
    }
}