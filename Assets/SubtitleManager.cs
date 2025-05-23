using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public TextMeshProUGUI subtitleText;
    public TextMeshProUGUI noticeText;
    public string subtitleFileName = "subtitles_colored_fragmented.json";

    private List<SubtitleLine> subtitles;
    private int currentIndex = 0;
    private bool subtitlesEnabled = true;
    private Coroutine noticeCoroutine;

    void Start()
    {
        LoadSubtitles();
        StartCoroutine(SubtitleCoroutine());

        noticeText.alpha = 0f;
        noticeText.enabled = true;

        // Mostrar mensaje inicial
        ShowNotice("Subtítulos activados (S para ocultar)");
    }

    void Update()
    {
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

        // Fade In
        float duration = 0.3f;
        float elapsed = 0f;
        Color color = noticeText.color;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            noticeText.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, t));
            elapsed += Time.deltaTime;
            yield return null;
        }
        noticeText.color = new Color(color.r, color.g, color.b, 1);

        // Wait 2 seconds
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
        while (audioSource.isPlaying && currentIndex < subtitles.Count)
        {
            float currentTime = audioSource.time;
            SubtitleLine line = subtitles[currentIndex];

            if (currentTime >= line.start && currentTime <= line.end && subtitlesEnabled)
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
}
