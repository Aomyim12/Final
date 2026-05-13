using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HorrorUI : MonoBehaviour
{
    public static HorrorUI Instance;

    [Header("Flash Image — ภาพที่แฟลชแล้วหาย")]
    public Image flashImage;             
    public float flashDuration = 0.15f;  

    [Header("Overlay — หน้าจอมืดกะทันหัน")]
    public Image blackOverlay;           

    [Header("Narration — ข้อความบรรยาย")]
    public TextMeshProUGUI narrationText;
    public float narrationDuration = 3f;

    void Awake()
    {
        Instance = this;
        flashImage.gameObject.SetActive(false);
        blackOverlay.gameObject.SetActive(false);
        narrationText.gameObject.SetActive(false);
    }

    // ===== แฟลชภาพ =====
    public void FlashImage(Sprite sprite)
    {
        StopAllCoroutines();
        StartCoroutine(DoFlash(sprite));
    }

    IEnumerator DoFlash(Sprite sprite)
    {
        flashImage.sprite = sprite;
        flashImage.gameObject.SetActive(true);
        flashImage.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(flashDuration);

        float t = 0;
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, t / 0.3f);
            flashImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        flashImage.gameObject.SetActive(false);
    }

    // ===== หน้าจอมืดวูบ =====
    public void BlackFlash()
    {
        StartCoroutine(DoBlackFlash());
    }

    IEnumerator DoBlackFlash()
    {
        blackOverlay.gameObject.SetActive(true);
        blackOverlay.color = new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(0.05f);

        float t = 0;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, t / 0.5f);
            blackOverlay.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        blackOverlay.gameObject.SetActive(false);
    }

    // ===== ข้อความบรรยาย =====
    public void ShowNarration(string text)
    {
        StopCoroutine("DoNarration");
        StartCoroutine(DoNarration(text));
    }

    IEnumerator DoNarration(string text)
    {
        narrationText.text = text;
        narrationText.gameObject.SetActive(true);
        narrationText.color = new Color(1, 1, 1, 0);

        
        float t = 0;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            narrationText.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t / 0.5f));
            yield return null;
        }

        yield return new WaitForSeconds(narrationDuration);

        
        t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime;
            narrationText.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, t / 1f));
            yield return null;
        }

        narrationText.gameObject.SetActive(false);
    }
}