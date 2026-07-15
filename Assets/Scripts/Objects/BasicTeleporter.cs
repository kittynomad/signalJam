using UnityEngine;

public class BasicTeleporter : MonoBehaviour
{
    [SerializeField] private GameObject _teleportLocation;

    public void TeleportPlayer()
    {
        GameObject player = FindAnyObjectByType<PlayerBehaviors>().gameObject;
        player.transform.position = _teleportLocation.transform.position;
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
}
