using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using System.IO;
using Newtonsoft.Json;

public class SubtitleManager : MonoBehaviour
{
    [System.Serializable]
    public class SubtitleLine
    {
        public float start;
        public float end;
        public string text;
    }

    public AudioSource audioSource;
    public VideoPlayer videoPlayer;
    public TextMeshProUGUI subtitleText;
    public TextMeshProUGUI noticeText;
    public string subtitleFileName = "subtitles_colored_fragmented.json";
    public Animator transicionAnimator; 
    public string triggerNombre = "IniciarTransicion"; 
    public transicionEscena controladorTransicion;


    private List<SubtitleLine> subtitles;
    private int currentIndex = 0;
    private bool subtitlesEnabled = true;
    private Coroutine noticeCoroutine;
    private Coroutine subtitleCoroutine;

    void Start()
    {
        LoadSubtitles();
        subtitleCoroutine = StartCoroutine(SubtitleCoroutine());

        noticeText.alpha = 0f;
        noticeText.enabled = true;

        // Mostrar mensaje inicial
        ShowNotice("Subtítulos activados (S para ocultar)");
    }

    void Update()
    {
        // Toggle subtítulos con S
        if (Input.GetKeyDown(KeyCode.S))
        {
            subtitlesEnabled = !subtitlesEnabled;
            subtitleText.enabled = subtitlesEnabled;

            if (!subtitlesEnabled)
                subtitleText.text = "";

            ShowNotice(subtitlesEnabled
                ? "Subtítulos activados (S para ocultar)"
                : "Subtítulos desactivados (S para mostrar)");
        }

        // Saltar video con ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipVideo();
        }
    }

    void LoadSubtitles()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, subtitleFileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            subtitles = JsonConvert.DeserializeObject<List<SubtitleLine>>(json);
        }
        else
        {
            Debug.LogError("No se encontró el archivo de subtítulos: " + filePath);
        }
    }

    void ShowNotice(string message)
    {
        if (noticeCoroutine != null)
            StopCoroutine(noticeCoroutine);

        noticeCoroutine = StartCoroutine(ShowNoticeCoroutine(message));
    }

    IEnumerator ShowNoticeCoroutine(string message)
    {
        noticeText.text = message;
        noticeText.enabled = true;

        float duration = 0.3f;
        float elapsed = 0f;
        Color color = noticeText.color;

        // Fade In
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            noticeText.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, t));
            elapsed += Time.deltaTime;
            yield return null;
        }

        noticeText.color = new Color(color.r, color.g, color.b, 1);

        // Wait before fade out
        yield return new WaitForSeconds(2f);

        // Fade Out
        elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            noticeText.color = new Color(color.r, color.g, color.b, Mathf.Lerp(1, 0, t));
            elapsed += Time.deltaTime;
            yield return null;
        }

        noticeText.color = new Color(color.r, color.g, color.b, 0);
        noticeText.text = "";
    }

    IEnumerator SubtitleCoroutine()
    {
        subtitleText.text = "";

        while (currentIndex < subtitles.Count)
        {
            float currentTime = audioSource.time;

            if (currentIndex >= subtitles.Count)
                break;

            SubtitleLine line = subtitles[currentIndex];

            if (subtitlesEnabled && currentTime >= line.start && currentTime <= line.end)
            {
                subtitleText.text = line.text;
            }
            else if (currentTime > line.end)
            {
                subtitleText.text = "";
                currentIndex++;
            }

            yield return null;
        }

        subtitleText.text = "";
    }


    void SkipVideo()
{
    if (audioSource.isPlaying)
        audioSource.Stop();

    if (videoPlayer != null && videoPlayer.isPlaying)
        videoPlayer.Stop();

    if (subtitleCoroutine != null)
        StopCoroutine(subtitleCoroutine);

    subtitleText.text = "";
    subtitleText.enabled = false;

    ShowNotice("Video saltado");

    // 🔥 Ejecutar la transición (mostrar imagen y cambiar escena)
    if (controladorTransicion != null)
        controladorTransicion.EjecutarTransicion();
}


}
