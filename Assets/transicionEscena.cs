using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class transicionEscena : MonoBehaviour
{
   private Animator animator;
   [SerializeField] private AnimationClip animacionFinal;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CambiarEscenas());
        }   
    }
    IEnumerator CambiarEscenas(){
        animator.SetTrigger("Iniciar");
        
        yield return new WaitForSeconds(animacionFinal.length);

        SceneManager.LoadScene(1);

    }
}
