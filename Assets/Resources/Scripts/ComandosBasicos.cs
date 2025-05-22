using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    // Función que será llamada cuando se presione el botón
    public void LoadSampleScene()
    {
        // Cargar la escena llamada "Nivel"
        SceneManager.LoadScene("INTRO1");
    }
}