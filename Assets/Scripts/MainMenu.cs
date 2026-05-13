using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstSceneName = "Map1"; // ชื่อ Scene แรกของเกม

    public void PlayGame()
    {
        SceneManager.LoadScene(firstSceneName);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit"); // ใช้ดูใน Editor
    }
}