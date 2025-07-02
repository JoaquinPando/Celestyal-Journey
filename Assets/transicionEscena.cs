using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class transicionEscena : MonoBehaviour
{
    [SerializeField] private Animator animator; // ← ahora lo asignas desde el Inspector
    [SerializeField] private AnimationClip animacionFinal;
    [SerializeField] private GameObject visualPanel;   // ← panel con la imagen de transición

    void Start()
    {
        if (visualPanel != null)
            visualPanel.SetActive(false); // oculta el panel visual al inicio
    }

    public void EjecutarTransicion()
    {
        if (visualPanel != null)
            visualPanel.SetActive(true);

        StartCoroutine(CambiarEscenas());
    }

    IEnumerator CambiarEscenas()
    {
        // Asegúrate de que el animator esté asignado
        if (animator != null)
        {
            animator.SetTrigger("Iniciar");
            yield return new WaitForSeconds(animacionFinal.length);
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogError("No se ha asignado el Animator en el script transicionEscena.");
        }
    }
}
