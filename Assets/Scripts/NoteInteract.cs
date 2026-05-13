using UnityEngine;

public class NoteInteract : MonoBehaviour
{
    [TextArea(5, 20)]
    public string noteContent;   

    public string noteTitle;    

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
            NoteUI.Instance.ShowPrompt(true);  
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