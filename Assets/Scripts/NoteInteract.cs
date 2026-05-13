using UnityEngine;

public class NoteInteract : MonoBehaviour
{
    [TextArea(5, 20)]
    public string noteContent;   // พิมพ์เนื้อหาโน้ตตรงนี้เลย

    public string noteTitle;     // ชื่อโน้ต

    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            NoteUI.Instance.OpenNote(noteTitle, noteContent);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            NoteUI.Instance.ShowPrompt(true);  // แสดง "กด E เพื่ออ่าน"
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            NoteUI.Instance.ShowPrompt(false);
        }
    }
}