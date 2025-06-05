using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo a seguir")]
    [Tooltip("Asigna el transform del jugador aquí")]
    [SerializeField] private Transform target;

    [Header("Ajustes de seguimiento")]
    [Tooltip("Suavizado del movimiento de la cámara")]
    [SerializeField] private float smoothSpeed = 0.125f;
    [Tooltip("Compensa la posición vertical del objetivo")]
    [SerializeField] private float verticalOffset = 0f;
    [Tooltip("Compensa la posición horizontal del objetivo")]
    [SerializeField] private float horizontalOffset = 0f;

    [Header("Límites de la cámara")]
    [Tooltip("Activa los límites de la cámara")]
    [SerializeField] private bool useCameraBounds = false;
    [Tooltip("Límite mínimo (esquina inferior izquierda)")]
    [SerializeField] private Vector2 minBounds;
    [Tooltip("Límite máximo (esquina superior derecha)")]
    [SerializeField] private Vector2 maxBounds;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    void Start()
    {
        // Obtener referencia a la cámara
        cam = GetComponent<Camera>();
        
        // Verificar que tenemos un target asignado
        if (target == null)
        {
            Debug.LogError("No hay objetivo asignado para la cámara!", this);
            enabled = false;
            return;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calcular posición deseada con offsets
        Vector3 desiredPosition = new Vector3(
            target.position.x + horizontalOffset,
            target.position.y + verticalOffset,
            transform.position.z
        );

        // Suavizar el movimiento
        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothSpeed
        );

        // Aplicar límites si están activados
        if (useCameraBounds && cam != null)
        {
            // Calcular el tamaño de la cámara
            float camHeight = cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            // Asegurar que la cámara no salga de los límites
            smoothedPosition.x = Mathf.Clamp(
                smoothedPosition.x,
                minBounds.x + camWidth,
                maxBounds.x - camWidth
            );
            smoothedPosition.y = Mathf.Clamp(
                smoothedPosition.y,
                minBounds.y + camHeight,
                maxBounds.y - camHeight
            );
        }

        // Aplicar la posición suavizada
        transform.position = smoothedPosition;
    }

    // Método para cambiar el objetivo en tiempo de ejecución
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Método para actualizar los límites en tiempo de ejecución
    public void SetBounds(Vector2 newMinBounds, Vector2 newMaxBounds)
    {
        minBounds = newMinBounds;
        maxBounds = newMaxBounds;
    }

    // Dibujar los límites en el editor
    void OnDrawGizmosSelected()
    {
        if (useCameraBounds)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                new Vector3(minBounds.x, minBounds.y, 0),
                new Vector3(maxBounds.x, minBounds.y, 0)
            );
            Gizmos.DrawLine(
                new Vector3(maxBounds.x, minBounds.y, 0),
                new Vector3(maxBounds.x, maxBounds.y, 0)
            );
            Gizmos.DrawLine(
                new Vector3(maxBounds.x, maxBounds.y, 0),
                new Vector3(minBounds.x, maxBounds.y, 0)
            );
            Gizmos.DrawLine(
                new Vector3(minBounds.x, maxBounds.y, 0),
                new Vector3(minBounds.x, minBounds.y, 0)
            );
        }
    }
}
