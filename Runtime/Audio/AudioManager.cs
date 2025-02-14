using System.Collections;
using UnityEngine;

/// <summary>
/// Управляет аудио в игре.
/// </summary>
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    /// <summary>
    /// Источник для музыки.
    /// </summary>
    private static AudioSource _musicSource;

    /// <summary>
    /// Источник для звуковых эффектов.
    /// </summary>
    private static AudioSource _sfxSource;

    /// <summary>
    /// Громкость музыки.
    /// </summary>
    public static float MusicVolume { get; private set; } = 1f;

    /// <summary>
    /// Громкость звуковых эффектов.
    /// </summary>
    public static float SfxVolume { get; private set; } = 1f;


    public void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
        _musicSource = gameObject.transform.Find("Music").GetComponent<AudioSource>();
        _sfxSource = gameObject.transform.Find("Sfx").GetComponent<AudioSource>();
    }

    /// <summary>
    /// Устанавливает громкость музыки.
    /// </summary>
    /// <param name="volume"></param>
    public static void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
        _musicSource.volume = volume;
    }

    /// <summary>
    /// Устанавливает громкость звуковых эффектов.
    /// </summary>
    /// <param name="volume"></param>
    public static void SetSfxVolume(float volume)
    {
        SfxVolume = volume;
        _sfxSource.volume = volume;
    }

    /// <summary>
    /// Проигрывает музыку.
    /// </summary>
    public static void PlayMusic(AudioClip clip)
    {
        _musicSource.volume = MusicVolume;
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    /// <summary>
    /// Останавливает проигрывание музыки плавно.
    /// </summary>
    /// <param name="durationInMilliseconds"></param>
    public static void FadeOutMusic(int durationInMilliseconds = 1000)
    {
        if (_musicSource == null)
            return;
        _instance.StartCoroutine(FadeOutCoroutine(durationInMilliseconds));

    }

    /// <summary>
    /// Начинает проигрывать музыку плавно.
    /// </summary>
    /// <param name="durationInMilliseconds"></param>
    public static void FadeInMusic(AudioClip clip, int fadeDurationInMilliseconds = 1000)
    {
        if (_musicSource == null || clip == null)
            return;
        _instance.StartCoroutine(FadeInCoroutine(clip, fadeDurationInMilliseconds));
    }

    /// <summary>
    /// Проигрывает звуковой эффект.
    /// </summary>
    /// <param name="clip"></param>
    public static void PlaySfx(AudioClip clip)
    {
        _sfxSource.volume = SfxVolume;
        _sfxSource.PlayOneShot(clip);
    }


    // Корутин для fadeIn
    private static IEnumerator FadeInCoroutine(AudioClip clip, int fadeDurationInMilliseconds = 1000)
    {
        _musicSource.clip = clip;
        _musicSource.volume = 0;
        _musicSource.Play();

        float fadeDurationInSeconds = fadeDurationInMilliseconds / 1000f;
        float startVolume = 0.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDurationInSeconds)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDurationInSeconds;
            _musicSource.volume = Mathf.Lerp(startVolume, MusicVolume, Mathf.Pow(t, 2));
            yield return null;
        }

        _musicSource.volume = MusicVolume;
    }



    // Корутин для fadeOut
    private static IEnumerator FadeOutCoroutine(int durationInMilliseconds = 1000)
    {
        float fadeDurationInSeconds = durationInMilliseconds / 1000f;
        float startVolume = _musicSource.volume;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDurationInSeconds)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDurationInSeconds;
            _musicSource.volume = Mathf.Lerp(startVolume, 0, Mathf.Pow(t, 2));
            yield return null;
        }

        _musicSource.volume = 0;
        _musicSource.Stop();
    }
}
