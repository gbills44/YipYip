
using System.Numerics;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos;
    private float backgroundLength = 28.0f;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cam.transform.position.y * parallaxEffect;
        float movement = cam.transform.position.y * (1 - parallaxEffect);
        transform.position = new UnityEngine.Vector3(transform.position.x, startPos + distance, transform.position.z);
        
        if(movement > (startPos + backgroundLength))
        {
            startPos += backgroundLength;
        }
        else if(movement < (startPos - backgroundLength))
        {
            startPos -= backgroundLength;
        }
    }
}
