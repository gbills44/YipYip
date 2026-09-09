using UnityEngine;

public class SnowController : MonoBehaviour
{
    public PlayerController player;

    void Update()
    {
        transform.position = player.transform.position + Vector3.up * 20f;
    }
}
