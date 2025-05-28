using UnityEngine;

public class SubtleParallaxController : MonoBehaviour
{
    [System.Serializable]
    public class LayerConfig
    {
        public Transform layerTransform;
        [Range(0.1f, 2f)]
        public float movementRange = 0.5f;
        [Range(0.1f, 3f)]
        public float movementSpeed = 1f;
        public string layerName;
    }

    [Header("Configuración Global")]
    [SerializeField] private float globalMovementRange = 0.5f;
    [SerializeField] private float globalMovementSpeed = 1f;
    [SerializeField] private bool pauseAllMovement = false;

    [Header("Auto Setup para Hijos")]
    [SerializeField] private bool autoSetupChildren = true;
    [SerializeField] private float[] rangeMultipliers = { 0.3f, 0.5f, 0.7f, 1f, 1.2f }; // Diferentes rangos para cada capa
    [SerializeField] private float[] speedMultipliers = { 0.8f, 0.9f, 1f, 1.1f, 1.3f }; // Diferentes velocidades

    [Header("Configuración Manual")]
    [SerializeField] private LayerConfig[] manualLayers;

    private ParallaxLayer[] layerComponents;

    void Start()
    {
        if (autoSetupChildren)
        {
            AutoSetupLayers();
        }
        else
        {
            SetupManualLayers();
        }
    }

    void AutoSetupLayers()
    {
        int childCount = transform.childCount;
        layerComponents = new ParallaxLayer[childCount];

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // Agregar componente si no existe
            ParallaxLayer layerComponent = child.GetComponent<ParallaxLayer>();
            if (layerComponent == null)
            {
                layerComponent = child.gameObject.AddComponent<ParallaxLayer>();
            }

            layerComponents[i] = layerComponent;

            // Configurar rango y velocidad basado en el índice
            float rangeMultiplier = i < rangeMultipliers.Length ? rangeMultipliers[i] : 1f;
            float speedMultiplier = i < speedMultipliers.Length ? speedMultipliers[i] : 1f;

            layerComponent.SetMovementRange(globalMovementRange * rangeMultiplier);
            layerComponent.SetMovementSpeed(globalMovementSpeed * speedMultiplier);

            Debug.Log($"Configurada capa {child.name}: Rango={globalMovementRange * rangeMultiplier:F2}, Velocidad={globalMovementSpeed * speedMultiplier:F2}");
        }
    }

    void SetupManualLayers()
    {
        if (manualLayers == null || manualLayers.Length == 0) return;

        layerComponents = new ParallaxLayer[manualLayers.Length];

        for (int i = 0; i < manualLayers.Length; i++)
        {
            if (manualLayers[i].layerTransform == null) continue;

            ParallaxLayer layerComponent = manualLayers[i].layerTransform.GetComponent<ParallaxLayer>();
            if (layerComponent == null)
            {
                layerComponent = manualLayers[i].layerTransform.gameObject.AddComponent<ParallaxLayer>();
            }

            layerComponents[i] = layerComponent;
            layerComponent.SetMovementRange(manualLayers[i].movementRange);
            layerComponent.SetMovementSpeed(manualLayers[i].movementSpeed);
        }
    }

    void Update()
    {
        // Control con teclas para testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pauseAllMovement = !pauseAllMovement;
            ToggleAllMovement();
        }

        // Aumentar/disminuir rango global
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            AdjustGlobalRange(-0.5f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            AdjustGlobalRange(0.5f * Time.deltaTime);
        }

        // Aumentar/disminuir velocidad global
        if (Input.GetKey(KeyCode.UpArrow))
        {
            AdjustGlobalSpeed(1f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            AdjustGlobalSpeed(-1f * Time.deltaTime);
        }
    }

    public void ToggleAllMovement()
    {
        if (layerComponents == null) return;

        foreach (ParallaxLayer layer in layerComponents)
        {
            if (layer != null)
            {
                if (pauseAllMovement)
                    layer.PauseMovement();
                else
                    layer.ResumeMovement();
            }
        }

        Debug.Log($"Movimiento sutil {(pauseAllMovement ? "pausado" : "reanudado")}");
    }

    public void AdjustGlobalRange(float deltaRange)
    {
        globalMovementRange = Mathf.Max(0.1f, globalMovementRange + deltaRange);

        if (layerComponents == null) return;

        for (int i = 0; i < layerComponents.Length; i++)
        {
            if (layerComponents[i] != null)
            {
                float multiplier = i < rangeMultipliers.Length ? rangeMultipliers[i] : 1f;
                layerComponents[i].SetMovementRange(globalMovementRange * multiplier);
            }
        }
    }

    public void AdjustGlobalSpeed(float deltaSpeed)
    {
        globalMovementSpeed = Mathf.Max(0.1f, globalMovementSpeed + deltaSpeed);

        if (layerComponents == null) return;

        for (int i = 0; i < layerComponents.Length; i++)
        {
            if (layerComponents[i] != null)
            {
                float multiplier = i < speedMultipliers.Length ? speedMultipliers[i] : 1f;
                layerComponents[i].SetMovementSpeed(globalMovementSpeed * multiplier);
            }
        }
    }

    public void SetGlobalRange(float newRange)
    {
        globalMovementRange = newRange;
        AdjustGlobalRange(0); // Aplicar sin cambio para refrescar
    }

    public void SetGlobalSpeed(float newSpeed)
    {
        globalMovementSpeed = newSpeed;
        AdjustGlobalSpeed(0); // Aplicar sin cambio para refrescar
    }

    public void ResetAllToOriginalPositions()
    {
        if (layerComponents == null) return;

        foreach (ParallaxLayer layer in layerComponents)
        {
            if (layer != null)
            {
                layer.ResetToOriginalPosition();
            }
        }
    }
}