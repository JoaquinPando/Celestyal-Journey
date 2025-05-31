using System.Collections;
using UnityEngine;

public class JumpDialogue : MonoBehaviour
{
    public GameObject dialoguePrefab;  // Prefab con tu cuadro PNG
    public AudioClip jumpAudio;        // Tu clip de voz

    private GameObject currentDialogue;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// Instancia el prefab, lo destruye al cabo de unos segundos y reproduce el audio.
    /// </summary>
    public void ShowDialogue()
    {
        if (dialoguePrefab != null && currentDialogue == null)
        {
            Vector3 spawnPos = transform.position + new Vector3(0, 1.5f, 0);
            currentDialogue = Instantiate(dialoguePrefab, spawnPos, Quaternion.identity);
            currentDialogue.transform.SetParent(null); 
            Destroy(currentDialogue, 2f);
        }

        if (jumpAudio != null)
        {
            audioSource.PlayOneShot(jumpAudio);
        }
    }
}
