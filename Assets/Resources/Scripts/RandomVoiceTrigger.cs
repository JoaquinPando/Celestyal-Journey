using System.Collections;
using UnityEngine;

public class RandomVoiceTrigger : MonoBehaviour
{
    [Header("Configuración del audio")]
    public AudioClip danteJumpVoice;
    public float minDelay = 60f;   // mínimo 1 minuto
    public float maxDelay = 180f;  // máximo 3 minutos

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(PlayRandomVoiceCoroutine());
    }

    IEnumerator PlayRandomVoiceCoroutine()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            if (danteJumpVoice != null)
            {
                audioSource.PlayOneShot(danteJumpVoice);
            }
        }
    }
}
