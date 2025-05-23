using UnityEngine;
 // Necesario para 2D Light

public class LightPulse : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D lightSource;    // La fuente de luz 2D
    public float pulseSpeed = 1f;  // Velocidad del pulso
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;

    void Update()
    {
        // Oscila la intensidad de la luz
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f);
        lightSource.intensity = intensity;
    }
}