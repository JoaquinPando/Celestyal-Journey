using UnityEngine;

public class Musicacontinua : MonoBehaviour
{
    private static AudioSource _audioSource;

    private void Awake()
    {
        if(_audioSource != null){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(this.gameObject);
            _audioSource = GetComponent<AudioSource>();
        }
    }   

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
