using UnityEngine;

public class HorrorAudio : MonoBehaviour
{
    public static HorrorAudio Instance;

    public AudioSource audioSource;

    void Awake()
    {
        Instance = this;
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayAndFade(AudioClip clip, float duration = 2f)
    {
        StartCoroutine(DoPlayAndFade(clip, duration));
    }

    System.Collections.IEnumerator DoPlayAndFade(AudioClip clip, float duration)
    {
        audioSource.clip = clip;
        audioSource.volume = 1f;
        audioSource.Play();

        yield return new WaitForSeconds(clip.length - duration);

        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(1, 0, t / duration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 1f;
    }
}