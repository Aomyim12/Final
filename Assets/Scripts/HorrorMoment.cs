using UnityEngine;

public class HorrorMoment : MonoBehaviour
{
    [Header("ประเภท Moment")]
    public MomentType momentType;

    public enum MomentType
    {
        FlashImage,      // แฟลชภาพ
        BlackFlash,      // หน้าจอมืดวูบ
        Narration,       // ข้อความในใจ
        SoundOnly,       // เสียงอย่างเดียว
        Combined         // หลายอย่างพร้อมกัน
    }

    [Header("ภาพ")]
    public Sprite horrorSprite;

    [Header("เสียง")]
    public AudioClip horrorSound;
    public float soundVolume = 1f;

    [Header("ข้อความ")]
    [TextArea(3, 6)]
    public string narrationText;

    [Header("ตัวเลือก")]
    public bool triggerOnce = true;     
    public float delayBeforeTrigger = 0f; 

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (triggerOnce && hasTriggered) return;

        hasTriggered = true;

        if (delayBeforeTrigger > 0)
            Invoke("TriggerMoment", delayBeforeTrigger);
        else
            TriggerMoment();
    }

    void TriggerMoment()
    {
        switch (momentType)
        {
            case MomentType.FlashImage:
                if (HorrorUI.Instance != null)
                    HorrorUI.Instance.FlashImage(horrorSprite);
                break;

            case MomentType.BlackFlash:
                if (HorrorUI.Instance != null)
                    HorrorUI.Instance.BlackFlash();
                break;

            case MomentType.Narration:
                if (HorrorUI.Instance != null)
                    HorrorUI.Instance.ShowNarration(narrationText);
                break;

            case MomentType.SoundOnly:
                if (HorrorAudio.Instance != null)
                    HorrorAudio.Instance.PlayAndFade(horrorSound);
                break;

            case MomentType.Combined:
                if (horrorSprite && HorrorUI.Instance != null)
                    HorrorUI.Instance.FlashImage(horrorSprite);
                if (horrorSound && HorrorAudio.Instance != null)
                    HorrorAudio.Instance.PlaySound(horrorSound, soundVolume);
                if (!string.IsNullOrEmpty(narrationText) && HorrorUI.Instance != null)
                    HorrorUI.Instance.ShowNarration(narrationText);
                break;
        }
    }
}