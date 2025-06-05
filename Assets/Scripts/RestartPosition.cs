using UnityEngine;

public class RestartPosition : MonoBehaviour
{   
    private Vector3 _initPosition;
    private Vector3 _currentPosition;
    void Start()
    {
        _initPosition = transform.position;
        _currentPosition = _initPosition;
    }

    public void SetCurrentPosition(Vector3 position)
    {
        _currentPosition = position;
    }

    public void ResetPosition()
    {
        transform.position = _currentPosition;
    }
}
