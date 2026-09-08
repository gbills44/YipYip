using UnityEngine;


public class DeerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 direction = new Vector2(1f, 0.6f);

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}