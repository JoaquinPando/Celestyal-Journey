using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

   public void Start()
   {
      GameObject musica = GameObject.FindGameObjectWithTag("musica");

      if (musica != null)
      {
         Musicacontinua controlador = musica.GetComponentInChildren<Musicacontinua>();
         if (controlador != null)
         {
            controlador.PlayMusic();
         }
         else
         {
            Debug.LogWarning("No se encontró Musicacontinua en hijos de 'musica'");
         }
      }
      else
      {
         Debug.LogWarning("No se encontró ningún objeto con el tag 'musica'");
      }
   }



   /*
   public void Start()
   {
      GameObject.FindGameObjectWithTag("musica").GetComponent<Musicacontinua>().PlayMusic();
   }
   */
}
