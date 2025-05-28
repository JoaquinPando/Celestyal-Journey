using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Header("Configuración de Movimiento Sutil")]
    [SerializeField] private float movementRange = 0.5f; // Qué tan lejos se mueve (en unidades)
    [SerializeField] private float movementSpeed = 1f; // Qué tan rápido oscila
    [SerializeField] private AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // Curva de movimiento

    [Header("Configuración de Timing")]
    [SerializeField] private float timeOffset = 0f; // Para que cada capa se mueva en diferente tiempo
    [SerializeField] private bool randomizeOffset = true; // Randomizar el offset automáticamente

    private Vector3 originalPosition;
    private float timer;

    void Start()
    {
        // Guardar la posición original
        originalPosition = transform.position;

        // Si está activado, crear un offset aleatorio para cada capa
        if (randomizeOffset)
        {
            timeOffset = Random.Range(0f, Mathf.PI * 2f);
        }

        timer = timeOffset;
    }

    void Update()
    {
        // Incrementar el timer
        timer += Time.deltaTime * movementSpeed;

        // Calcular el desplazamiento usando seno para movimiento suave
        float sineValue = Mathf.Sin(timer);

        // Aplicar la curva de animación si se desea
        float normalizedSine = (sineValue + 1f) / 2f; // Convertir de [-1,1] a [0,1]
        float curvedValue = movementCurve.Evaluate(normalizedSine);
        float finalOffset = (curvedValue * 2f - 1f) * movementRange; // Volver a [-range, range]

        // Aplicar solo al eje X, manteniendo Y y Z originales
        Vector3 newPosition = originalPosition;
        newPosition.x += finalOffset;

        transform.position = newPosition;
    }

    // Métodos para controlar la animación
    public void SetMovementRange(float newRange)
    {
        movementRange = newRange;
    }

    public void SetMovementSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

    public void PauseMovement()
    {
        enabled = false;
    }

    public void ResumeMovement()
    {
        enabled = true;
    }

    public void ResetToOriginalPosition()
    {
        transform.position = originalPosition;
        timer = timeOffset;
    }
}