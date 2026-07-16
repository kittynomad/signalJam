using UnityEngine;

public class BackgroundHazard : MonoBehaviour
{
    private bool playerInZone = false;
    private PlayerBehaviors pb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerBehaviors a))
        {
            playerInZone = true;
            pb = a;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInZone = false;
    }

    private void FixedUpdate()
    {
        if (playerInZone && pb.ExposedFunction())
        {
            //play animation or whatever
            pb.OnKill();
        }
    }
}
