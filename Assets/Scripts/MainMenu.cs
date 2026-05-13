using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstSceneName = "Map1"; // ชื่อ Scene แรกของเกม

    public void PlayGame()
    {
        SceneManager.LoadScene(firstSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit"); // ใช้ดูใน Editor
    }
}