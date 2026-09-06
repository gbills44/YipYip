using UnityEngine;

public class AvoidObstacle : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D boxColl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        

        
    }

}
