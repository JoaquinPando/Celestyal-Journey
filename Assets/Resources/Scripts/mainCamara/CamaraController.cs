using UnityEngine;

public class CameraControllerPorZonas : MonoBehaviour
{
    public Transform jugador;
    public CameraZoneGenerator generadorZonas; // 👈 Aquí referenciamos el generador visual
    public float desplazamientoY = 0.5f;
    public float snapDelay = 0.1f;

    private int zonaActual = 0;
    private float siguienteSnapPermitido = 0f;

    void Start()
    {
        if (generadorZonas == null)
        {
            Debug.LogError("CameraZoneGenerator no asignado al controlador de cámara.");
            enabled = false;
            return;
        }

        zonaActual = CalcularZona(jugador.position.y);
        SnapInstantaneo();
    }

    void Update()
    {
        if (Time.time < siguienteSnapPermitido) return;

        int zonaJugador = CalcularZona(jugador.position.y);
        if (zonaJugador != zonaActual)
        {
            zonaActual = zonaJugador;
            SnapInstantaneo();
            siguienteSnapPermitido = Time.time + snapDelay;
        }
    }

    int CalcularZona(float alturaJugador)
    {
        float offsetY = generadorZonas.offsetY;
        float alturaZona = generadorZonas.alturaZona;
        return Mathf.FloorToInt((alturaJugador - offsetY) / alturaZona);
    }

    void SnapInstantaneo()
    {
        float alturaZona = generadorZonas.alturaZona;
        float offsetY = generadorZonas.offsetY;

        Vector3 nuevaPos = new Vector3(
            transform.position.x,
            zonaActual * alturaZona + offsetY + desplazamientoY,
            transform.position.z
        );

        transform.position = nuevaPos;
    }
}
