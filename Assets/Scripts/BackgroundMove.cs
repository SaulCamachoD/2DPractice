using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private Vector2 MovementSpeed;
    private Vector2 offset;
    private Material material;
    
    private Rigidbody2D jugadorRB;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        jugadorRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        offset = (jugadorRB.velocity.x * 0.1f) * MovementSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
