using UnityEngine;

public class FallDetection : MonoBehaviour
{
    public RestartPosition resetPosition;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Detected");
            resetPosition.ResetPosition();
        }
    }
}
