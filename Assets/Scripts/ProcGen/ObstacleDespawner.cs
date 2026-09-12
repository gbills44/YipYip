using Unity.VisualScripting;
using UnityEngine;

public class ObstacleDespawner : MonoBehaviour
{
    public float despawnDistance = 15f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null && transform.position.y < player.position.y - despawnDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log("ObstacleDespawner");

        if(collision.gameObject.tag == ("PlayerDespawnCollider"))
        {
            Destroy(this.GameObject());
        }
    }
}
