using UnityEngine;
using UnityEngine.Video; // Necesario para VideoPlayer
using UnityEngine.UI; // Necesario si vas a interactuar con elementos de UI

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Arrastra tu VideoPlayer aquí desde el Inspector
    public GameObject endScreenCanvas; // Arrastra tu EndScreenCanvas aquí

    void Start()
    {
        // Asegúrate de que el Canvas de la pantalla final esté deshabilitado al inicio
        if (endScreenCanvas != null)
        {
            endScreenCanvas.SetActive(false);
        }

        // Suscríbete al evento que se dispara cuando el video termina
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            // Si quieres que el video empiece a reproducirse automáticamente al inicio del juego
            // videoPlayer.Play(); 
        }
    }

    // Esta función se llamará cuando el video termine (o llegue a su punto de bucle)
    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("¡El video ha terminado!");

        // Habilita el Canvas de la pantalla final
        if (endScreenCanvas != null)
        {
            endScreenCanvas.SetActive(true);
        }

        // Opcional: Pausar o detener el VideoPlayer si no lo quieres seguir viendo
        videoPlayer.Stop(); 

        // Opcional: Puedes añadir un botón para que el usuario presione Espacio
        // y cargue la siguiente escena, por ejemplo.
        // Si necesitas detectar la tecla Espacio, puedes hacerlo en un Update()
        // if (Input.GetKeyDown(KeyCode.Space)) { SceneManager.LoadScene("NombreDeTuSiguienteEscena"); }
    }

    // Es buena práctica desuscribirse del evento cuando el objeto se destruye
    void OnDisable()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}