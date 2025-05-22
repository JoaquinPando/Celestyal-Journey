using UnityEngine;

public class Musicacontinua : MonoBehaviour
{
    private static Musicacontinua instancia;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayMusic()
    {
        if (_audioSource != null && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (_audioSource != null)
        {
            _audioSource.Stop();
        }
    }
}
