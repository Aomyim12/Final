using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;

    [Header("UI ฉากสุดท้าย")]
    public GameObject endingPanel;           // Panel ครอบทั้งหมด
    public TextMeshProUGUI dialogueText;     // บทพูดเพื่อน / มัน
    public TextMeshProUGUI speakerText;      // ชื่อคนพูด
    public Image blackOverlay;              // หน้าจอมืด
    public Image underwaterOverlay;         // overlay สีน้ำเงินใต้น้ำ

    [Header("ปุ่มเลือก")]
    public GameObject choicePanel;          // Panel ปุ่มสองปุ่ม
    public Button acceptButton;             // รับพร
    public Button refuseButton;             // ปฏิเสธ

    [Header("เสียง")]
    public AudioSource audioSource;
    public AudioClip friendVoice;           // เสียงเพื่อน
    public AudioClip creatureSound;         // เสียง "มัน"
    public AudioClip cryingSound;           // เสียงเพื่อนร้องไห้ (Ending B)
    public AudioClip underwaterAmbience;    // เสียงใต้น้ำ (Ending A)

    void Awake()
    {
        Instance = this;
        endingPanel.SetActive(false);
        choicePanel.SetActive(false);
    }

    // เริ่ม Ending — เรียกจาก Trigger ในฉาก
    public void StartEnding()
    {
        endingPanel.SetActive(true);
        StartCoroutine(PlayEndingSequence());
    }

    IEnumerator PlayEndingSequence()
    {
        // หน้าจอมืดก่อน
        yield return StartCoroutine(FadeBlack(1f, 0f, 1f));

        // เพื่อนพูด
        yield return StartCoroutine(ShowDialogue("???", "...เธอมาถึงที่นี่แล้ว"));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(ShowDialogue("เพื่อน", "ฉันรู้ว่าเธอตามหาฉัน ฉันโกรธเลยล่ะ"));
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(ShowDialogue("เพื่อน", "แต่ฉันทำแบบนี้เพราะ... มันให้ทุกอย่างที่ฉันต้องการ"));
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(ShowDialogue("เพื่อน", "และตอนนี้ถึงเวลาของเธอแล้ว"));
        yield return new WaitForSeconds(1f);

        // เสียง "มัน" ปรากฏ
        if (creatureSound) audioSource.PlayOneShot(creatureSound);

        yield return StartCoroutine(FadeBlack(0f, 0.7f, 2f));

        yield return StartCoroutine(ShowDialogue("???", "...เจ้ามาถึงที่นี่แล้ว"));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(ShowDialogue("???", "เจ้าต้องการอยู่กับเพื่อนของเจ้าไหม"));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(ShowDialogue("???", "รับพรของข้า — และเจ้าจะไม่มีวันโดดเดี่ยวอีกต่อไป"));
        yield return new WaitForSeconds(1.5f);

        // แสดงปุ่มเลือก
        dialogueText.text = "";
        speakerText.text = "";
        choicePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        acceptButton.onClick.AddListener(StartEndingA);
        refuseButton.onClick.AddListener(StartEndingB);
    }

    // ===== ENDING A — รับพร =====
    void StartEndingA()
    {
        choicePanel.SetActive(false);
        StartCoroutine(PlayEndingA());
    }

    IEnumerator PlayEndingA()
    {
        yield return StartCoroutine(ShowDialogue("ตัวละครหลัก", "...ฉันรับ"));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(ShowDialogue("เพื่อน", "ฉันรู้ว่าเธอจะเลือกแบบนี้"));
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(ShowDialogue("เพื่อน", "ยินดีต้อนรับ... บ้าน"));
        yield return new WaitForSeconds(2f);

        // Fade มืด แล้วเปิด underwater overlay
        yield return StartCoroutine(FadeBlack(0f, 1f, 2f));
        yield return new WaitForSeconds(1f);

        if (underwaterAmbience)
        {
            audioSource.clip = underwaterAmbience;
            audioSource.Play();
        }

        // เปลี่ยนเป็นสีน้ำเงินใต้น้ำ
        yield return StartCoroutine(FadeUnderwater(0f, 0.6f, 3f));
        yield return StartCoroutine(FadeBlack(1f, 0f, 3f));

        // ข้อความปิด
        yield return StartCoroutine(ShowDialogue("", "แสงจากโลกภายนอกค่อย ๆ เลือนหาย"));
        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(FadeBlack(0f, 1f, 3f));

        // จบเกม
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Credits"); // หรือ MainMenu
    }

    // ===== ENDING B — ปฏิเสธ =====
    void StartEndingB()
    {
        choicePanel.SetActive(false);
        StartCoroutine(PlayEndingB());
    }

    IEnumerator PlayEndingB()
    {
        yield return StartCoroutine(ShowDialogue("ตัวละครหลัก", "...ฉันปฏิเสธ"));
        yield return new WaitForSeconds(1f);

        // เพื่อนสะดุด
        yield return StartCoroutine(ShowDialogue("เพื่อน", "..."));
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(ShowDialogue("เพื่อน", "ทำไม"));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(ShowDialogue("ตัวละครหลัก", "เพราะฉันยังเป็นตัวเอง"));
        yield return new WaitForSeconds(2f);

        // Fade มืด
        yield return StartCoroutine(FadeBlack(0f, 1f, 3f));
        yield return new WaitForSeconds(1f);

        // ได้ยินเสียงร้องไห้ในความมืด
        if (cryingSound) audioSource.PlayOneShot(cryingSound);

        dialogueText.text = "...";
        speakerText.text = "";
        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(ShowDialogue("", "ในความมืด ยังได้ยินเสียงเพื่อนร้องไห้"));
        yield return new WaitForSeconds(4f);

        // จบเกม
        SceneManager.LoadScene("Credits");
    }

    // ===== Helper Functions =====

    IEnumerator ShowDialogue(string speaker, string dialogue)
    {
        speakerText.text = speaker;
        dialogueText.text = "";

        // พิมพ์ทีละตัว
        foreach (char c in dialogue)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1.5f);
    }

    IEnumerator FadeBlack(float from, float to, float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, t / duration);
            blackOverlay.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    IEnumerator FadeUnderwater(float from, float to, float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, t / duration);
            underwaterOverlay.color = new Color(0.05f, 0.1f, 0.3f, alpha);
            yield return null;
        }
    }
}