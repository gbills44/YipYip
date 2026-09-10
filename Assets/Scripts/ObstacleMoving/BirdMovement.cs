using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float riseSpeed = 3f;
    public float horizontalRange = 2f;
    public float spiralSpeed = 3f;

    private float startX;
    private float time;

    void Start()
    {
        riseSpeed = Random.Range(2f, 4f);
        horizontalRange = Random.Range(1f, 3.5f);
        spiralSpeed = Random.Range(1.5f, 3.5f);

        startX = transform.position.x;
    }

    void Update()
    {
        time += Time.deltaTime;

        float x = startX + Mathf.Sin(time * spiralSpeed) * horizontalRange;
        float y = transform.position.y + riseSpeed * Time.deltaTime;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}