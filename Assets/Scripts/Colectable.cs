using UnityEngine;

public class Colectable : MonoBehaviour
{
    private CounterColectable colectable;

    void Start()
    {
        colectable = FindObjectOfType<CounterColectable>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {   
            colectable.AddItem();
            Destroy(gameObject);
        }
    }
}
