using UnityEngine;

public class SavePosition : MonoBehaviour
{
    public RestartPosition savePosition;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            savePosition.SetCurrentPosition(transform.position);
        }
    }
}
