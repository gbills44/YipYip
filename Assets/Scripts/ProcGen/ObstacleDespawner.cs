using Unity.VisualScripting;
using UnityEngine;

public class ObstacleDespawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log("ObstacleDespawner");

        if(collision.gameObject.tag == ("PlayerDespawnCollider"))
        {
            Destroy(this.GameObject());
        }
    }
}
