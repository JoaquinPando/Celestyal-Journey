using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
   public void Start(){
    GameObject.FindGameObjectWithTag("musica").GetComponent<Musicacontinua>().PlayMusic();
   }
}
