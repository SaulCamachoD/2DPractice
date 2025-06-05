using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("Velocidad del jugador")]
    [SerializeField] float velocidad;
    [Tooltip("Fuerza de salto del jugador")]
    [SerializeField] float fuerzaSalto;
    
    [Header("Configuracion de Animaciones")]
    [Tooltip("Asignar Script de animaciones del personaje")]
    [SerializeField] AnimationPlayer animator;
    
    [Header("Configuracion de salto")]
    [Tooltip("Asignar Layer Del piso")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] bool canJump;

    [Header("Wall Check")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask wallLayer;

    private bool isAgainstWall;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Verificar componentes esenciales
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D no encontrado en el jugador!", this);
            enabled = false; // Deshabilita el script si no hay Rigidbody2D
            return;
        }
        
        if (animator == null)
        {
            Debug.LogWarning("AnimationPlayer no asignado en el inspector!", this);
        }
        
        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck no asignado en el inspector!", this);
            enabled = false; // Deshabilita el script si no hay groundCheck
        }
    }

    void Update()
    {   
        // Si algún componente crítico falta, no ejecutar Update
        if (rb == null || groundCheck == null) return;
        
        // Entrada de movimiento
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        
        // Verificar groundCheck antes de usarlo
        if (groundCheck != null)
        {
            canJump = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
        
        // Activar animacion de caminar (si hay animator)
        if (animator != null)
        {
            animator.AnimationMove(movement.x);
        }
        
        // Configura hacia donde mira el jugador
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x > 0) // Solo cambiar si hay movimiento positivo
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {   
            if (animator != null)
            {
                animator.AnimationJump(true);
            }
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        }
        else if (animator != null)
        {
            animator.AnimationJump(false);
        }

        isAgainstWall = Physics2D.Raycast(wallCheck.position, transform.localScale.x > 0 ? Vector2.right : Vector2.left, wallCheckDistance, wallLayer);
    }
    
    void FixedUpdate()
    {   
        // Si el Rigidbody2D es nulo, no ejecutar
        if (rb == null) return;

        float targetVelocityX = isAgainstWall ? 0 : movement.x * velocidad;
        rb.velocity = new Vector2(targetVelocityX, rb.velocity.y);
    }

    void OnDrawGizmos()
    {
        // Solo dibujar gizmos si groundCheck está asignado
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Vector3 wallCheckDirection = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
            Gizmos.DrawRay(wallCheck.position, wallCheckDirection * wallCheckDistance);
        }
    }
}
