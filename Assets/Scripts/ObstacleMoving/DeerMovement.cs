using UnityEngine;


public class DeerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float vertical = 0.6f;
    private Vector2 direction;

    void Start()
    {
        speed = Random.Range(5, 7);
        float horizontal = transform.position.x < 0 ? 1f : -1f;
        direction = new Vector2(horizontal, vertical);
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}