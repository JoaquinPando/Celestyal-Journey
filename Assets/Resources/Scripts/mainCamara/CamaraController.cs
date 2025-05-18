using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform objetivo;
    public float velocidadCamara = 0.025f;
    public Vector3 desplazamiento;

    void Start()
    {

    }
    void Update()
    {

    }

    private void LateUpdate()
    {
        Vector3 posicionDeseada = objetivo.position + desplazamiento;
        Vector3 posicionSuave = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamara);
        transform.position = posicionSuave;
    }

}
