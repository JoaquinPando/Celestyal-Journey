// CameraZoneGenerator.cs
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraZoneGenerator : MonoBehaviour
{
    public float offsetY = -1f;
    public float alturaZona = 1.63f;
    public int cantidadZonas = 10;
    public float largoLinea = 20f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        for (int i = 0; i < cantidadZonas; i++)
        {
            float altura = i * alturaZona + offsetY;
            Vector3 izquierda = new Vector3(transform.position.x - largoLinea / 2f, altura, 0);
            Vector3 derecha = new Vector3(transform.position.x + largoLinea / 2f, altura, 0);
            Gizmos.DrawLine(izquierda, derecha);
        }
    }
}
