using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteUI : MonoBehaviour
{
    public static NoteUI Instance;

    public GameObject notePanel;         
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
    public GameObject prompt;            
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
 
        if (notePanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            CloseNote();
    }

    public void OpenNote(string title, string content)
    {
        Debug.Log("OpenNote ถูกเรียก");
        titleText.text = title;
        contentText.text = content;
        notePanel.SetActive(true);
        prompt.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseNote()
    {
        notePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowPrompt(bool show)
    {
        if (!notePanel.activeSelf)
            prompt.SetActive(show);
    }
}