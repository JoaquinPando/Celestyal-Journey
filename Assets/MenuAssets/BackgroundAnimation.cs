using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorChange : MonoBehaviour
{
    public float colorChangeSpeed = 1f; // Velocidad del cambio de color
    private Image panelImage;           // Componente de imagen del panel

    void Start()
    {
        // Obtener el componente de imagen del panel
        panelImage = GetComponent<Image>();
    }

    void Update()
    {
        // Interpolación suave entre negro (0,0,0) y rojo (1,0,0)
        float t = Mathf.Sin(Time.time * colorChangeSpeed) * 0.5f + 0.5f; // Oscila entre 0 y 1
        Color color = Color.Lerp(Color.grey, Color.red, t); // Interpola entre negro y rojo
        panelImage.color = color;
    }
}